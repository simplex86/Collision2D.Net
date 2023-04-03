using System.Drawing;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class PieRenderer : BaseRenderer
    {
        public Pie geometry;

        public PieRenderer(Pie pie)
        {
            geometry = pie;
        }

        protected override void DrawGeometry(Graphics grap, Transform transform)
        {
            var p = -geometry.radius;
            var s =  geometry.radius * 2;
            var a = -geometry.sweep * 0.5f;

            grap.DrawPie(bodyPen, p, p, s, s, a, geometry.sweep);
        }
    }
}
