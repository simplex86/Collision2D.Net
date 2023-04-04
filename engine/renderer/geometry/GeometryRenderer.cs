using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX.Collision2D
{
    internal abstract class GeometryRenderer<T> : IRenderer where T : IGeometry
    {
        protected Pen pen = new Pen(Color.Red);
        // 质点画刷
        protected static SolidBrush pivotBrush = new SolidBrush(Color.Brown);

        // 图形
        public T geometry { get; }
        // 颜色
        public Color color { get; set; } = Color.Red;

        protected GeometryRenderer(T geometry)
        {
            this.geometry = geometry;

            pen.Width = 1.0f;
            pen.DashStyle = DashStyle.Solid;
        }

        // 画图形
        public void Render(Graphics grap, Transform transform)
        {
            pen.Color = color;

            PrevDrawGeometry(grap, transform);
            {
                DrawGeometry(grap);
            }
            PostDrawGeometry(grap);

            DrawPivot(grap, transform.position);
        }

        private void PrevDrawGeometry(Graphics grap, Transform transform)
        {
            grap.TranslateTransform(transform.position.x, transform.position.y);
            grap.RotateTransform(transform.rotation);
        }

        protected abstract void DrawGeometry(Graphics grap);

        private void PostDrawGeometry(Graphics grap)
        {
            grap.ResetTransform();
        }

        // 画质点
        protected void DrawPivot(Graphics grap, Vector2 position)
        {
            grap.FillEllipse(pivotBrush, position.x - 1.5f, position.y - 1.5f, 3, 3);
        }
    }
}
