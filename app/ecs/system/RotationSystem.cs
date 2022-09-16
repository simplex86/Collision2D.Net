using System;
using System.Collections.Generic;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class RotationSystem : BaseSystem
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

                    var collision = entity.collisionComponent.collision;
                    collision.Rotate(delta);
                }
            });
        }
    }
}
