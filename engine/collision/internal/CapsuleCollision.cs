using System;

namespace SimpleX.Collision2D.Engine
{
    internal class CapsuleCollision : BaseCollision
    {
        internal Capsule geometry;

        internal override Vector position => (geometry.points[0] + geometry.points[1]) * 0.5f;
        internal override Vector[] points => geometry.points;

        private Rectangle rectangle;

        public float length => geometry.length;
        public float radius => geometry.radius;
        public float angle => geometry.angle;

        public CapsuleCollision(Vector position, float length, float radius, float angle)
            : base(CollisionType.Capsule)
        {
            geometry = new Capsule()
            {
                length = length,
                radius = radius,
                angle = angle,
                points = GeometryHelper.GetCapsulePoints(ref position, length, angle),
            };
            rectangle = new Rectangle()
            {
                width = length + radius * 2,
                height = radius * 2,
                angle = angle,
                vertics = GeometryHelper.GetRectanglePoints(ref position, length + radius * 2, radius * 2, angle),
            };
        }

        public override void Move(Vector delta)
        {   
            for (int i=0; i< rectangle.vertics.Length; i++)
            {
                rectangle.vertics[i] += delta;
            }

            base.Move(delta);
        }

        // 旋转
        public override void Rotate(float delta)
        {
            geometry.angle += delta;
            rectangle.angle += delta;

            dirty |= DirtyFlag.Rotation;
        }

        public override void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                if ((dirty & DirtyFlag.Rotation) == DirtyFlag.Rotation)
                {
                    var position = this.position;
                    geometry.points = GeometryHelper.GetCapsulePoints(ref position, length, angle);
                    rectangle.vertics = GeometryHelper.GetRectanglePoints(ref position, rectangle.width, rectangle.height, angle);
                }

                var p1 = rectangle.vertics[0];
                var p2 = rectangle.vertics[1];
                var p3 = rectangle.vertics[2];
                var p4 = rectangle.vertics[3];

                boundingBox.minx = MathX.Min(p1.x, p2.x, p3.x, p4.x);
                boundingBox.maxx = MathX.Max(p1.x, p2.x, p3.x, p4.x);
                boundingBox.miny = MathX.Min(p1.y, p2.y, p3.y, p4.y);
                boundingBox.maxy = MathX.Max(p1.y, p2.y, p3.y, p4.y);

                dirty = DirtyFlag.None;
            }
        }

        public override bool Contains(ref Vector pt)
        {
            if (IsAABBContains(ref pt))
            {
                return GeometryHelper.IsCapsuleContains(ref geometry, ref pt);
            }
            return false;
        }

        public override bool Overlaps(BaseCollision collision)
        {
            switch (collision.type)
            {
                case CollisionType.Circle:
                    return CollisionHelper.Overlaps(this, collision as CircleCollision);
                case CollisionType.Rectangle:
                    return CollisionHelper.Overlaps(this, collision as RectangleCollision);
                case CollisionType.Capsule:
                    return CollisionHelper.Overlaps(this, collision as CapsuleCollision);
                case CollisionType.Polygon:
                    return CollisionHelper.Overlaps(collision as PolygonCollision, this);
                default:
                    break;
            }

            return false;
        }
    }
}
