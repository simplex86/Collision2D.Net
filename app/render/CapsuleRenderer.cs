using System.Drawing;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    class CapsuleRenderer : BaseRenderer
    {
        public override void DrawCollision(Graphics grap, BaseCollision collision, ref Color color)
        {
            brush.Color = color;
            DrawCollision(grap, collision as CapsuleCollision);
        }

        private void DrawCollision(Graphics grap, CapsuleCollision collision)
        {
            var position = collision.position;
            var vertics = GeometryHelper.GetRectanglePoints(ref position, collision.length, collision.radius * 2, collision.angle);
            DrawRectangle(grap, vertics);

            var points = collision.points;
            DrawSemicircle(grap, points[0].x, points[0].y, collision.radius, brush);
            DrawSemicircle(grap, points[1].x, points[1].y, collision.radius, brush);
        }

        private void DrawSemicircle(Graphics grap, float x, float y, float radius, Brush brush)
        {
            grap.FillEllipse(brush, x - radius, y - radius, radius * 2, radius * 2);
        }
    }
}
