using System;

namespace SimpleX
{
    using SimpleX.Collision2D;

    class BoundarySystem : BaseSystem
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
                var movementComponent = entity.movementComponent;

                var aabb = collisionComponent.collision.boundingBox;
                var direction = movementComponent.direction;

                if (aabb.minx <= world.left.x)
                {
                    movementComponent.direction = Vector2.Reflect(ref direction, ref world.left.normal);
                }
                else if (aabb.maxx >= world.right.x)
                {
                    movementComponent.direction = Vector2.Reflect(ref direction, ref world.right.normal);
                }
                else if (aabb.miny <= world.top.y)
                {
                    movementComponent.direction = Vector2.Reflect(ref direction, ref world.top.normal);
                }
                else if(aabb.maxy >= world.bottom.y)
                {
                    movementComponent.direction = Vector2.Reflect(ref direction, ref world.bottom.normal);
                }
            });
        }
    }
}
