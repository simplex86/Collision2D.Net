using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SimpleX.Collision2D.App
{
    class RenderTask
    {
        private Thread thread = null;
        private long previousTime = 0;

        private World world = null;

        private float canvasTime = 0;
        private float statsTime = 0;
        private int frameCount = 0;

        private Control canvas = null;
        private Control stats = null;

        // 子线程刷新画布的委托
        private Action OnRefreshCanvasHandler;
        // 子线程刷新Stats的委托
        private Action OnRefreshStatsHandler;
        
        public float cost { get; private set; } = 1.0f;

        public RenderTask(World world)
        {
            this.world = world;
        }

        public void SetCanvasHandler(Control canvas, Action handler)
        {
            this.canvas = canvas;
            OnRefreshCanvasHandler = handler;
        }

        public void SetStatsHandler(Control stats, Action handler)
        {
            this.stats = stats;
            OnRefreshStatsHandler = handler;
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
            statsTime += dt;
            canvasTime += dt;

            if (canvasTime >= 0.016f)
            {
                canvas.Invoke(OnRefreshCanvasHandler);
                canvasTime = 0;

                frameCount++;
                if (statsTime >= 1.0f)
                {
                    cost = statsTime / frameCount;
                    stats.Invoke(OnRefreshStatsHandler);

                    statsTime = 0;
                    frameCount = 0;
                }
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
