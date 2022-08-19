using System;

namespace SimpleX.Collision2D.Engine
{
    public class CircleCollision : BaseCollision
    {
        public float radius;

        public CircleCollision(ref Vector position, float radius)
            : base(position)
        {
            this.radius = radius;
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
