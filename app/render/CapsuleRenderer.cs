using System.Drawing;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class CapsuleRenderer : BaseRenderer
    {
        public override void DrawCollision(Graphics grap, BaseCollision collision, ref Color color)
        {
            brush.Color = color;
            DrawCollision(grap, collision as CapsuleCollision);
        }

        private void DrawCollision(Graphics grap, CapsuleCollision collision)
        {
            var position = collision.transform.position;
            var vertics = GeometryHelper.GetRectanglePoints(ref position, collision.geometry.length, collision.geometry.radius * 2, collision.transform.rotation);
            DrawRectangle(grap, vertics);

            var points = GeometryHelper.GetCapsulePoints(ref position, collision.geometry.length, collision.transform.rotation);
            DrawSemicircle(grap, points[0].x, points[0].y, collision.geometry.radius, brush);
            DrawSemicircle(grap, points[1].x, points[1].y, collision.geometry.radius, brush);
        }

        private void DrawSemicircle(Graphics grap, float x, float y, float radius, Brush brush)
        {
            grap.FillEllipse(brush, x - radius, y - radius, radius * 2, radius * 2);
        }
    }
}
