using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX
{
    using SimpleX.Collision2D;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    class CapsuleRenderer : BaseRenderer
    {
        public Capsule geometry;

        public CapsuleRenderer(Capsule capsule)
        {
            geometry = capsule;
        }

        protected override void DrawGeometry(Graphics grap, ref Transform transform)
        {
            var points = GeometryHelper.GetCapsulePoints(geometry.length, transform.rotation);

            DrawRectangle(grap, transform.position.x, transform.position.y, geometry.length, geometry.radius * 2, transform.rotation);
            DrawSemicircle1(grap, transform.position.x + points[0].x, transform.position.y + points[0].y, geometry.radius, transform.rotation);
            DrawSemicircle2(grap, transform.position.x + points[1].x, transform.position.y + points[1].y, geometry.radius, transform.rotation);
        }

        protected override void OnDrawGeometry(Graphics grap, ref Transform transform)
        {

        }

        private void DrawRectangle(Graphics grap, float x, float y, float width, float height, float rotation)
        {
            var w = width * 0.5f;
            var h = height * 0.5f;
            var x1 = -w;
            var y1 = -h;
            var x2 = w;
            var y2 = h;

            grap.TranslateTransform(x, y);
            grap.RotateTransform(rotation);
            grap.DrawLine(bodyPen, x1, y1, x2, y1);
            grap.DrawLine(bodyPen, x1, y2, x2, y2);
            grap.ResetTransform();
        }

        private void DrawSemicircle1(Graphics grap, float x, float y, float radius, float rotation)
        {
            grap.TranslateTransform(x, y);
            grap.RotateTransform(rotation);
            grap.DrawArc(bodyPen, -radius, -radius, radius * 2, radius * 2, -90, 180);
            grap.ResetTransform();
        }

        private void DrawSemicircle2(Graphics grap, float x, float y, float radius, float rotation)
        {
            grap.TranslateTransform(x, y);
            grap.RotateTransform(rotation);
            grap.DrawArc(bodyPen, -radius, -radius, radius * 2, radius * 2, 270, -180);
            grap.ResetTransform();
        }
    }
}
