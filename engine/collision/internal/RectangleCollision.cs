using System;

namespace SimpleX.Collision2D.Engine
{
    internal class RectangleCollision : BaseCollision
    {
        public float width;
        public float height;
        public float angle;

        public RectangleCollision(float width, float height, float angle)
            : base(CollisionType.Rectangle)
        {
            this.width = width;
            this.height = height;
            this.angle = angle;
        }

        public override void RefreshGeometry()
        {
            if (dirty)
            {
                points = GeometryHelper.GetRectanglePoints(ref position, width, height, angle);

                var p1 = points[0];
                var p2 = points[1];
                var p3 = points[2];
                var p4 = points[3];

                boundingBox.minx = MathX.Min(p1.x, p2.x, p3.x, p4.x);
                boundingBox.maxx = MathX.Max(p1.x, p2.x, p3.x, p4.x);
                boundingBox.miny = MathX.Min(p1.y, p2.y, p3.y, p4.y);
                boundingBox.maxy = MathX.Max(p1.y, p2.y, p3.y, p4.y);

                dirty = false;
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
