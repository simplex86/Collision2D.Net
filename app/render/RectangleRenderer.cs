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
            var x = collision.position.x;
            var y = collision.position.y;
            var width = collision.width;
            var height = collision.height;
            var angle = collision.angle;

            DrawRectangle(grap, x, y, width, height, angle, brush);
        }
    }
}
