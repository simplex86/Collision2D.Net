using SimpleX.Collision2D;
using System.Drawing;

namespace SimpleX
{
    internal class BoundingRenderSystem : RenderSystem
    {
        public BoundingRenderSystem(World world)
            : base(world)
        {

        }

        public override void Render(Graphics grap)
        {
            if (!Settings.showBoundingBox) return;

            world.Each((entity) =>
            {
                var renderer = entity.boundingRendererComponent.renderer as AABBRenderer;
                renderer.boundingBox = entity.collisionComponent.collider.boundingBox;
                renderer.color = entity.colorComponent.color;

                renderer.Render(grap, entity.transformComponent.transform);
            });
        }
    }
}
