using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

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
                var collision = entity.collisionComponent.collision;
                if (collision == null || collision is CircleCollision) return;

                if (collision is RectangleCollision)
                {
                    var rectangle = collision as RectangleCollision;

                    var angle = rectangle.angle;
                    var speed = entity.rotationComponent.speed;
                    var delta = speed * dt;

                    rectangle.angle = angle + delta;
                }
            });
        }
    }
}
