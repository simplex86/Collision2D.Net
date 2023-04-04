using System.Drawing;

namespace SimpleX.Collision2D
{
    internal class EllipseRenderer : GeometryRenderer<Ellipse>
    {
        public EllipseRenderer(Ellipse ellipse)
            : base(ellipse)
        {

        }
        public EllipseRenderer(Ellipse ellipse, Color color)
            : base(ellipse)
        {
            this.color = color;
        }

        protected override void DrawGeometry(Graphics grap)
        {
            grap.DrawEllipse(pen, -geometry.width * 0.5f, -geometry.height * 0.5f, geometry.width, geometry.height);
        }
    }
}
