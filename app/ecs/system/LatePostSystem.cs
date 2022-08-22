using System;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    // 在LateUpdate中最后被执行的System
    // 负责收尾工具，比如：纠正飞出碰撞场区的实体位置
    class LatePostSystem : BaseSystem
    {
        public LatePostSystem(World world)
            : base(world)
        {

        }

        public override void Tick(float dt)
        {
            world.Each((entity) =>
            {
                var collision = entity.collisionComponent.collision;
                CheckCollision(collision);
            });
        }

        private void CheckCollision(BaseCollision collision)
        {
            var position = collision.position;
            var bounding = collision.boundingBox;

            var w = bounding.width * 0.5f;
            var h = bounding.height * 0.5f;

            position.x = MathX.Clamp(position.x, world.left.x + w, world.right.x - w);
            position.y = MathX.Clamp(position.y, world.top.y + h, world.bottom.y - h);

            collision.position = position;
            collision.RefreshBoundingBox();
        }
    }
}
