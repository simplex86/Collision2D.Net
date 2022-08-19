using System;

namespace SimpleX.Collision2D.Engine
{
    public class RectangleCollision : BaseCollision
    {
        public float width;
        public float height;
        public float angle;

        public RectangleCollision(ref Vector position, float width, float height, float angle)
            : base(position)
        {
            this.width = width;
            this.height = height;
            this.angle = angle;
        }

        public override bool Contains(ref Vector pt)
        {
            return CollisionHelper.Contains(this, ref pt);
        }

        public override bool Collides(BaseCollision collision)
        {
            if (collision is CircleCollision)
            {
                var other = collision as CircleCollision;
                return CollisionHelper.Collides(this, other);
            }

            if (collision is RectangleCollision)
            {
                var other = collision as RectangleCollision;
                return CollisionHelper.Collides(this, other);
            }

            return false;
        }
    }
}
