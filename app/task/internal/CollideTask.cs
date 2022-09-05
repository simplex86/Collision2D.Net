using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    class CollideTask
    {
        private Thread thread = null;
        
        private readonly int width;
        private readonly int height;

        private Random random = new Random();

        private long previousTime = 0;
        private float runtimeAcc = 0;
        private int frameCount = 0;

        // 实体数量
        private const int ENTITY_COUNT = 100;

        private World world = null;
        public float cost { get; private set; } = 1.0f;

        public CollideTask(World world, int width, int height)
        {
            this.world = world;
            this.width = width;
            this.height = height;
        }

        // 开始
        public void Start()
        {
            previousTime = GMT.now;

            for (int i = 0; i < ENTITY_COUNT; i++)
            {
                var entity = CreateEntity();
                world.AddEntity(entity);
            }

            thread = new Thread(OnTick);
            thread.Start();
        }

        // 销毁
        public void Destroy()
        {
            try
            {
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
            while (true)
            {
                var deltaTime = GetDeltaTime();
                // 刷新数据
                world.Update(deltaTime);
                // 碰撞检测
                world.LateUpdate(deltaTime);
                // 刷新统计数据
                UpdateStats(deltaTime);
            }
        }

        // 统计数据
        private void UpdateStats(float dt)
        {
            runtimeAcc += dt;
            frameCount++;

            if (runtimeAcc >= 1.0f)
            {
                cost = runtimeAcc / frameCount;

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

        // 随机获取碰撞体类型
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
                var position = GetRandomPosition();
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
                var position = GetRandomPosition();
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
                var position = GetRandomPosition();
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

        private Vector GetRandomPosition()
        {
            var x = random.Next(50, width - 50);
            var y = random.Next(50, height - 50);
            return new Vector(x, y);
        }

        private float GetDeltaTime()
        {
            var currentTime = GMT.now;
            var deltaTime = (currentTime - previousTime) / 1000.0f;
            previousTime = currentTime;

            return deltaTime;
        }
    }
}
