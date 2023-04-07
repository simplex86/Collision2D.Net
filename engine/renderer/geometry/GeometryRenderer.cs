using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX.Collision2D
{
    internal abstract class GeometryRenderer<T> : IRenderer where T : IGeometry
    {
        // 图形
        public T geometry { get; }
        // 颜色
        public Color color
        {
            set { pen.Color = value; }
            get { return pen.Color; }
        }

        protected Pen pen = new Pen(Color.Red);
        // 质点画刷
        protected static SolidBrush pivotBrush = new SolidBrush(Color.Brown);


        protected GeometryRenderer(T geometry)
        {
            this.geometry = geometry;

            pen.Width = 1.0f;
            pen.DashStyle = DashStyle.Solid;
        }

        // 画图形
        public void Render(Graphics grap, Transform transform)
        {
            PrevProcess(grap, transform);
            {
                DrawGeometry(grap);
                DrawPivot(grap);
            }
            PostProcess(grap);
        }

        private void PrevProcess(Graphics grap, Transform transform)
        {
            grap.TranslateTransform(transform.position.x, transform.position.y);
            grap.RotateTransform(transform.rotation);
        }

        protected abstract void DrawGeometry(Graphics grap);

        // 画质点
        private void DrawPivot(Graphics grap)
        {
            grap.FillEllipse(pivotBrush, -1.5f, -1.5f, 3, 3);
        }

        private void PostProcess(Graphics grap)
        {
            grap.ResetTransform();
        }
    }
}
