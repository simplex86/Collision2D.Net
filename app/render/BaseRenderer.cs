using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX
{
    using SimpleX.Collision2D;

    abstract class BaseRenderer
    {
        protected Pen pen = new Pen(Color.Red);
        protected SolidBrush brush = new SolidBrush(Color.Red);

        // 方向的线头
        protected static AdjustableArrowCap directionCap = new AdjustableArrowCap(6, 6, true)   	
        {
            Filled = true,
            MiddleInset = 1.5f, //设置箭头中间的缩进
        };
        // 方向的画笔
        protected static Pen directionPen = new Pen(Color.Black)
        {
            CustomStartCap = new AdjustableArrowCap(1, 1, true),
            CustomEndCap = directionCap,
        };
        // 质点画刷
        protected static SolidBrush pivotBrush = new SolidBrush(Color.Brown);

        public bool showBoundingBox = false;
        public bool showDirection = false;

        protected BaseRenderer()
        {
            pen.DashStyle = DashStyle.Dash;
        }

        public void Render(Graphics grap, ref Transform transform, ref AABB boundingBox, ref Vector2 direction, ref Color color)
        {
            brush.Color = color;

            DrawCollision(grap, ref transform);
            if (showBoundingBox)
            {
                DrawBoundingBox(grap, ref boundingBox, ref color);
            }
            if (showDirection)
            {
                var position = transform.position;
                DrawDirection(grap, ref position, ref direction);
            }
            DrawPivot(grap, ref transform.position);
        }

        protected virtual void DrawCollision(Graphics grap, ref Transform transform)
        {
            PrevDrawCollision(grap, ref transform);
            {
                OnDrawCollision(grap, ref transform);
            }
            PostDrawCollision(grap);
        }

        protected abstract void OnDrawCollision(Graphics grap, ref Transform transform);

        private void PrevDrawCollision(Graphics grap, ref Transform transform)
        {
            grap.TranslateTransform(transform.position.x, transform.position.y);
            grap.RotateTransform(transform.rotation);
        }

        private void PostDrawCollision(Graphics grap)
        {
            grap.ResetTransform();
        }

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
                var sp = position;
                var ep = position + (dir * 0.8f);

                grap.DrawLine(directionPen, sp.x, sp.y, ep.x, ep.y);
            }
        }

        protected void DrawPivot(Graphics grap, ref Vector2 position)
        {
            grap.FillEllipse(pivotBrush, position.x - 1.5f, position.y - 1.5f, 3, 3);
        }
    }
}
