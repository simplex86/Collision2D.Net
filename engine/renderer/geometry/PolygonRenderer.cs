using System.Drawing;

namespace SimpleX.Collision2D
{
    internal class PolygonRenderer : GeometryRenderer<Polygon>
    {
        public PolygonRenderer(Polygon polygon)
            : base(polygon)
        {

        }

        public PolygonRenderer(Polygon polygon, Color color)
            : base(polygon)
        {
            this.color = color;
        }

        protected override void DrawGeometry(Graphics grap)
        {
            var n = geometry.vertics.Length;

            var vertics = new PointF[n + 1];
            for (int i = 0; i <= n; i++)
            {
                var pt = geometry.vertics[i % n];
                vertics[i] = new PointF(pt.x, pt.y);
            }
            
            grap.DrawPolygon(pen, vertics);
        }
    }
}

