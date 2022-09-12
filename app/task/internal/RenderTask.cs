using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SimpleX.Collision2D.App
{
    class RenderTask
    {
        private Thread thread = null;
        private World world = null;
        private Stats stats = null;

        private long previousTime = 0;
        private float canvasTime = 0;

        private Control canvas = null;
        // 子线程刷新画布的委托
        public Action OnRefreshCanvasHandler;

        public RenderTask(World world, Stats stats, Control canvas)
        {
            this.world = world;
            this.stats = stats;
            this.canvas = canvas;
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
                // 统计数据
                UpdateStats(deltaTime);
            }
        }

        // 刷新统计数据
        private void UpdateStats(float dt)
        {
            canvasTime += dt;

            if (canvasTime >= 0.016f)
            {
                stats.OnRenderFrame(canvasTime);

                canvas.Invoke(OnRefreshCanvasHandler);
                canvasTime = 0;
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
