using System.Drawing;

namespace SimpleX.Collision2D
{
    internal class CapsuleRenderer : GeometryRenderer<Capsule>
    {
        public CapsuleRenderer(Capsule capsule)
            : base(capsule)
        {

        }

        public CapsuleRenderer(Capsule capsule, Color color)
            : base(capsule)
        {
            this.color = color;
        }

        protected override void DrawGeometry(Graphics grap)
        {
            var r = geometry.radius;
            var w = geometry.length * 0.5f;
            var h = geometry.radius;

            grap.DrawLine(pen, -w, -h, w, -h);
            grap.DrawLine(pen, -w,  h, w,  h);

            grap.DrawArc(pen,   w - r,  -r, r * 2, r * 2, -90, 180);
            grap.DrawArc(pen, -(w + r), -r, r * 2, r * 2,  90, 180);
        }
    }
}
