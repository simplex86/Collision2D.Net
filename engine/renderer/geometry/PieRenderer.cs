using System.Drawing;

namespace SimpleX.Collision2D
{
    internal class PieRenderer : GeometryRenderer<Pie>
    {
        public PieRenderer(Pie pie)
            : base(pie)
        {

        }

        public PieRenderer(Pie pie, Color color)
            : base(pie)
        {
            this.color = color;
        }

        protected override void DrawGeometry(Graphics grap)
        {
            var p = -geometry.radius;
            var s =  geometry.radius * 2;
            var a = -geometry.sweep * 0.5f;

            grap.DrawPie(pen, p, p, s, s, a, geometry.sweep);
        }
    }
}
