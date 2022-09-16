using System;

namespace SimpleX.Collision2D
{
    internal class CircleCollision : BaseCollision
    {
        internal Circle geometry;
        //
        internal override Vector2 position => geometry.center;
        //
        internal override Vector2[] points => null;

        //半径
        public float radius => geometry.radius;

        public CircleCollision(Vector2 position, float radius)
            : base(CollisionType.Circle)
        {
            geometry = new Circle()
            {
                radius = radius,
            };
            geometry.center = position;
        }

        // 移动
        public override void Move(ref Vector2 delta)
        {
            geometry.center += delta;
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

        public override bool Contains(ref Vector2 pt)
        {
            if (IsAABBContains(ref pt))
            {
                return GeometryHelper.IsCircleContains(ref geometry, ref pt);
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
