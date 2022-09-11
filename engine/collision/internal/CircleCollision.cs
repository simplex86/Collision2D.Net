using System;

namespace SimpleX.Collision2D.Engine
{
    internal class CircleCollision : BaseCollision
    {
        internal Circle geometry;
        //
        internal override Vector position => geometry.center;
        //
        internal override Vector[] points => null;

        //半径
        public float radius => geometry.radius;

        public CircleCollision(Vector position, float radius)
            : base(CollisionType.Circle)
        {
            geometry = new Circle()
            {
                radius = radius,
            };
            geometry.center = position;
        }

        // 移动
        public override void Move(Vector delta)
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

        public override bool Contains(ref Vector pt)
        {
            if (IsAABBContains(ref pt))
            {
                return GeometryHelper.IsCircleContains(ref geometry, ref pt);
            }
            return false;
        }

        public override bool Overlays(BaseCollision collision)
        {
            switch (collision.type)
            {
                case CollisionType.Circle:
                    return CollisionHelper.Overlays(this, collision as CircleCollision);
                case CollisionType.Rectangle:
                    return CollisionHelper.Overlays(collision as RectangleCollision, this);
                case CollisionType.Capsule:
                    return CollisionHelper.Overlays(collision as CapsuleCollision, this);
                case CollisionType.Polygon:
                    return CollisionHelper.Overlays(collision as PolygonCollision, this);
                default:
                    break;
            }

            return false;
        }
    }
}
