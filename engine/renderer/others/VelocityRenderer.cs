using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX.Collision2D
{
    class VelocityRenderer : IRenderer
    {
        // 方向
        public Velocity velocity { get; set; }
        // 颜色
        public Color color
        {
            set { pen.Color = Color.FromArgb(128, value); }
            get { return pen.Color; }
        }

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
            if (velocity.magnitude > 0)
            {
                grap.TranslateTransform(transform.position.x, transform.position.y);
                {
                    var p = velocity.direction * velocity.magnitude * 0.4f;
                    grap.DrawLine(pen, 0, 0, p.x, p.y);
                }
                grap.ResetTransform();
            }
        }
    }
}
