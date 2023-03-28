using System.Drawing;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class PolygonRenderer : BaseRenderer
    {
        public Polygon geometry;

        public PolygonRenderer(Polygon polygon)
        {
            geometry = polygon;
        }

        protected override void OnDrawCollision(Graphics grap, ref Transform transform)
        {
            var length = geometry.vertics.Length;

            var vertics = new PointF[length + 1];
            for (int i = 0; i <= length; i++)
            {
                var pt = geometry.vertics[i % length];
                vertics[i] = new PointF(pt.x, pt.y);
            }
            
            grap.FillPolygon(brush, vertics);
        }
    }
}

