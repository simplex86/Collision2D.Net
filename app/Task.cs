using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    class Task
    {
        private Thread thread = null;
        private bool running = false;
        private Random random = new Random();
        private long previousTime = 0;

        private float runtimeAcc = 0;
        private int frameCount = 0;

        private Control canvas = null;
        private Control cfps = null;

        // 子线程刷新画布的委托
        private Action OnRefreshCanvasHandler;
        // 子线程刷新FPS的委托
        private Action OnRefreshFpsHandler;

        // 格林威治时间起始
        private static readonly DateTime GMT_ZERO = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        // 实体数量
        private const int ENTITY_COUNT = 150;

        public World world { get; private set; } = null;
        public int fps { get; private set; } = 0;

        public Task(Control canvas, Control cfps)
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

            this.canvas = canvas;
            this.cfps = cfps;

            OnRefreshCanvasHandler = () => canvas.Refresh();
            OnRefreshFpsHandler = () => cfps.Text = $"FPS: {fps}";
        }

        // 开始
        public void Start()
        {
            for (int i = 0; i < ENTITY_COUNT; i++)
            {
                var entity = CreateEntity();
                world.AddEntity(entity);
            }

            previousTime = GetCurrentTime();
            running = true;

            thread = new Thread(OnTick);
            thread.Start();
        }

        // 销毁
        public void Destroy()
        {
            try
            {
                running = false;

                if (thread != null)
                {
                    thread.Abort();
                    thread = null;
                }

                if (world != null)
                {
                    world.Destroy();
                    world = null;
                }
            }
            catch (Exception e)
            {

            }
        }

        // 线程函数
        private void OnTick()
        {
            while (running)
            {
                var deltaTime = GetDeltaTime();
                // 刷新数据
                world.Update(deltaTime);
                // 重绘（非常耗时）
                canvas.Invoke(OnRefreshCanvasHandler);
                // 碰撞检测
                world.LateUpdate(deltaTime);
                // 计算FPS并刷新UI
                UpdateFPS(deltaTime);
            }
        }

        // 刷新FPS
        private void UpdateFPS(float dt)
        {
            runtimeAcc += dt;
            frameCount++;

            if (runtimeAcc >= 1.0f)
            {
                fps = (int)(frameCount / runtimeAcc);
                cfps.Invoke(OnRefreshFpsHandler);

                runtimeAcc = 0;
                frameCount = 0;
            }
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
                default:
                    break;
            }

            var x = 0;
            var y = 0;
            while (x * y == 0)
            {
                x = random.Next(-99, 100);
                y = random.Next(-99, 100);
            }
            entity.movementComponent.direction = Vector.Normalize(x, y);
            entity.movementComponent.speed = random.Next(20, 80);

            var speed = (random.Next(0, 10) % 2 == 0) ? random.Next(20, 80)
                                                      : random.Next(-80, -20);
            entity.rotationComponent.speed = speed;

            return entity;
        }

        public CollisionType GetRandomCollisionType()
        {
            return (CollisionType)random.Next(0, 3);
        }

        // 创建圆形碰撞体
        private BaseCollision CreateCircleCollision()
        {
            BaseCollision collision = null;

            while (true)
            {
                var x = canvas.Location.X + random.Next(100, canvas.Width - 100);
                var y = canvas.Location.Y + random.Next(100, canvas.Height - 100);
                var position = new Vector(x, y);
                var radius = random.Next(15, 30);

                collision = CollisionFactory.CreateCircleCollision(ref position, radius);
                world.Each((entity) =>
                {
                    var overlay = entity.collisionComponent.collision.Collides(collision);
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
                var x = canvas.Location.X + random.Next(100, canvas.Width - 100);
                var y = canvas.Location.Y + random.Next(100, canvas.Height - 100);
                var position = new Vector(x, y);

                var width = random.Next(20, 50);
                var height = random.Next(20, 50);
                var angle = random.Next(0, 360);

                collision = CollisionFactory.CreateRectangleCollision(ref position, width, height, angle);
                world.Each((entity) =>
                {
                    var overlay = entity.collisionComponent.collision.Collides(collision);
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
                var x = canvas.Location.X + random.Next(100, canvas.Width - 100);
                var y = canvas.Location.Y + random.Next(100, canvas.Height - 100);
                var position = new Vector(x, y);

                var length = random.Next(18, 36);
                var radius = random.Next(8, Math.Min(14, length / 2));
                var angle = random.Next(0, 360);

                collision = CollisionFactory.CreateCapsuleCollision(ref position, length, radius, angle);
                world.Each((entity) =>
                {
                    var overlay = entity.collisionComponent.collision.Collides(collision);
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

        private long GetCurrentTime()
        {
            var ts = DateTime.UtcNow - GMT_ZERO;
            return Convert.ToInt64(ts.TotalMilliseconds);
        }

        private float GetDeltaTime()
        {
            var currentTime = GetCurrentTime();
            var deltaTime = (currentTime - previousTime) / 1000.0f;
            previousTime = currentTime;

            return deltaTime;
        }
    }
}
