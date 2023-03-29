using System.Drawing;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class CircleRenderer : BaseRenderer
    {
        public Circle geometry;

        public CircleRenderer(Circle circle)
        {
            geometry = circle;
        }

        protected override void OnDrawGeometry(Graphics grap, ref Transform transform)
        {
            grap.DrawEllipse(bodyPen, -geometry.radius, -geometry.radius, geometry.radius * 2, geometry.radius * 2);
        }
    }
}
