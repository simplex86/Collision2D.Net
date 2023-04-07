using System;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class BoundarySystem : LogicSystem
    {
        public BoundarySystem(World world)
            : base(world)
        {

        }

        public override void Tick(float dt)
        {
            world.Each((entity) =>
            {
                var transformComponent = entity.transformComponent;
                var collisionComponent = entity.collisionComponent;
                var velocityComponent = entity.velocityComponent;

                var position = transformComponent.transform.position;
                var bounding = collisionComponent.collider.boundingBox;
                var direction = velocityComponent.velocity.direction;

                if (position.x + bounding.minx <= world.left.x)
                {
                    if (Vector2.Dot(direction, world.left.normal) < 0)
                    {
                        velocityComponent.velocity.direction = Vector2.Reflect(direction, world.left.normal);
                    }
                }
                else if (position.x + bounding.maxx >= world.right.x)
                {
                    if (Vector2.Dot(direction, world.right.normal) < 0)
                    {
                        velocityComponent.velocity.direction = Vector2.Reflect(direction, world.right.normal);
                    }
                }
                else if (position.y + bounding.miny <= world.top.y)
                {
                    if (Vector2.Dot(direction, world.top.normal) < 0)
                    {
                        velocityComponent.velocity.direction = Vector2.Reflect(direction, world.top.normal);
                    }
                }
                else if(position.y + bounding.maxy >= world.bottom.y)
                {
                    if (Vector2.Dot(direction, world.bottom.normal) < 0)
                    {
                        velocityComponent.velocity.direction = Vector2.Reflect(direction, world.bottom.normal);
                    }
                }
            });
        }
    }
}
