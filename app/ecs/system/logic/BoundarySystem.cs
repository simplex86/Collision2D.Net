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
                var collisionComponent = entity.collisionComponent;
                var velocityComponent = entity.velocityComponent;

                var aabb = collisionComponent.collider.boundingBox;
                var direction = velocityComponent.direction;

                if (aabb.minx <= world.left.x)
                {
                    velocityComponent.direction = Vector2.Reflect(direction, world.left.normal);
                }
                else if (aabb.maxx >= world.right.x)
                {
                    velocityComponent.direction = Vector2.Reflect(direction, world.right.normal);
                }
                else if (aabb.miny <= world.top.y)
                {
                    velocityComponent.direction = Vector2.Reflect(direction, world.top.normal);
                }
                else if(aabb.maxy >= world.bottom.y)
                {
                    velocityComponent.direction = Vector2.Reflect(direction, world.bottom.normal);
                }
            });
        }
    }
}
