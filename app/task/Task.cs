using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

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
                    normal = Vector.right
                },
                right = new Boundary()
                {
                    x = canvas.Location.X + canvas.Width,
                    y = canvas.Location.Y,
                    normal = Vector.left
                },
                top = new Boundary()
                {
                    x = canvas.Location.X,
                    y = canvas.Location.Y,
                    normal = Vector.down
                },
                bottom = new Boundary()
                {
                    x = canvas.Location.X,
                    y = canvas.Location.Y + canvas.Height,
                    normal = Vector.up
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
            stats.collisionCount = ENTITY_COUNT;

            collide.Start();
            render.Start();

            timer.Start();
        }

        public void Destroy()
        {
            collide.Destroy();
            render.Destroy();
        }

        // 创建实体
        private Entity CreateEntity()
        {
            var entity = new Entity();

            var type = GetRandomCollisionType();
            switch (type)
            {
                case CollisionType.Circle:
                    entity.collisionComponent.collision = CreateCircleCollision();
                    entity.colorComponent.color = Color.Red;
                    entity.renderComponent.renderer = new CircleRenderer();
                    break;
                case CollisionType.Rectangle:
                    entity.collisionComponent.collision = CreateRectangleCollision();
                    entity.colorComponent.color = Color.Green;
                    entity.renderComponent.renderer = new RectangleRenderer();
                    break;
                case CollisionType.Capsule:
                    entity.collisionComponent.collision = CreateCapsuleCollision();
                    entity.colorComponent.color = Color.Blue;
                    entity.renderComponent.renderer = new CapsuleRenderer();
                    break;
                case CollisionType.Polygon:
                    entity.collisionComponent.collision = CreatePolygonCollision();
                    entity.colorComponent.color = Color.Orange;
                    entity.renderComponent.renderer = new PolygonRenderer();
                    break;
                default:
                    break;
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
                entity.movementComponent.direction = Vector.Normalize(x, y);
                entity.movementComponent.speed = random.Next(20, 80);

                var rotatable = IsRotatable(); // 能移动的才有可能旋转
                if (rotatable)
                {
                    var speed = (random.Next(0, 10) % 2 == 0) ? random.Next(20, 80)
                                                              : random.Next(-80, -20);
                    entity.rotationComponent.speed = speed;
                }
            }

            // 不能移动(同时即不能旋转）的渲染成灰色
            if (!movable)
            {
                entity.colorComponent.color = Color.Gray;
            }

            return entity;
        }

        // 随机获取碰撞体类型
        public CollisionType GetRandomCollisionType()
        {
            return (CollisionType)random.Next(0, 4);
        }

        // 是否可移动
        public bool IsMovable()
        {
            return random.Next(0, 10) > 0;
        }

        // 是否可旋转
        public bool IsRotatable()
        {
            return random.Next(0, 10) > 4;
        }

        // 创建圆形碰撞体
        private BaseCollision CreateCircleCollision()
        {
            BaseCollision collision = null;

            while (true)
            {
                var position = GetRandomPosition();
                var radius = random.Next(15, 30);

                collision = CollisionFactory.CreateCircleCollision(ref position, radius);
                world.Each((entity) =>
                {
                    var overlap = entity.collisionComponent.collision.Overlaps(collision);
                    if (overlap)
                    {
                        collision = null;
                    }
                    return !overlap;
                });

                if (collision != null) break;
            }

            return collision;
        }

        // 创建矩形碰撞体
        private BaseCollision CreateRectangleCollision()
        {
            BaseCollision collision = null;

            while (true)
            {
                var position = GetRandomPosition();
                var width = random.Next(20, 50);
                var height = random.Next(20, 50);
                var angle = random.Next(0, 360);

                collision = CollisionFactory.CreateRectangleCollision(ref position, width, height, angle);
                world.Each((entity) =>
                {
                    var overlap = entity.collisionComponent.collision.Overlaps(collision);
                    if (overlap)
                    {
                        collision = null;
                    }
                    return !overlap;
                });

                if (collision != null) break;
            }

            return collision;
        }

        // 创建矩形碰撞体
        private BaseCollision CreateCapsuleCollision()
        {
            BaseCollision collision = null;

            while (true)
            {
                var position = GetRandomPosition();
                var length = random.Next(18, 36);
                var radius = random.Next(8, Math.Min(14, length / 2));
                var angle = random.Next(0, 360);

                collision = CollisionFactory.CreateCapsuleCollision(ref position, length, radius, angle);
                world.Each((entity) =>
                {
                    var overlap = entity.collisionComponent.collision.Overlaps(collision);
                    if (overlap)
                    {
                        collision = null;
                    }
                    return !overlap;
                });

                if (collision != null) break;
            }
            return collision;
        }

        // 创建矩形碰撞体
        private BaseCollision CreatePolygonCollision()
        {
            BaseCollision collision = null;

            while (true)
            {
                var vertics = GetRandomConvexPoints();
                if (CalculatePolygonSize(vertics) < 300) continue;

                collision = CollisionFactory.CreatePolygonCollision(vertics);
                world.Each((entity) =>
                {
                    var overlap = entity.collisionComponent.collision.Overlaps(collision);
                    if (overlap)
                    {
                        collision = null;
                    }
                    return !overlap;
                });

                if (collision != null) break;
            }
            return collision;
        }

        // 获取随机坐标
        private Vector GetRandomPosition()
        {
            var x = random.Next(50, width - 50);
            var y = random.Next(50, height - 50);
            return new Vector(x, y);
        }

        // 计算三角形面积
        // 注：格林公式
        private float CalculatePolygonSize(Vector[] vertics)
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
        private Vector[] GetRandomConvexPoints()
        {
            var count = random.Next(3, 10);
            var position = GetRandomPosition();

            var convex = new ConvexGenerator(random);
            return convex.Gen(ref position, 45, 45, count);
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
            var countText = string.Format("Collision Count: {0}", stats.collisionCount);
            var renderText = string.Format("Render\n  FPS: {0}  Cost: {1:F2} ms", (int)(1.0f / stats.renderCost), stats.renderCost * 1000);
            var logicText = string.Format("Collide\n  FPS: {0}  Cost: {1:F2} ms", (int)(1.0f / stats.collideCost), stats.collideCost * 1000);
            detail.Text = $"{countText}\n{renderText}\n{logicText}";
        }
    }
}
