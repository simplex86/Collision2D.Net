using System;
using System.Windows.Forms;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    class Task
    {
        public World world { get; private set; }

        private LogicTask logic;
        private RenderTask render;

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

            logic = new LogicTask(world, canvas.Width, canvas.Height);

            render = new RenderTask(world);
            render.SetCanvasHandler(canvas, () => canvas.Refresh());
            render.SetStatsHandler(stats, () => {
                var countText = string.Format("Collision Count: {0}", world.GetEntityCount());
                var renderText = string.Format("Render\n  FPS: {0}  Cost: {1:F2} ms", (int)(1.0f / render.cost), render.cost * 1000);
                var logicText = string.Format("Logic\n  FPS: {0}  Cost: {1:F2} ms", (int)(1.0f / logic.cost), logic.cost * 1000);
                stats.Text = $"{countText}\n{renderText}\n{logicText}";
            });
        }

        public void Start()
        {
            logic.Start();
            render.Start();
        }

        public void Destroy()
        {
            logic.Destroy();
            render.Destroy();
        }
    }
}
