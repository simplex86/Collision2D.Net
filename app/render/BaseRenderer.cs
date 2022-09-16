using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX
{
    using SimpleX.Collision2D;

    abstract class BaseRenderer
    {
        protected Pen pen = new Pen(Color.Red);
        protected SolidBrush brush = new SolidBrush(Color.Red);

        public bool showBoundingBox = false;
        public bool showDirection = false;

        protected BaseRenderer()
        {
            pen.DashStyle = DashStyle.Dash;
        }

        public void Render(Graphics grap, BaseCollision collision, ref Vector2 diretion, ref Color color)
        {
            DrawCollision(grap, collision, ref color);
            if (showBoundingBox)
            {
                DrawBoundingBox(grap, ref collision.boundingBox, ref color);
            }
            if (showDirection)
            {
                var position = collision.position;
                DrawDirection(grap, ref position, ref diretion);
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

        // 画方向
        protected void DrawDirection(Graphics grap, ref Vector2 position, ref Vector2 dir)
        {
            if (dir.magnitude2 > 0)
            {
                var cap = new AdjustableArrowCap(6, 6, true)   //设置一个线头	
                {
                    Filled = true,
                    MiddleInset = 1.5f, //设置箭头中间的缩进
                };
                var pen = new Pen(Color.Black)
                {
                    CustomStartCap = new AdjustableArrowCap(1, 1, true),
                    CustomEndCap = cap,
                };

                var sp = position;
                var ep = position + (dir * 0.8f);

                grap.DrawLine(pen, sp.x, sp.y, ep.x, ep.y);
            }
        }

        // 画矩形
        protected void DrawRectangle(Graphics grap, Vector2[] vertics)
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
