using System;

namespace SimpleX
{
    using SimpleX.Collision2D;

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
                var collider1 = a.collisionComponent.collider;
                var collider2 = b.collisionComponent.collider;

                if (collider1.Overlaps(collider2))
                {
                    var direction = Vector2.Normalize(collider1.transform.position - collider2.transform.position);
                    a.movementComponent.direction =  direction;
                    b.movementComponent.direction = -direction;
                }
            });
        }
    }
}
