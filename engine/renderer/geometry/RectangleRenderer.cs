using System.Drawing;

namespace SimpleX.Collision2D
{
    internal class RectangleRenderer : GeometryRenderer<Rectangle>
    {
        public RectangleRenderer(Rectangle rectangle)
            : base(rectangle)
        {

        }

        public RectangleRenderer(Rectangle rectangle, Color color)
            : base(rectangle)
        {
            this.color = color;
        }

        protected override void DrawGeometry(Graphics grap)
        {
            grap.DrawRectangle(pen, -geometry.width * 0.5f, -geometry.height * 0.5f, geometry.width, geometry.height);
        }
    }
}
