using System.Drawing;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class EllipseRenderer : BaseRenderer
    {
        public Ellipse geometry;

        public EllipseRenderer(Ellipse ellipse)
        {
            geometry = ellipse;
        }

        protected override void OnDrawCollision(Graphics grap, ref Transform transform)
        {
            grap.FillEllipse(brush, -geometry.width * 0.5f, -geometry.height * 0.5f, geometry.width, geometry.height);
        }
    }
}
