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
        private Control canvas = null;
        private long timestamp = 0;

        // 子线程刷新控件的委托
        private Action OnRefreshCanvasHandler;
        // 格林威治时间起始
        private static readonly DateTime GMT_ZERO = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public World world { get; private set; } = null;

        public Task(Control canvas)
        {
            this.world = new World();
            this.canvas = canvas;

            OnRefreshCanvasHandler = () => canvas.Refresh();
        }

        // 开始
        public void Start()
        {
            for (int i = 0; i < 20; i++)
            {
                var entity = CreateEntity();
                world.AddEntity(entity);
            }

            timestamp = GetCurrentTime();
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
                var currentTime = GetCurrentTime();
                var deltaTime = (currentTime - timestamp) / 1000.0f;

                // 刷新数据后重绘
                world.Update(deltaTime);
                canvas.Invoke(OnRefreshCanvasHandler);
                // 碰撞检测
                world.LateUpdate(deltaTime);

                timestamp = currentTime;
                Thread.Sleep(1);
            }
        }

        // 创建实体
        private Entity CreateEntity()
        {
            var entity = new Entity();

            var type = random.Next(0, 2);
            switch (type)
            {
                case 0:
                    entity.collisionComponent.collision = CreateCircleCollision();
                    entity.colorComponent.color = Color.Red;
                    break;
                case 1:
                    entity.collisionComponent.collision = CreateRectangleCollision();
                    entity.colorComponent.color = Color.Green;
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
            entity.movementComponent.speed = random.Next(10, 30);

            var speed = (random.Next(0, 10) % 2 == 0) ? random.Next(10, 50) 
                                                      : random.Next(-50, -10);
            entity.rotationComponent.speed = speed;

            return entity;
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
                var radius = random.Next(20, 50);

                collision = CollisionFactory.CreateCircleCollision(ref position, radius);
                world.Each((entity) =>
                {
                    var collided = entity.collisionComponent.collision.Collides(collision);
                    if (collided)
                    {
                        collision = null;
                    }
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

                var width = random.Next(20, 60);
                var height = random.Next(20, 60);
                var angle = random.Next(0, 360);

                collision = CollisionFactory.CreateRectangleCollision(ref position, width, height, angle);
                world.Each((entity) =>
                {
                    var collided = entity.collisionComponent.collision.Collides(collision);
                    if (collided)
                    {
                        collision = null;
                    }
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
    }
}
