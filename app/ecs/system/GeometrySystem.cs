using System;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class GeometrySystem : BaseSystem
    {
        public GeometrySystem(World world)
            : base(world)
        {

        }

        public override void Tick(float dt)
        {
            world.Each((entity) =>
            {
                var collision = entity.collisionComponent.collision;
                collision.RefreshGeometry();
            });
        }
    }
}
