using System;

namespace SimpleX.Collision2D.Engine
{
    public class CircleCollision : BaseCollision
    {
        public float radius;

        public CircleCollision(ref Vector position, float radius)
            : base(CollisionType.Circle, position)
        {
            this.radius = radius;
        }

        public override void RefreshGeometry()
        {
            boundingBox.minx = position.x - radius;
            boundingBox.maxx = position.x + radius;
            boundingBox.miny = position.y - radius;
            boundingBox.maxy = position.y + radius;
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
