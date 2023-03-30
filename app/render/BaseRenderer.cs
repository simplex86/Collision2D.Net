using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX
{
    using SimpleX.Collision2D;

    abstract class BaseRenderer
    {
        protected Pen bodyPen = new Pen(Color.Red);
        protected Pen bboxPen = new Pen(Color.Red);

        // 速度的线头
        protected static AdjustableArrowCap velocityCap = new AdjustableArrowCap(6, 6, true)   	
        {
            Filled = true,
            MiddleInset = 1.5f, //设置箭头中间的缩进
        };
        // 速度的画笔
        protected static Pen velocityPen = new Pen(Color.FromArgb(128, 0, 0, 0))
        {
            CustomStartCap = new AdjustableArrowCap(1, 1, true),
            CustomEndCap = velocityCap,
        };
        // 质点画刷
        protected static SolidBrush pivotBrush = new SolidBrush(Color.Brown);

        public bool showBoundingBox = false;
        public bool showDirection = false;

        protected BaseRenderer()
        {
            bodyPen.Width = 1.0f;
            bodyPen.DashStyle = DashStyle.Solid;

            bboxPen.Width = 1.0f;
            bboxPen.DashStyle = DashStyle.Dash;
        }

        // 画图形
        public void Render(Graphics grap, Transform transform, AABB boundingBox, Vector2 direction, Color color)
        {
            bodyPen.Color = color;
            bboxPen.Color = Color.FromArgb(80, color);

            PrevDrawGeometry(grap, transform);
            {
                DrawGeometry(grap, transform);
            }
            PostDrawGeometry(grap);

            if (showBoundingBox)
            {
                DrawBoundingBox(grap, boundingBox);
            }
            if (showDirection)
            {
                var position = transform.position;
                DrawDirection(grap, position, direction);
            }
            DrawPivot(grap, transform.position);
        }

        private void PrevDrawGeometry(Graphics grap, Transform transform)
        {
            grap.TranslateTransform(transform.position.x, transform.position.y);
            grap.RotateTransform(transform.rotation);
        }

        protected abstract void DrawGeometry(Graphics grap, Transform transform);

        private void PostDrawGeometry(Graphics grap)
        {
            grap.ResetTransform();
        }

        // 画包围盒
        protected void DrawBoundingBox(Graphics grap, AABB box)
        {
            var x = box.minx;
            var y = box.miny;
            var w = box.maxx - box.minx;
            var h = box.maxy - box.miny;

            grap.DrawRectangle(bboxPen, x, y, w, h);
        }

        // 画方向
        protected void DrawDirection(Graphics grap, Vector2 position, Vector2 dir)
        {
            if (dir.magnitude2 > 0)
            {
                var sp = position;
                var ep = position + (dir * 0.8f);

                grap.DrawLine(velocityPen, sp.x, sp.y, ep.x, ep.y);
            }
        }

        // 画质点
        protected void DrawPivot(Graphics grap, Vector2 position)
        {
            grap.FillEllipse(pivotBrush, position.x - 1.5f, position.y - 1.5f, 3, 3);
        }
    }
}
