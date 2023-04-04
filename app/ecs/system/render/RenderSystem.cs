using SimpleX.Collision2D;
using System.Drawing;

namespace SimpleX
{
    abstract class RenderSystem
    {
        protected World world { get; private set; } = null;

        protected RenderSystem(World world)
        {
            this.world = world;
        }

        public abstract void Render(Graphics grap);
    }
}
