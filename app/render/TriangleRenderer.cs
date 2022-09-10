using System.Drawing;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    class TriangleRenderer : BaseRenderer
    {
        public override void DrawCollision(Graphics grap, BaseCollision collision, ref Color color)
        {
            brush.Color = color;
            DrawCollision(grap, collision as TriangleCollision);
        }

        private void DrawCollision(Graphics grap, TriangleCollision collision)
        {
            var vertics = collision.points;

            var points = new PointF[]
            {
                new PointF(vertics[0].x, vertics[0].y),
                new PointF(vertics[1].x, vertics[1].y),
                new PointF(vertics[2].x, vertics[2].y),
                new PointF(vertics[0].x, vertics[0].y),
            };
            grap.FillPolygon(brush, points);
        }
    }
}

