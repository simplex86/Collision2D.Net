﻿namespace SimpleX
{
    using SimpleX.Collision2D;

    // 在LateUpdate中最后被执行的System
    // 负责收尾工作，比如：纠正飞出碰撞场区的实体位置
    class LatePostSystem : LogicSystem
    {
        public LatePostSystem(World world)
            : base(world)
        {

        }

        public override void Tick(float dt)
        {
            world.Each((entity) =>
            {
                var position = entity.transformComponent.transform.position;
                var bounding = entity.collisionComponent.collider.boundingBox;

                var w1 = position.x    - bounding.minx;
                var w2 = bounding.maxx - position.x;
                var h1 = position.y    - bounding.miny;
                var h2 = bounding.maxy - position.y;

                var dx = MathX.Clamp(position.x, world.left.x + w1, world.right.x  - w2);
                var dy = MathX.Clamp(position.y, world.top.y  + h1, world.bottom.y - h2);

                entity.transformComponent.transform.position = new Vector2(dx, dy);
            });
        }
    }
}
