using System;
using System.Threading;

namespace SimpleX
{
    class LogicTask
    {
        private Thread thread = null;
        private World world = null;
        private Stats stats = null;
        private DeltaTime deltaTime = new DeltaTime();

        public LogicTask(World world, Stats stats)
        {
            this.world = world;
            this.stats = stats;
        }

        // 开始
        public void Start()
        {
            deltaTime.Init();

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
                var dt = deltaTime.Get();
                
                // 刷新数据
                world.Update(dt);
                // 碰撞检测
                world.FixedUpdate(dt); 
                // 
                world.LateUpdate(dt);
                // 刷新统计数据
                stats.OnCollideFrame(dt);
            }
        }
    }
}
