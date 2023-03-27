using System.Drawing;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class RectangleRenderer : BaseRenderer
    {
        public override void DrawCollision(Graphics grap, BaseCollision collision, ref Color color)
        {
            brush.Color = color;

            var geometry = (collision as RectangleCollision).geometry;
            var transform = collision.transform;
            DrawRectangle(grap, ref geometry, ref transform);
        }

        private void DrawRectangle(Graphics grap, ref Rectangle rectangle, ref Transform transform)
        {
            var vertics = GeometryHelper.GetRectanglePoints(ref transform.position, rectangle.width, rectangle.height, transform.rotation);
            DrawRectangle(grap, vertics);
        }
    }
}
