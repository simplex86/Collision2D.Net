using System;

namespace SimpleX.Collision2D.Engine
{
    internal class RectangleCollision : BaseCollision
    {
        internal Rectangle geometry;

        internal override Vector position => (geometry.vertics[0] + geometry.vertics[2]) * 0.5f;
        internal override Vector[] points => geometry.vertics;

        public float width
        {
            get => geometry.width;
        }
        public float height
        {
            get => geometry.height;
        }
        public float angle
        {
            get => geometry.angle;
        }

        public RectangleCollision(Vector position, float width, float height, float angle)
            : base(CollisionType.Rectangle)
        {
            geometry = new Rectangle()
            {
                width = width,
                height = height,
                angle = angle,
                vertics = GeometryHelper.GetRectanglePoints(ref position, width, height, angle),
            };
        }

        // 旋转
        public override void Rotate(float delta)
        {
            geometry.angle += delta;
            dirty |= DirtyFlag.Rotation;
        }

        public override void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                if ((dirty & DirtyFlag.Rotation) == DirtyFlag.Rotation)
                {
                    var position = this.position;
                    geometry.vertics = GeometryHelper.GetRectanglePoints(ref position, width, height, angle);
                }

                var p1 = geometry.vertics[0];
                var p2 = geometry.vertics[1];
                var p3 = geometry.vertics[2];
                var p4 = geometry.vertics[3];

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
                return GeometryHelper.IsRectangleContains(ref geometry, ref pt);
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
