using System;

namespace SimpleX.Collision2D.Engine
{
    public class CapsuleCollision : BaseCollision
    {
        public float length = 20.0f;
        public float radius = 10.0f;
        public float angle = 0.0f;
        
        public CapsuleCollision(ref Vector position, float length, float radius, float angle)
            : base(CollisionType.Capsule, position)
        {
            this.length = length;
            this.radius = radius;
            this.angle = angle;
        }

        public override void RefreshBoundingBox()
        {
            float w = length + radius * 2;
            float h = radius * 2;
            var points = GeometryHelper.GetRectanglePoints(ref position, w, h, angle);

            var p1 = points[0];
            var p2 = points[1];
            var p3 = points[2];
            var p4 = points[3];

            boundingBox.minx = MathX.Min(p1.x, p2.x, p3.x, p4.x);
            boundingBox.maxx = MathX.Max(p1.x, p2.x, p3.x, p4.x);
            boundingBox.miny = MathX.Min(p1.y, p2.y, p3.y, p4.y);
            boundingBox.maxy = MathX.Max(p1.y, p2.y, p3.y, p4.y);
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
