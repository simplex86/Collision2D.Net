using SimpleX.Collision2D;
using System.Drawing;

namespace SimpleX
{
    class GeometryRenderSystem : RenderSystem
    {
        public GeometryRenderSystem(World world)
            : base(world)
        {
            
        }

        public override void Render(Graphics grap)
        {
            world.Each((entity) =>
            {
                var transform = entity.transformComponent.transform;
                var geometryRenderer = entity.geometryRendererComponent.renderer;
                geometryRenderer.color = entity.colorComponent.color;

                geometryRenderer.Render(grap, transform);
            });
        }
    }
}
