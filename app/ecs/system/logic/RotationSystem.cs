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
                var speed = entity.rotationComponent.speed;
                if (!MathX.Equals(speed, 0))
                {
                    var delta = speed * dt;
                    entity.transformComponent.transform.rotation += delta;
                }
            });
        }
    }
}
