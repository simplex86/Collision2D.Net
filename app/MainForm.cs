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

            var world = task.world;
            world.Each((entity) =>
            {
                DrawEntity(grap, entity);
                return true;
            });

            DrawBorder(grap, ref world.left, ref world.right, ref world.top, ref world.bottom);
        }

        // 绘制实体
        private void DrawEntity(Graphics grap, Entity entity)
        {
            var collision = entity.collisionComponent.collision;
            if (collision == null) return;

            var color = entity.colorComponent.color;

            switch (collision.type)
            {
                case CollisionType.Circle:
                    var circle = collision as CircleCollision;
                    DrawCircleCollision(grap, circle, ref color);
                    break;
                case CollisionType.Rectangle:
                    var rectangle = collision as RectangleCollision;
                    DrawRectangleCollision(grap, rectangle, ref color);
                    break;
                case CollisionType.Capsule:
                    var capsule = collision as CapsuleCollision;
                    DrawCapsuleCollision(grap, capsule, ref color);
                    break;
                default:
                    break;
            }

            var box = collision.boundingBox;
            DrawBoundingBox(grap, ref box, ref color);
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
            var width = collision.width;
            var height = collision.height;
            var angle = collision.angle;

            DrawRectangle(grap, x, y, width, height, angle, brush);
        }

        private void DrawCapsuleCollision(Graphics grap, CapsuleCollision collision, ref Color color)
        {
            var brush = new SolidBrush(color);

            var x = collision.position.x;
            var y = collision.position.y;
            var length = collision.length;
            var radius = collision.radius;
            var angle = collision.angle;
            var points = GeometryHelper.GetCapsulePoints(x, y, length, angle);

            DrawRectangle(grap, x, y, length, radius * 2, angle, brush);
            DrawSemicircle(grap, points[0].x, points[0].y, radius, angle - 90, brush);
            DrawSemicircle(grap, points[1].x, points[1].y, radius, angle + 90, brush);
        }

        private void DrawRectangle(Graphics grap, float x, float y, float width, float height, float angle, Brush brush)
        {
            var verts = GeometryHelper.GetRectanglePoints(x, y, width, height, angle);

            var points = new PointF[]
            {
                new PointF(verts[0].x, verts[0].y),
                new PointF(verts[1].x, verts[1].y),
                new PointF(verts[2].x, verts[2].y),
                new PointF(verts[3].x, verts[3].y),
                new PointF(verts[0].x, verts[0].y),
            };

            grap.FillPolygon(brush, points);
        }

        private void DrawSemicircle(Graphics grap, float x, float y, float radius, float angle, Brush brush)
        {
            grap.FillPie(brush, x - radius, y - radius, radius * 2, radius * 2, angle, 180.0f);
        }

        private void DrawBoundingBox(Graphics grap, ref AABB box, ref Color color)
        {
            var pen = new Pen(color)
            {
                DashStyle = DashStyle.Dash
            };

            var x = box.minx;
            var y = box.miny;
            var w = box.maxx - box.minx;
            var h = box.maxy - box.miny;

            grap.DrawRectangle(pen, x, y, w, h);
        }

        private void DrawBorder(Graphics grap, ref Boundary L, ref Boundary R, ref Boundary T, ref Boundary B)
        {
            var pen = new Pen(Color.Black);

            grap.DrawLine(pen, new PointF(L.x,     L.y),     new PointF(L.x,                    L.y + canvas.Height - 2));
            grap.DrawLine(pen, new PointF(R.x - 1, R.y),     new PointF(R.x - 1,                R.y + canvas.Height - 2));
            grap.DrawLine(pen, new PointF(T.x,     T.y),     new PointF(T.x + canvas.Width - 1, T.y));
            grap.DrawLine(pen, new PointF(B.x,     B.y - 1), new PointF(B.x + canvas.Width - 1, B.y - 1));
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
