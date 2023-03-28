using System.Drawing;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class RectangleRenderer : BaseRenderer
    {
        public Rectangle geometry;

        public RectangleRenderer(Rectangle rectangle)
        {
            geometry = rectangle;
        }

        protected override void OnDrawCollision(Graphics grap, ref Transform transform)
        {
            grap.FillRectangle(brush, -geometry.width * 0.5f, -geometry.height * 0.5f, geometry.width, geometry.height);
        }
    }
}
