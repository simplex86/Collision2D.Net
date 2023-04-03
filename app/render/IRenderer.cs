using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX
{
    using SimpleX.Collision2D;

    public interface IRenderer
    {
        // 颜色
        Color color { get; set; }
        // 是否显示包围盒
        bool boundingBox { get; set; }
        // 是否显示速度
        bool velocity { get; set; }

        // 绘制图形
        void Render(Graphics grap, Transform transform, AABB boundingBox, Vector2 velocity);
    }
}
