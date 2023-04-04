using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX.Collision2D
{
    class VelocityRenderer : IRenderer
    {
        // 
        public Vector2 direction { get; set; } = Vector2.zero;
        // 
        public float magnitude { get; set; } = 0f;
        // 颜色
        public Color color { get; set; } = Color.Black;

        // 速度的线头
        protected static AdjustableArrowCap cap = new AdjustableArrowCap(4, 4, true)
        {
            Filled = true,
            MiddleInset = 1.5f, //设置箭头中间的缩进
        };
        // 速度的画笔
        protected static Pen pen = new Pen(Color.FromArgb(128, Color.Black))
        {
            CustomStartCap = new AdjustableArrowCap(1, 1, true),
            CustomEndCap = cap,
        };

        public VelocityRenderer()
        {
            
        }

        // 绘制图形
        public void Render(Graphics grap, Transform transform)
        {
            if (magnitude > 0)
            {
                var position = transform.position;
                var velocity = direction * magnitude;

                var sp = position;
                var ep = position + (velocity * 0.4f);

                pen.Color = Color.FromArgb(128, color);
                grap.DrawLine(pen, sp.x, sp.y, ep.x, ep.y);
            }
        }
    }
}
