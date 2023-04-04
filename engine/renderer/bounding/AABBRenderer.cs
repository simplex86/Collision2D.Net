using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX.Collision2D
{
    internal class AABBRenderer : IRenderer
    {
        // 
        public AABB boundingBox { get; set; }
        // 颜色
        public Color color { get; set; } = Color.Red;

        protected Pen pen = new Pen(Color.Red);

        public AABBRenderer() 
        {
            pen.Width = 1.0f;
            pen.DashStyle = DashStyle.Dash;
        }

        // 绘制图形
        public void Render(Graphics grap, Transform transform)
        {
            pen.Color = Color.FromArgb(80, color);

            var x = boundingBox.minx;
            var y = boundingBox.miny;
            var w = boundingBox.maxx - boundingBox.minx;
            var h = boundingBox.maxy - boundingBox.miny;

            grap.DrawRectangle(pen, x, y, w, h);
        }
    }
}
