using System.Drawing;

namespace SimpleX.Collision2D
{
    internal class CircleRenderer : GeometryRenderer<Circle>
    {
        public CircleRenderer(Circle circle)
            : base(circle)
        {

        }

        public CircleRenderer(Circle circle, Color color)
            : base(circle)
        {
            this.color = color;
        }

        protected override void DrawGeometry(Graphics grap)
        {
            grap.DrawEllipse(pen, -geometry.radius, -geometry.radius, geometry.radius * 2, geometry.radius * 2);
        }
    }
}
