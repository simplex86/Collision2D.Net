using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    abstract class BaseRenderer
    {
        protected Pen pen = new Pen(Color.Red);
        protected SolidBrush brush = new SolidBrush(Color.Red);

        public bool showBoundingBox = false;

        protected BaseRenderer()
        {
            pen.DashStyle = DashStyle.Dash;
        }

        public void Render(Graphics grap, BaseCollision collision, ref Color color)
        {
            DrawCollision(grap, collision, ref color);
            if (showBoundingBox)
            {
                DrawBoundingBox(grap, ref collision.boundingBox, ref color);
            }
        }

        public abstract void DrawCollision(Graphics grap, BaseCollision collision, ref Color color);

        // 画包围盒
        protected void DrawBoundingBox(Graphics grap, ref AABB box, ref Color color)
        {
            pen.Color = color;

            var x = box.minx;
            var y = box.miny;
            var w = box.maxx - box.minx;
            var h = box.maxy - box.miny;

            grap.DrawRectangle(pen, x, y, w, h);
        }

        // 画矩形
        protected void DrawRectangle(Graphics grap, Vector[] vertics)
        {
            var points = new PointF[]
            {
                new PointF(vertics[0].x, vertics[0].y),
                new PointF(vertics[1].x, vertics[1].y),
                new PointF(vertics[2].x, vertics[2].y),
                new PointF(vertics[3].x, vertics[3].y),
                new PointF(vertics[0].x, vertics[0].y),
            };

            grap.FillPolygon(brush, points);
        }
    }
}
