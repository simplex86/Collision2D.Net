﻿using System;
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
            stats.collisionCount = ENTITY_COUNT;

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

            var type = GetRandomCollisionType();
            while (true)
            {
                switch (type)
                {
                    case CollisionType.Circle:
                        var circle = CreateRandomCircle();
                        entity.collisionComponent.collision = CreateCircleCollision(ref circle);
                        entity.colorComponent.color = Color.Red;
                        entity.renderComponent.renderer = new CircleRenderer(circle);
                        break;
                    case CollisionType.Rectangle:
                        var rectangle = CreateRandomRectangle();
                        entity.collisionComponent.collision = CreateRectangleCollision(ref rectangle);
                        entity.colorComponent.color = Color.Green;
                        entity.renderComponent.renderer = new RectangleRenderer(rectangle);
                        break;
                    case CollisionType.Capsule:
                        var capsule = CreateRandomCapsule();
                        entity.collisionComponent.collision = CreateCapsuleCollision(ref capsule);
                        entity.colorComponent.color = Color.Blue;
                        entity.renderComponent.renderer = new CapsuleRenderer(capsule);
                        break;
                    case CollisionType.Polygon:
                        var polygon = CreateRandomPolygon();
                        entity.collisionComponent.collision = CreatePolygonCollision(ref polygon);
                        entity.colorComponent.color = Color.Orange;
                        entity.renderComponent.renderer = new PolygonRenderer(polygon);
                        break;
                    case CollisionType.Ellipse:
                        var ellipse = CreateRandomEllipse();
                        entity.collisionComponent.collision = CreateEllipseCollision(ref ellipse);
                        entity.colorComponent.color = Color.Cyan;
                        entity.renderComponent.renderer = new EllipseRenderer(ellipse);
                        break;
                    default:
                        break;
                }

                var overlap = false;
                var collision = entity.collisionComponent.collision;

                world.Each((e) =>
                {
                    overlap = e.collisionComponent.collision.Overlaps(collision);
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
                entity.movementComponent.direction = Vector2.Normalize(x, y);
                entity.movementComponent.speed = random.Next(20, 80);

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
                entity.colorComponent.color = Color.Gray;
            }

            return entity;
        }

        // 随机获取碰撞体类型
        public CollisionType GetRandomCollisionType()
        {
            //return CollisionType.Ellipse;
            return (CollisionType)random.Next(0, 4);
        }

        // 
        private Circle CreateRandomCircle()
        {
            return new Circle()
            {
                radius = random.Next(15, 30),
            };
        }

        // 
        private Rectangle CreateRandomRectangle()
        {
            return new Rectangle()
            {
                width = random.Next(20, 50),
                height = random.Next(20, 50)
            };
        }

        // 
        private Capsule CreateRandomCapsule()
        {
            var length = random.Next(18, 36);

            return new Capsule()
            {
                length = length,
                radius = random.Next(8, Math.Min(14, length / 2)),
            };
        }

        // 
        private Polygon CreateRandomPolygon()
        {
            var vertics = GetRandomConvexPoints();
            while (CalculatePolygonSize(vertics) < 300)
            {
                vertics = GetRandomConvexPoints();
            }

            return new Polygon()
            {
                vertics = vertics
            };
        }

        // 
        private Ellipse CreateRandomEllipse()
        {
            return new Ellipse()
            {
                width = 50,//random.Next(20, 50),
                height = 20,//random.Next(20, 50),
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

        // 创建圆形碰撞体
        private BaseCollision CreateCircleCollision(ref Circle circle)
        {
            var position = GetRandomPosition();
            return CollisionFactory.CreateCircleCollision(ref position, ref circle);
        }

        // 创建矩形碰撞体
        private BaseCollision CreateRectangleCollision(ref Rectangle rectangle)
        {
            var position = GetRandomPosition();
            var rotation = GetRandomRotation();

            return CollisionFactory.CreateRectangleCollision(ref position, ref rectangle, rotation);
        }

        // 创建矩形碰撞体
        private BaseCollision CreateCapsuleCollision(ref Capsule capsule)
        {
            var position = GetRandomPosition();
            var rotation = GetRandomRotation();

            return CollisionFactory.CreateCapsuleCollision(ref position, ref capsule, rotation);
        }

        // 创建矩形碰撞体
        private BaseCollision CreatePolygonCollision(ref Polygon polygon)
        {
            var position = GetRandomPosition();
            var rotation = GetRandomRotation();

            return CollisionFactory.CreatePolygonCollision(ref position, ref polygon, rotation);
        }

        // 创建椭圆碰撞体
        private BaseCollision CreateEllipseCollision(ref Ellipse ellipse)
        {
            var position = GetRandomPosition();
            var rotation = GetRandomRotation();

            return CollisionFactory.CreateEllipseCollision(ref position, ref ellipse, rotation);
        }

        // 获取随机坐标
        private Vector2 GetRandomPosition()
        {
            var x = random.Next(50, width - 50);
            var y = random.Next(50, height - 50);
            return new Vector2(x, y);
        }

        // 获取随机旋转角度
        private float GetRandomRotation()
        {
            return random.Next(0, 360);
        }

        // 计算三角形面积
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
            var position = Vector2.zero;

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
