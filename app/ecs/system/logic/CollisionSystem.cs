using System;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class CollisionSystem : LogicSystem
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
                    var transform1 = a.transformComponent.transform;
                    var transform2 = b.transformComponent.transform;

                    var direction = Vector2.Normalize(transform1.position - transform2.position);
                    a.velocityComponent.velocity.direction =  direction;
                    b.velocityComponent.velocity.direction = -direction;
                }
            });
        }
    }
}
