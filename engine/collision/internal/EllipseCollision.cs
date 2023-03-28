using System;

namespace SimpleX.Collision2D
{
    internal class EllipseCollision : BaseCollision
    {
        internal Ellipse geometry;

        private static Vector2[] X =
        {
            Vector2.up,
            Vector2.right,
            Vector2.down,
            Vector2.left,
        };

        public EllipseCollision(Vector2 position, Ellipse ellipse, float rotation)
            : base(CollisionType.Ellipse)
        {
            geometry = ellipse;
            transform.position = position;
            transform.rotation = rotation;
        }

        public override void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                //if ((dirty & DirtyFlag.Rotation) == DirtyFlag.Rotation)
                //{
                //    var vertics = GeometryHelper.GetRectanglePoints(ref transform.position, geometry.width, geometry.height, transform.rotation);
                //}
                
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
                return GeometryHelper.IsEllipseContains(ref geometry, ref transform, ref pt);
            }
            return false;
        }

        public override bool Overlaps(BaseCollision collision)
        {
            //switch (collision.type)
            //{
            //    case CollisionType.Circle:
            //        return CollisionHelper.Overlaps(this, collision as CircleCollision);
            //    case CollisionType.Rectangle:
            //        return CollisionHelper.Overlaps(this, collision as RectangleCollision);
            //    case CollisionType.Capsule:
            //        return CollisionHelper.Overlaps(collision as CapsuleCollision, this);
            //    case CollisionType.Polygon:
            //        return CollisionHelper.Overlaps(collision as PolygonCollision, this);
            //    default:
            //        break;
            //}

            return false;
        }

        public override Vector2 GetFarthestProjectionPoint(ref Vector2 dir)
        {
            if (MathX.Equals(dir.x, 0.0f))
            {
                var sign = dir.x < 0.0f ? -1 : 1;
                return new Vector2(0.0f, sign * geometry.B);
            }
            if (MathX.Equals(dir.y, 0.0f))
            {
                var sign = dir.y < 0.0f ? -1 : 1;
                return new Vector2(sign * geometry.A, 0.0f);
            }

            var k = dir.y / dir.x;

            var a2 = MathX.Pow2(geometry.A);
            var b2 = MathX.Pow2(geometry.B);
            var k2 = MathX.Pow2(k);

            var d = MathX.Sqrt((a2 + b2 * k2) / k2);
            var v = new Vector2(0.0f, d);
            if (Vector2.Dot(ref v, ref dir) < 0)
            {
                d *= -1;
            }

            var x = k * d - (b2 * k2 * k * d) / (a2 + b2 * k2);
            var y = (b2 * k2 * d) / (a2 + b2 * k2);

            return transform.position + new Vector2(x, y);
        }
    }
}
