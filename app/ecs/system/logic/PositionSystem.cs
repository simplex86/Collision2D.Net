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
                var velocityComponent = entity.velocityComponent;
                var magnitude = velocityComponent.magnitude;

                if (!MathX.Equals(magnitude, 0))
                {
                    var direction = velocityComponent.direction;
                    var delta = direction * (magnitude * dt);

                    entity.transformComponent.transform.position += delta;
                }
            });
        }
    }
}
