using System;
using System.Collections.Generic;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class RotationSystem : LogicSystem
    {
        public RotationSystem(World world)
            : base(world)
        {
            
        }

        public override void Tick(float dt)
        {
            world.Each((entity) =>
            {
                var magnitude = entity.rotationComponent.magnitude;
                if (!MathX.Equals(magnitude, 0))
                {
                    var delta = magnitude * dt;
                    entity.transformComponent.transform.Rotate(delta);
                }
            });
        }
    }
}
