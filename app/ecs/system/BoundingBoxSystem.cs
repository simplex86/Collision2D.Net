using System;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    class BoundingBoxSystem : BaseSystem
    {
        public BoundingBoxSystem(World world)
            : base(world)
        {

        }

        public override void Tick(float dt)
        {
            world.Each((entity) =>
            {
                var collision = entity.collisionComponent.collision;
                collision.RefreshBoundingBox();

                CheckPosition(collision);
            });
        }

        private void CheckPosition(BaseCollision collision)
        {
            var position = collision.position;
            var bounding = collision.boundingBox;

            var w = bounding.width * 0.5f;
            var h = bounding.height * 0.5f;

            position.x = MathX.Clamp(position.x, world.left.x + w, world.right.x - w);
            position.y = MathX.Clamp(position.y, world.top.y + h, world.bottom.y - h);
        }
    }
}
