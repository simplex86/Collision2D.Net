using System.Drawing;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    class PolygonRenderer : BaseRenderer
    {
        public override void DrawCollision(Graphics grap, BaseCollision collision, ref Color color)
        {
            brush.Color = color;
            DrawCollision(grap, collision as PolygonCollision);
        }

        private void DrawCollision(Graphics grap, PolygonCollision collision)
        {
            var vertics = collision.points;
            var n = vertics.Length;

            var points = new PointF[n + 1];
            for (int i=0; i< points.Length; i++)
            {
                var v = vertics[i % n];
                points[i] = new PointF(v.x, v.y);
            }

            grap.FillPolygon(brush, points);
        }
    }
}

