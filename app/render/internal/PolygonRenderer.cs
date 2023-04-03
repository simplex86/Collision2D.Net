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

        protected override void DrawGeometry(Graphics grap, Transform transform)
        {
            var n = geometry.vertics.Length;

            var vertics = new PointF[n + 1];
            for (int i = 0; i <= n; i++)
            {
                var pt = geometry.vertics[i % n];
                vertics[i] = new PointF(pt.x, pt.y);
            }
            
            grap.DrawPolygon(bodyPen, vertics);
        }
    }
}

