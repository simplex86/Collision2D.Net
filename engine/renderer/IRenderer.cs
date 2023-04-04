using System.Drawing;

namespace SimpleX.Collision2D
{
    public interface IRenderer
    {
        // 颜色
        Color color { get; set; }

        // 绘制图形
        void Render(Graphics grap, Transform transform);
    }
}
