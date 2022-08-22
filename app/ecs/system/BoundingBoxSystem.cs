using System;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    class BoundingBoxSystem : BaseSystem
    {
        public BoundingBoxSystem(World world)
            : base(world)
        {

        }

        public override void Tick(float dt)
        {
            world.Each((entity) =>
            {
                var collision = entity.collisionComponent.collision;
                collision.RefreshBoundingBox();
            });
        }
    }
}
