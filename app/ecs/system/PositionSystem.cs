using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

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
                var movementComponent = entity.movementComponent;
                var speed = movementComponent.speed;

                if (!MathX.Equals(speed, 0))
                {
                    var direction = movementComponent.direction;
                    var delta = direction * (speed * dt);

                    entity.collisionComponent.collision.Move(delta);
                }
            });
        }
    }
}
