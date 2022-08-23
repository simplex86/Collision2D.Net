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
                if (collision.type == CollisionType.Circle) return true; ;

                switch (collision.type)
                {
                    case CollisionType.Rectangle:
                        RotateRectangle(entity, collision as RectangleCollision, dt);
                        break;
                    case CollisionType.Capsule:
                        RotateCapsule(entity, collision as CapsuleCollision, dt);
                        break;
                    default:
                        break;
                }

                return true;
            });
        }

        private void RotateRectangle(Entity entity, RectangleCollision rectangle, float dt)
        {
            var angle = rectangle.angle;
            var speed = entity.rotationComponent.speed;
            var delta = speed * dt;

            rectangle.angle = angle + delta;
        }

        private void RotateCapsule(Entity entity, CapsuleCollision capsule, float dt)
        {
            var angle = capsule.angle;
            var speed = entity.rotationComponent.speed;
            var delta = speed * dt;

            capsule.angle = angle + delta;
        }
    }
}
