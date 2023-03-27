using System.Drawing;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class EllipseRenderer : BaseRenderer
    {
        public override void DrawCollision(Graphics grap, BaseCollision collision, ref Color color)
        {
            brush.Color = color;
            DrawCollision(grap, collision as EllipseCollision);
        }

        private void DrawCollision(Graphics grap, EllipseCollision collision)
        {
            var p = collision.transform.position;

            grap.TranslateTransform(p.x, p.y);
            grap.RotateTransform(collision.angle);
            grap.FillEllipse(brush, 0, 0, collision.width, collision.height);
            grap.ResetTransform();
        }
    }
}
