using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D.App
{
    class PositionSystem
    {
        private World world = null;

        public PositionSystem(World world)
        {
            this.world = world;
        }

        public void Tick(float dt)
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
