using System;
using System.Threading;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    class CollideTask
    {
        private Thread thread = null;
        private World world = null;
        private Stats stats = null;
        private long previousTime = 0;

        public CollideTask(World world, Stats stats)
        {
            this.world = world;
            this.stats = stats;
        }

        // 开始
        public void Start()
        {
            previousTime = GMT.now;

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
                stats.OnCollideFrame(deltaTime);
            }
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
