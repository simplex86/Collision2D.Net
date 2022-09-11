using System;
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

        private Random random = new Random(1);

        private readonly int width;
        private readonly int height;

        // 实体数量
        private const int ENTITY_COUNT = 100;

        public Task(Control canvas, Control stats)
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

            collide = new CollideTask(world);
            render = new RenderTask(world, canvas, stats);

            render.OnRefreshCanvasHandler = () => canvas.Refresh();
            render.OnRefreshStatsHandler = () => {
                var countText  = string.Format("Collision Count: {0}", world.GetEntityCount());
                var renderText = string.Format("Render\n  FPS: {0}  Cost: {1:F2} ms", (int)(1.0f / render.cost), render.cost * 1000);
                var logicText  = string.Format("Collide\n  FPS: {0}  Cost: {1:F2} ms", (int)(1.0f / collide.cost), collide.cost * 1000);
                stats.Text = $"{countText}\n{renderText}\n{logicText}";
            };

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

            collide.Start();
            render.Start();
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
                case CollisionType.Triangle:
                    entity.collisionComponent.collision = CreateTriangleCollision();
                    entity.colorComponent.color = Color.Orange;
                    entity.renderComponent.renderer = new TriangleRenderer();
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
                    var overlay = entity.collisionComponent.collision.Overlays(collision);
                    if (overlay)
                    {
                        collision = null;
                    }
                    return !overlay;
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
                    var overlay = entity.collisionComponent.collision.Overlays(collision);
                    if (overlay)
                    {
                        collision = null;
                    }
                    return !overlay;
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
                    var overlay = entity.collisionComponent.collision.Overlays(collision);
                    if (overlay)
                    {
                        collision = null;
                    }
                    return !overlay;
                });

                if (collision != null) break;
            }
            return collision;
        }

        // 创建矩形碰撞体
        private BaseCollision CreateTriangleCollision()
        {
            BaseCollision collision = null;

            while (true)
            {
                var o = GetRandomPosition();
                var a = GetRandomPosition(ref o, -30, -10);
                var b = GetRandomPosition(ref o, 10, 30);
                var c = GetRandomPosition(ref o, -25, 25);

                if (GetTriangleSize(ref a, ref b, ref c) < 200) continue;

                collision = CollisionFactory.CreateTriangleCollision(ref a, ref b, ref c);
                world.Each((entity) =>
                {
                    var overlay = entity.collisionComponent.collision.Overlays(collision);
                    if (overlay)
                    {
                        collision = null;
                    }
                    return !overlay;
                });

                if (collision != null) break;
            }
            return collision;
        }

        private Vector GetRandomPosition()
        {
            var x = random.Next(50, width - 50);
            var y = random.Next(50, height - 50);
            return new Vector(x, y);
        }

        private Vector GetRandomPosition(ref Vector O, int min, int max)
        {
            var x = random.Next(min, max+1);
            var y = random.Next(min, max+1);
            return new Vector(O.x + x, O.y + y);
        }

        private float GetTriangleSize(ref Vector a, ref Vector b, ref Vector c)
        {
            float x = MathX.Sqrt(GeometryHelper.GetDistance2(ref a, ref b));
            float y = MathX.Sqrt(GeometryHelper.GetDistance2(ref b, ref c));
            float z = MathX.Sqrt(GeometryHelper.GetDistance2(ref c, ref a));
            float p = (x + y + z) * 0.5f;

            return MathX.Sqrt(p * (p - x) * (p - y) * (p - z));
        }
    }
}
