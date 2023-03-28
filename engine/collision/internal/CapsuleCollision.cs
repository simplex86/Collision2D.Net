using System;

namespace SimpleX.Collision2D
{
    internal class CapsuleCollision : BaseCollision
    {
        internal Capsule geometry;

        private static Vector2[] X =
        {
            Vector2.up,
            Vector2.right,
            Vector2.down,
            Vector2.left,
        };

        public CapsuleCollision(Vector2 position, Capsule capsule, float rotation)
            : base(CollisionType.Capsule)
        {
            geometry = capsule;
            transform.position = position;
            transform.rotation = rotation;
        }

        public override void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                var p1 = GetFarthestProjectionPoint(ref X[0]);
                var p2 = GetFarthestProjectionPoint(ref X[1]);
                var p3 = GetFarthestProjectionPoint(ref X[2]);
                var p4 = GetFarthestProjectionPoint(ref X[3]);

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
                return GeometryHelper.IsCapsuleContains(ref geometry, ref transform, ref pt);
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

        public override Vector2 GetFarthestProjectionPoint(ref Vector2 dir)
        {
            var m1 = Matrix.CreateRotationMatrix(-transform.rotation * MathX.DEG2RAD);
            var d1 = Matrix.Transform(ref dir, ref m1);

            var p1 = geometry.radius * d1.normalized;
            var dx = d1.x >= 0 ? geometry.length * 0.5f : -geometry.length * 0.5f;
            p1.x += dx;

            var m2 = Matrix.CreateRotationMatrix(transform.rotation * MathX.DEG2RAD);
            var p2 = Matrix.Transform(ref p1, ref m2);

            return p2 + transform.position;
        }
    }
}
