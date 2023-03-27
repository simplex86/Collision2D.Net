using System.Drawing;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class CircleRenderer : BaseRenderer
    {
        public override void DrawCollision(Graphics grap, BaseCollision collision, ref Color color)
        {
            brush.Color = color;

            var geometry = (collision as CircleCollision).geometry;
            var transform = collision.transform;
            DrawCollision(grap, ref geometry, ref transform);
        }

        private void DrawCollision(Graphics grap, ref Circle circle, ref Transform transform)
        {
            var position = transform.position;
            grap.FillEllipse(brush, position.x - circle.radius, position.y - circle.radius, circle.radius * 2, circle.radius * 2);
        }
    }
}
