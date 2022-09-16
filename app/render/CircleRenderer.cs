using System.Drawing;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class CircleRenderer : BaseRenderer
    {
        public override void DrawCollision(Graphics grap, BaseCollision collision, ref Color color)
        {
            brush.Color = color;
            DrawCollision(grap, collision as CircleCollision);
        }

        private void DrawCollision(Graphics grap, CircleCollision collision)
        {
            var x = collision.position.x - collision.radius;
            var y = collision.position.y - collision.radius;
            var w = collision.radius * 2;
            var h = collision.radius * 2;

            grap.FillEllipse(brush, x, y, w, h);
        }
    }
}
