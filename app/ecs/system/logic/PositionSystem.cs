using System;
using System.Collections.Generic;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class PositionSystem : LogicSystem
    {
        public PositionSystem(World world)
            : base(world)
        {

        }

        public override void Tick(float dt)
        {
            world.Each((entity) =>
            {
                var velocity = entity.velocityComponent.velocity;

                if (!MathX.Equals(velocity.magnitude, 0))
                {
                    var delta = velocity.direction * (velocity.magnitude * dt);
                    entity.transformComponent.transform.Move(delta);
                }
            });
        }
    }
}
