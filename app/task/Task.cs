using System;
using System.Windows.Forms;
using System.Drawing;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class Task
    {
        public World world { get; private set; }

        private CollideTask collide;
        private RenderTask render;

        private Random random = new Random();
        private Stats stats = new Stats();
        private Timer timer = new Timer();

        private readonly int width;
        private readonly int height;

        // 实体数量
        private const int ENTITY_COUNT = 100;

        public Task(Control canvas, Control detail)
        {
            this.world = new World()
            {
                left = new Boundary()
                {
                    x = canvas.Location.X,
                    y = canvas.Location.Y,
                    normal = Vector2.right
                },
                right = new Boundary()
                {
                    x = canvas.Location.X + canvas.Width,
                    y = canvas.Location.Y,
                    normal = Vector2.left
                },
                top = new Boundary()
                {
                    x = canvas.Location.X,
                    y = canvas.Location.Y,
                    normal = Vector2.down
                },
                bottom = new Boundary()
                {
                    x = canvas.Location.X,
                    y = canvas.Location.Y + canvas.Height,
                    normal = Vector2.up
                },
            };

            collide = new CollideTask(world, stats);
            render = new RenderTask(world, stats, canvas)
            {
                OnRefreshCanvasHandler = () => canvas.Refresh()
            };

            timer.Interval = 500;
            timer.Tag = detail;
            timer.Tick += OnTimerHandler;

            width = canvas.Width;
            height = canvas.Height;
        }

        public void Start()
        {
            for (int i = 0; i < ENTITY_COUNT; i++)
            {
                var entity = CreateEntity();
                world.AddEntity(entity);
            }
            stats.colliderCount = ENTITY_COUNT;

            collide.Start();
            render.Start();

            timer.Start();
        }

        public void Destroy()
        {
            timer.Stop();

            collide.Destroy();
            render.Destroy();
        }

        // 创建实体
        private Entity CreateEntity()
        {
            var entity = new Entity();

            var type = GetRandomColliderType();
            while (true)
            {
                var transform = CreateRanfomTransform();
                entity.transformComponent.transform = transform;

                entity.boundingRendererComponent.renderer = RendererFactory.CreateAABBRenderer();
                entity.velocityRendererComponent.renderer = RendererFactory.CreateVelocityRenderer();

                switch (type)
                {
                    case GeometryType.Circle:
                        var circle = CreateRandomCircle();
                        entity.collisionComponent.collider = ColliderFactory.CreateCollider(circle);
                        entity.colorComponent.color = Color.Red;
                        entity.geometryRendererComponent.renderer = RendererFactory.CreateRenderer(circle);
                        break;
                    case GeometryType.Rectangle:
                        var rectangle = CreateRandomRectangle();
                        entity.collisionComponent.collider = ColliderFactory.CreateCollider(rectangle);
                        entity.colorComponent.color = Color.Green;
                        entity.geometryRendererComponent.renderer = RendererFactory.CreateRenderer(rectangle);
                        break;
                    case GeometryType.Capsule:
                        var capsule = CreateRandomCapsule();
                        entity.collisionComponent.collider = ColliderFactory.CreateCollider(capsule);
                        entity.colorComponent.color = Color.Blue;
                        entity.geometryRendererComponent.renderer = RendererFactory.CreateRenderer(capsule);
                        break;
                    case GeometryType.Polygon:
                        var polygon = CreateRandomPolygon();
                        entity.collisionComponent.collider = ColliderFactory.CreateCollider(polygon);
                        entity.colorComponent.color = Color.Orange;
                        entity.geometryRendererComponent.renderer = RendererFactory.CreateRenderer(polygon);
                        break;
                    case GeometryType.Ellipse:
                        var ellipse = CreateRandomEllipse();
                        entity.collisionComponent.collider = ColliderFactory.CreateCollider(ellipse);
                        entity.colorComponent.color = Color.Cyan;
                        entity.geometryRendererComponent.renderer = RendererFactory.CreateRenderer(ellipse);
                        break;
                    case GeometryType.Pie:
                        var pie = CreateRandomPie();
                        entity.collisionComponent.collider = ColliderFactory.CreateCollider(pie);
                        entity.colorComponent.color = Color.DarkKhaki;
                        entity.geometryRendererComponent.renderer = RendererFactory.CreateRenderer(pie);
                        break;
                    case GeometryType.Segment:
                        var segment = CreateRandomSegment();
                        entity.collisionComponent.collider = ColliderFactory.CreateCollider(segment);
                        entity.colorComponent.color = Color.DarkSalmon;
                        entity.geometryRendererComponent.renderer = RendererFactory.CreateRenderer(segment);
                        break;
                    default:
                        break;
                }

                var collider = entity.collisionComponent.collider;
                collider.RefreshGeometry(transform);

                var overlap = false;
                world.Each((e) =>
                {
                    overlap = e.collisionComponent.collider.Overlaps(collider);
                    return !overlap;
                });

                if (!overlap) break;
            }

            var movable = IsMovable();
            if (movable)
            {
                var x = 0;
                var y = 0;
                while (x * y == 0)
                {
                    x = random.Next(-99, 100);
                    y = random.Next(-99, 100);
                }
                entity.velocityComponent.direction = Vector2.Normalize(x, y);
                entity.velocityComponent.magnitude = random.Next(40, 80);

                var rotatable = IsRotatable(); // 能移动的才有可能旋转
                if (rotatable)
                {
                    var speed = (random.Next(0, 10) % 2 == 0) ? random.Next(20, 80)
                                                              : random.Next(-80, -20);
                    entity.rotationComponent.speed = speed;
                }
            }

            // 不能移动(同时不能旋转）的渲染成灰色
            if (!movable)
            {
                entity.colorComponent.color = Color.DarkGray;
            }

            return entity;
        }

        // 随机获取碰撞体类型
        public GeometryType GetRandomColliderType()
        {
            return (GeometryType)random.Next((int)GeometryType.BOT, 
                                             (int)GeometryType.EOT);
        }

        // 创建随机圆形
        private Circle CreateRandomCircle()
        {
            return new Circle()
            {
                radius = random.Next(15, 30),
            };
        }

        // 创建随机矩形
        private Rectangle CreateRandomRectangle()
        {
            return new Rectangle()
            {
                width = random.Next(45, 60),
                height = random.Next(20, 40)
            };
        }

        // 创建随机胶囊
        private Capsule CreateRandomCapsule()
        {
            var length = random.Next(18, 36);

            return new Capsule()
            {
                length = length,
                radius = random.Next(8, MathX.Min(14, length / 2)),
            };
        }

        // 创建随机多边形
        private Polygon CreateRandomPolygon()
        {
            var vertics = GetRandomConvexPoints();
            // 面积太小的不要
            while (CalculatePolygonSize(vertics) < 300)
            {
                vertics = GetRandomConvexPoints();
            }

            return new Polygon()
            {
                vertics = vertics
            };
        }

        // 创建随机椭圆
        private Ellipse CreateRandomEllipse()
        {
            return new Ellipse()
            {
                width = random.Next(45, 60),
                height = random.Next(20, 40),
            };
        }

        // 创建随机扇形
        private Pie CreateRandomPie()
        {
            return new Pie()
            {
                radius = random.Next(15, 30),
                sweep = random.Next(10, 180),
            };
        }

        // 创建随机线段
        private Segment CreateRandomSegment()
        {
            return new Segment()
            { 
                length = random.Next(30, 60),
            };
        }

        // 是否可移动
        public bool IsMovable()
        {
            return random.Next(0, 10) > 0;
        }

        // 是否可旋转
        public bool IsRotatable()
        {
            return true;
        }

        private Transform CreateRanfomTransform()
        {
            var x = random.Next(50, width - 50);
            var y = random.Next(50, height - 50);

            return new Transform()
            {
                position = new Vector2(x, y),
                rotation = random.Next(0, 360)
            };
        }

        // 计算多边形面积
        // 注：格林公式
        private float CalculatePolygonSize(Vector2[] vertics)
        {
            var s = 0.0f;
            var i = 0;
            for (; i<vertics.Length-1; i++)
            {
                var a = vertics[i];
                var b = vertics[i + 1];
                s += (a.x + b.x) * (b.y - a.y);
            }
            var c = vertics[i];
            var d = vertics[0];
            s += (c.x + d.x) * (d.y - c.y);

            return MathX.Abs(s) * 0.5f;
        }

        // 生成随机的凸多边形
        // 注：顶点数[3, 9]
        private Vector2[] GetRandomConvexPoints()
        {
            var count = random.Next(3, 10);
            var convex = new ConvexGenerator(random);
            return convex.Gen(45, 45, count);
        }

        private void OnTimerHandler(object sender, EventArgs e)
        {
            var timer = sender as Timer;
            var detail = timer.Tag as Control;

            UpdateStats(detail);
            stats.Reset();
        }

        private void UpdateStats(Control detail)
        {
            var countText = string.Format("Collider Count: {0}", stats.colliderCount);
            var renderText = string.Format("Render\n  FPS: {0}  Cost: {1:F2} ms", (int)(1.0f / stats.renderCost), stats.renderCost * 1000);
            var logicText = string.Format("Collide\n  FPS: {0}  Cost: {1:F2} ms", (int)(1.0f / stats.collideCost), stats.collideCost * 1000);
            detail.Text = $"{countText}\n{renderText}\n{logicText}";
        }
    }
}
