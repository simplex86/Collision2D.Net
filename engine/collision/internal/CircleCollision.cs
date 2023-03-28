using System;

namespace SimpleX.Collision2D
{
    internal class CircleCollision : BaseCollision
    {
        internal Circle geometry;

        public CircleCollision(Vector2 position, Circle circle)
            : base(CollisionType.Circle)
        {
            geometry = circle;
            transform.position = position;
        }

        public override void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                boundingBox.minx = transform.position.x - geometry.radius;
                boundingBox.maxx = transform.position.x + geometry.radius;
                boundingBox.miny = transform.position.y - geometry.radius;
                boundingBox.maxy = transform.position.y + geometry.radius;

                dirty = DirtyFlag.None;
            }
        }

        public override bool Contains(ref Vector2 pt)
        {
            if (IsAABBContains(ref pt))
            {
                return GeometryHelper.IsCircleContains(ref geometry, ref transform, ref pt);
            }
            return false;
        }

        public override Vector2 GetFarthestProjectionPoint(ref Vector2 dir)
        {
            return transform.position + geometry.radius * dir.normalized;
        }

        public override bool Overlaps(BaseCollision collision)
        {
            switch (collision.type)
            {
                case CollisionType.Circle:
                    return CollisionHelper.Overlaps(this, collision as CircleCollision);
                case CollisionType.Rectangle:
                    return CollisionHelper.Overlaps(collision as RectangleCollision, this);
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
