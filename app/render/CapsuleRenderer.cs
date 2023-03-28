using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX
{
    using SimpleX.Collision2D;
    using System.Text.RegularExpressions;

    class CapsuleRenderer : BaseRenderer
    {
        public Capsule geometry;

        public CapsuleRenderer(Capsule capsule)
        {
            geometry = capsule;
        }

        protected override void DrawCollision(Graphics grap, ref Transform transform)
        {
            var points = GeometryHelper.GetCapsulePoints(ref transform.position, geometry.length, transform.rotation);

            DrawRectangle(grap, transform.position.x, transform.position.y, geometry.length, geometry.radius * 2, transform.rotation);
            DrawSemicircle(grap, points[0].x, points[0].y, geometry.radius, transform.rotation);
            DrawSemicircle(grap, points[1].x, points[1].y, geometry.radius, transform.rotation);
        }

        protected override void OnDrawCollision(Graphics grap, ref Transform transform)
        {

        }

        private void DrawRectangle(Graphics grap, float x, float y, float width, float height, float rotation)
        {
            grap.TranslateTransform(x, y);
            grap.RotateTransform(rotation);
            grap.FillRectangle(brush, -width * 0.5f, -height * 0.5f, width, height);
            grap.ResetTransform();
        }

        private void DrawSemicircle(Graphics grap, float x, float y, float radius, float rotation)
        {
            grap.TranslateTransform(x, y);
            grap.RotateTransform(rotation);
            grap.FillEllipse(brush, -radius, -radius, radius * 2, radius * 2);
            grap.ResetTransform();
        }
    }
}
