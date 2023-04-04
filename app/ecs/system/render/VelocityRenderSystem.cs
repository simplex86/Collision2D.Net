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
                renderer.direction = entity.velocityComponent.direction;
                renderer.magnitude = entity.velocityComponent.magnitude;

                var transform = entity.transformComponent.transform;
                renderer.Render(grap, transform);
            });
        }
    }
}
