using System;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    class CollisionSystem : BaseSystem
    {
        public CollisionSystem(World world)
            : base(world)
        {

        }

        public override void Tick(float dt)
        {
            world.Each2((a, b) =>
            {
                var collision1 = a.collisionComponent.collision;
                var collision2 = b.collisionComponent.collision;

                if (collision1.Overlays(collision2))
                {
                    var direction = Vector.Normalize(collision1.position - collision2.position);
                    a.movementComponent.direction = direction;
                    b.movementComponent.direction = direction * -1;
                }
            });
        }
    }
}
