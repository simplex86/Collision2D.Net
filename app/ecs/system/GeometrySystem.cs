using System;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

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

                return true;
            });
        }
    }
}
