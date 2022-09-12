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

                if (collision1.Overlaps(collision2))
                {
                    var direction = Vector.Normalize(collision1.position - collision2.position);

                    var dir1 = a.movementComponent.direction;
                    if (dir1.magnitude2 > 0) a.movementComponent.direction = direction;
                    var dir2 = b.movementComponent.direction;
                    if (dir2.magnitude2 > 0) b.movementComponent.direction = direction * -1;
                }
            });
        }
    }
}
