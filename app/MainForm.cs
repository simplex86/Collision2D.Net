using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX
{
    using SimpleX.Collision2D;

    public partial class MainForm : Form
    {
        private Task task = null;

        public MainForm()
        {
            InitializeComponent();
        }

        // 窗体加载完后，初始化数据
        private void OnLoadHandler(object sender, EventArgs e)
        {
            task = new Task(canvas, detail);
            task.Start();
        }

        // 渲染
        private void OnPaintHandler(object sender, PaintEventArgs e)
        {
            var grap = e.Graphics;
            grap.SmoothingMode = SmoothingMode.HighQuality;

            var world = task.world;
            world.Each((entity) =>
            {
                DrawEntity(grap, entity);
            });
        }

        // 绘制实体
        private void DrawEntity(Graphics grap, Entity entity)
        {
            var collision = entity.collisionComponent.collision;
            var direction = entity.movementComponent.direction;
            var speed = entity.movementComponent.speed;
            var color = entity.colorComponent.color;
            var renderer = entity.renderComponent.renderer;

            direction *= speed;
            renderer.Render(grap, collision, ref direction, ref color);
        }

        private void OnClosingHandler(object sender, FormClosingEventArgs e)
        {
            if (task != null)
            {
                task.Destroy();
                task = null;
            }
        }

        private void OnBoundingBoxVisibleChanged(object sender, EventArgs e)
        {
            var world = task.world;
            world.Each((entity) =>
            {
                var renderer = entity.renderComponent.renderer;
                var checkbox = sender as CheckBox;
                renderer.showBoundingBox = checkbox.Checked;
            });
        }

        private void OnDirectionVisibleChanged(object sender, EventArgs e)
        {
            var world = task.world;
            world.Each((entity) =>
            {
                var renderer = entity.renderComponent.renderer;
                var checkbox = sender as CheckBox;
                renderer.showDirection = checkbox.Checked;
            });
        }
    }
}
