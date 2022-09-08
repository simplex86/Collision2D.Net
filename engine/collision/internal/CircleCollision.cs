using System;

namespace SimpleX.Collision2D.Engine
{
    internal class CircleCollision : BaseCollision
    {
        //半径
        public float radius;

        public CircleCollision(Vector position, float radius)
            : base(CollisionType.Circle)
        {
            this.position = position;
            this.radius = radius;
        }

        // 移动
        public override void Move(Vector delta)
        {
            position += delta;
            dirty |= DirtyFlag.Position;
        }

        // 旋转
        public override void Rotate(float delta)
        {

        }

        public override void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                boundingBox.minx = position.x - radius;
                boundingBox.maxx = position.x + radius;
                boundingBox.miny = position.y - radius;
                boundingBox.maxy = position.y + radius;

                dirty = DirtyFlag.None;
            }
        }

        public override bool Contains(ref Vector pt)
        {
            return CollisionHelper.Contains(this, ref pt);
        }

        public override bool Collides(BaseCollision collision)
        {
            switch (collision.type)
            {
                case CollisionType.Circle:
                    return CollisionHelper.Collides(this, collision as CircleCollision);
                case CollisionType.Rectangle:
                    return CollisionHelper.Collides(this, collision as RectangleCollision);
                case CollisionType.Capsule:
                    return CollisionHelper.Collides(this, collision as CapsuleCollision);
                default:
                    break;
            }

            return false;
        }
    }
}
