using System.Drawing;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    class RectangleRenderer : BaseRenderer
    {
        public override void DrawCollision(Graphics grap, BaseCollision collision, ref Color color)
        {
            brush.Color = color;
            DrawCollision(grap, collision as RectangleCollision);
        }

        private void DrawCollision(Graphics grap, RectangleCollision collision)
        {
            var verts = collision.points;
            if (verts == null) return;

            var points = new PointF[]
            {
                new PointF(verts[0].x, verts[0].y),
                new PointF(verts[1].x, verts[1].y),
                new PointF(verts[2].x, verts[2].y),
                new PointF(verts[3].x, verts[3].y),
                new PointF(verts[0].x, verts[0].y),
            };

            grap.FillPolygon(brush, points);
        }
    }
}
