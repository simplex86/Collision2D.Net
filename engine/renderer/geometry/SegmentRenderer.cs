using System.Drawing;

namespace SimpleX.Collision2D
{
    internal class SegmentRenderer : GeometryRenderer<Segment>
    {
        public SegmentRenderer(Segment segment)
            : base(segment)
        {

        }

        public SegmentRenderer(Segment segment, Color color)
            : base(segment)
        {
            this.color = color;
        }

        protected override void DrawGeometry(Graphics grap)
        {
            float x = geometry.length * 0.5f;
            float y = 0f;

            grap.DrawLine(pen, -x, y, x, y);
        }
    }
}
