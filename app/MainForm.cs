using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

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
            task = new Task(canvas);
            task.Start();
        }

        // 绘制
        private void OnPaintHandler(object sender, PaintEventArgs e)
        {
            var grap = e.Graphics;
            grap.SmoothingMode = SmoothingMode.HighQuality;

            task.world.Each((entity) =>
            {
                DrawEntity(grap, entity);
            });
        }

        // 绘制实体
        private void DrawEntity(Graphics grap, Entity entity)
        {
            var collision = entity.collisionComponent.collision;
            if (collision == null) return;

            var color = entity.colorComponent.color;

            if (collision is CircleCollision)
            {
                var circle = collision as CircleCollision;
                DrawCircleCollision(grap, circle, ref color);
            }
            else if (collision is RectangleCollision)
            {
                var rectangle = collision as RectangleCollision;
                DrawRectangleCollision(grap, rectangle, ref color);
            }
        }

        // 绘制圆形实体
        private void DrawCircleCollision(Graphics grap, CircleCollision collision, ref Color color)
        {
            var brush = new SolidBrush(color);

            var x = collision.position.x - collision.radius;
            var y = collision.position.y - collision.radius;
            var w = collision.radius * 2;
            var h = collision.radius * 2;

            grap.FillEllipse(brush, x, y, w, h);
        }

        // 绘制矩形实体
        private void DrawRectangleCollision(Graphics grap, RectangleCollision collision, ref Color color)
        {
            var brush = new SolidBrush(color);

            var x = collision.position.x;
            var y = collision.position.y;
            var w = collision.width * 0.5f;
            var h = collision.height * 0.5f;

            var p1 = new Vector(-w, -h);
            var p2 = new Vector( w, -h);
            var p3 = new Vector( w,  h);
            var p4 = new Vector(-w,  h);

            var m1 = Matrix.CreateRotationMatrix(collision.angle * MathX.DEG2RAD);
            var m2 = Matrix.CreateTranslationMatrix(x, y);
            var mt = m1 * m2; // 先旋转，再平移

            p1 = Matrix.Transform(ref p1, ref mt);
            p2 = Matrix.Transform(ref p2, ref mt);
            p3 = Matrix.Transform(ref p3, ref mt);
            p4 = Matrix.Transform(ref p4, ref mt);

            var points = new PointF[]
            {
                new PointF(p1.x, p1.y),
                new PointF(p2.x, p2.y),
                new PointF(p3.x, p3.y),
                new PointF(p4.x, p4.y),
                new PointF(p1.x, p1.y),
            };

            grap.FillPolygon(brush, points);
        }

        private void OnClosingHandler(object sender, FormClosingEventArgs e)
        {
            if (task != null)
            {
                task.Destroy();
                task = null;
            }
        }
    }
}
