using System.Drawing;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class CapsuleRenderer : BaseRenderer
    {
        public Capsule geometry;

        public CapsuleRenderer(Capsule capsule)
        {
            geometry = capsule;
        }

        protected override void DrawGeometry(Graphics grap, Transform transform)
        {
            var r = geometry.radius;
            var w = geometry.length * 0.5f;
            var h = geometry.radius;

            grap.DrawLine(bodyPen, -w, -h, w, -h);
            grap.DrawLine(bodyPen, -w,  h, w,  h);

            grap.DrawArc(bodyPen,   w - r,  -r, r * 2, r * 2, -90, 180);
            grap.DrawArc(bodyPen, -(w + r), -r, r * 2, r * 2,  90, 180);
        }
    }
}
