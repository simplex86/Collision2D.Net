using System;

namespace SimpleX.Collision2D
{
    internal class RectangleCollision : BaseCollision
    {
        internal Rectangle geometry;
        
        internal Vector2[] vertics { get; private set; } = null;

        public RectangleCollision(Vector2 position, float width, float height, float angle)
            : base(CollisionType.Rectangle)
        {
            geometry = new Rectangle()
            {
                width = width,
                height = height,
                angle = angle,
            };
            transform.position = position;

            vertics = GeometryHelper.GetRectanglePoints(ref position, width, height, angle);
        }

        public override void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                //if ((dirty & DirtyFlag.Rotation) == DirtyFlag.Rotation)
                //{
                    var position = transform.position;
                    vertics = GeometryHelper.GetRectanglePoints(ref position, geometry.width, geometry.height, transform.rotation);
                //}

                var p1 = vertics[0];
                var p2 = vertics[1];
                var p3 = vertics[2];
                var p4 = vertics[3];

                boundingBox.minx = MathX.Min(p1.x, p2.x, p3.x, p4.x);
                boundingBox.maxx = MathX.Max(p1.x, p2.x, p3.x, p4.x);
                boundingBox.miny = MathX.Min(p1.y, p2.y, p3.y, p4.y);
                boundingBox.maxy = MathX.Max(p1.y, p2.y, p3.y, p4.y);

                dirty = DirtyFlag.None;
            }
        }

        public override bool Contains(ref Vector2 pt)
        {
            if (IsAABBContains(ref pt))
            {
                return GeometryHelper.IsRectangleContains(ref geometry, ref transform, ref pt);
            }
            return false;
        }

        public override Vector2 GetFarthestProjectionPoint(ref Vector2 dir)
        {
            var point = vertics[0];
            var max = Vector2.Dot(ref point, ref dir);

            for (int i = 1; i < vertics.Length; i++)
            {
                var dot = Vector2.Dot(ref vertics[i], ref dir);
                if (dot > max)
                {
                    max = dot;
                    point = vertics[i];
                }
            }

            return point;
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
                    return CollisionHelper.Overlaps(collision as CapsuleCollision, this);
                case CollisionType.Polygon:
                    return CollisionHelper.Overlaps(collision as PolygonCollision, this);
                default:
                    break;
            }

            return false;
        }
    }
}
