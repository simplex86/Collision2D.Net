using System;

namespace SimpleX.Collision2D
{
    internal static class CollisionHelper
    {
        // 两个圆是否碰撞
        public static bool Overlaps(CircleCollision a, CircleCollision b)
        {
            if (IsAABBOverlaps(a, b))
            {
                return GeometryHelper.IsCircleOverlapsWithCircle(ref a.geometry, ref b.geometry);
            }
            return false;
        }

        // 两个矩形是否碰撞
        public static bool Overlaps(RectangleCollision a, RectangleCollision b)
        {
            if (IsAABBOverlaps(a, b))
            {
                return GeometryHelper.IsRectangleOverlapsWithRectangle(ref a.geometry, ref b.geometry);
            }
            return false;
        }

        // 矩形和圆是否碰撞
        public static bool Overlaps(RectangleCollision r, CircleCollision c)
        {
            if (IsAABBOverlaps(c, r))
            {
                return GeometryHelper.IsCircleOverlapsWithRectangle(ref c.geometry, ref r.geometry);
            }
            return false;
        }

        // 两个胶囊是否碰撞
        public static bool Overlaps(CapsuleCollision a, CapsuleCollision b)
        {
            if (IsAABBOverlaps(a, b))
            {
                var p1 = a.position;
                var p2 = b.position;
                return GJK.Overlaps(ref p1, a.length, a.radius, a.angle, ref p2, b.length, b.radius, b.angle);
            }
            return false;
        }

        // 胶囊和圆是否碰撞
        public static bool Overlaps(CapsuleCollision a, CircleCollision b)
        {
            if (IsAABBOverlaps(a, b))
            {
                var capsule = new Capsule()
                {
                    length = a.length,
                    radius = a.radius + b.radius,
                    points = new Vector2[] { a.points[0], a.points[1] },
                };
                var position = b.position;
                return GeometryHelper.IsCapsuleContains(ref capsule, ref position);
            }
            return false;
        }

        // 胶囊和矩形是否碰撞
        public static bool Overlaps(CapsuleCollision a, RectangleCollision b)
        {
            if (IsAABBOverlaps(a, b))
            {
                var p1 = b.position;
                var p2 = a.position;

                return GJK.Overlaps(ref p1, b.points, ref p2, a.length, a.radius, a.angle);
            }
            return false;
        }

        // 两个多边形是否碰撞
        public static bool Overlaps(PolygonCollision a, PolygonCollision b)
        {
            if (IsAABBOverlaps(a, b))
            {
                var p1 = a.position;
                var p2 = b.position;
                return GJK.Overlaps(ref p1, a.points, ref p2, b.points);
            }
            return false;
        }

        // 多边形和圆形是否碰撞
        public static bool Overlaps(PolygonCollision a, CircleCollision b)
        {
            if (IsAABBOverlaps(a, b))
            {
                //return GeometryHelper.IsPolygonOverlapsWithCircle(ref a.geometry, ref b.geometry);
                var p1 = a.position;
                var p2 = b.position;
                return GJK.Overlaps(ref p1, a.points, ref p2, b.radius);
            }
            return false;
        }

        // 多边形和矩形是否碰撞
        public static bool Overlaps(PolygonCollision a, RectangleCollision b)
        {
            if (IsAABBOverlaps(a, b))
            {
                var p1 = a.position;
                var p2 = b.position;
                return GJK.Overlaps(ref p1, a.points, ref p2, b.points);
            }
            return false;
        }

        // 多边形和胶囊是否碰撞
        public static bool Overlaps(PolygonCollision a, CapsuleCollision b)
        {
            if (IsAABBOverlaps(a, b))
            {
                var p1 = a.position;
                var p2 = b.position;

                return GJK.Overlaps(ref p1, a.points, ref p2, b.length, b.radius, b.angle);
            }
            return false;
        }

        // AABB是否重叠
        private static bool IsAABBOverlaps(BaseCollision a, BaseCollision b)
        {
            var abb = a.boundingBox;
            var bbb = b.boundingBox;

            if (abb.minx > bbb.maxx || abb.maxx < bbb.minx) return false;
            if (abb.miny > bbb.maxy || abb.maxy < bbb.miny) return false;

            return true;
        }
    }
}
