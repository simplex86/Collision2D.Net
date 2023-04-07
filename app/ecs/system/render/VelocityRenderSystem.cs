using System.Drawing;

namespace SimpleX
{
    using SimpleX.Collision2D;

    internal class VelocityRenderSystem : RenderSystem
    {
        public VelocityRenderSystem(World world)
            : base(world)
        {
            
        }

        public override void Render(Graphics grap)
        {
            if (!Settings.showVelocity) return;

            world.Each((entity) =>
            {
                var renderer = entity.velocityRendererComponent.renderer as VelocityRenderer;
                renderer.velocity = entity.velocityComponent.velocity;

                var transform = entity.transformComponent.transform;
                renderer.Render(grap, transform);
            });
        }
    }
}
