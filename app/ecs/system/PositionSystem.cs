using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D.App
{
    class PositionSystem : BaseSystem
    {
        public PositionSystem(World world)
            : base(world)
        {

        }

        public override void Tick(float dt)
        {
            world.Each((entity) =>
            {
                var collisionComponent = entity.collisionComponent;
                var movementComponent = entity.movementComponent;

                var direction = movementComponent.direction;
                var speed = movementComponent.speed;

                var position = collisionComponent.collision.position;
                var delta = direction * speed * dt;

                collisionComponent.collision.position = position + delta;
            });
        }
    }
}
