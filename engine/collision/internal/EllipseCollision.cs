using System;

namespace SimpleX.Collision2D
{
    internal class EllipseCollision : BaseCollision
    {
        internal Ellipse geometry;

        //internal override Vector2 position => (geometry.vertics[0] + geometry.vertics[2]) * 0.5f;
        //internal override Vector2[] vertics => null;// geometry.vertics;

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

        public EllipseCollision(Vector2 position, float width, float height, float angle)
            : base(CollisionType.Ellipse)
        {
            geometry = new Ellipse()
            {
                width = width,
                height = height,
                angle = angle,
                //vertics = GeometryHelper.GetRectanglePoints(ref position, width, height, angle),
            };
            transform.position = position;
        }

        public override void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                //if ((dirty & DirtyFlag.Rotation) == DirtyFlag.Rotation)
                //{
                    var position = transform.position;
                    var vertics = GeometryHelper.GetRectanglePoints(ref position, width, height, angle);
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
                return GeometryHelper.IsEllipseContains(ref geometry, ref pt);
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

            return new Vector2(x, y);
        }
    }
}
