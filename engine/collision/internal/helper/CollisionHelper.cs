using System;

namespace SimpleX.Collision2D
{
    internal static class CollisionHelper
    {
        // 两个圆是否碰撞
        public static bool Overlaps(CircleCollision circle1, CircleCollision circle2)
        {
            if (IsAABBOverlaps(circle1, circle2))
            {
                //return GeometryHelper.IsCircleOverlapsWithCircle(ref circle1.geometry, ref circle1.transform, ref circle2.geometry, ref circle2.transform);
                return GJK.Overlaps(ref circle1.transform.position, circle1.geometry.radius, 
                                    ref circle2.transform.position, circle2.geometry.radius);
            }
            return false;
        }

        // 两个矩形是否碰撞
        public static bool Overlaps(RectangleCollision rectangle1, RectangleCollision rectangle2)
        {
            if (IsAABBOverlaps(rectangle1, rectangle2))
            {
                //return GeometryHelper.IsRectangleOverlapsWithRectangle(ref a.geometry, ref b.geometry);
                return GJK.Overlaps(ref rectangle1.transform.position, rectangle1.vertics, 
                                    ref rectangle2.transform.position, rectangle2.vertics);
            }
            return false;
        }

        // 矩形和圆是否碰撞
        public static bool Overlaps(RectangleCollision rectangle, CircleCollision circle)
        {
            if (IsAABBOverlaps(circle, rectangle))
            {
                //return GeometryHelper.IsCircleOverlapsWithRectangle(ref circle.geometry, ref circle.transform, ref rectangle.geometry, ref rectangle.transform);
                return GJK.Overlaps(ref rectangle.transform.position, rectangle.vertics, 
                                    ref circle.transform.position, circle.geometry.radius);
            }
            return false;
        }

        // 两个胶囊是否碰撞
        public static bool Overlaps(CapsuleCollision capsule1, CapsuleCollision capsule2)
        {
            if (IsAABBOverlaps(capsule1, capsule2))
            {
                return GJK.Overlaps(ref capsule1.transform.position, capsule1.geometry.length, capsule1.geometry.radius, capsule1.transform.rotation, 
                                    ref capsule2.transform.position, capsule2.geometry.length, capsule2.geometry.radius, capsule2.transform.rotation);
            }
            return false;
        }

        // 胶囊和圆是否碰撞
        public static bool Overlaps(CapsuleCollision capsule, CircleCollision circle)
        {
            if (IsAABBOverlaps(capsule, circle))
            {
                return GJK.Overlaps(ref capsule.transform.position, capsule.geometry.length, capsule.geometry.radius, capsule.transform.rotation,
                                    ref circle.transform.position, circle.geometry.radius);
            }
            return false;
        }

        // 胶囊和矩形是否碰撞
        public static bool Overlaps(CapsuleCollision capsule, RectangleCollision rectangle)
        {
            if (IsAABBOverlaps(capsule, rectangle))
            {
                return GJK.Overlaps(ref rectangle.transform.position, rectangle.vertics, 
                                    ref capsule.transform.position, capsule.geometry.length, capsule.geometry.radius, capsule.transform.rotation);
            }
            return false;
        }

        // 两个多边形是否碰撞
        public static bool Overlaps(PolygonCollision polygon1, PolygonCollision polygon2)
        {
            if (IsAABBOverlaps(polygon1, polygon2))
            {
                return GJK.Overlaps(ref polygon1.transform.position, polygon1.vertics, 
                                    ref polygon2.transform.position, polygon2.vertics);
            }
            return false;
        }

        // 多边形和圆形是否碰撞
        public static bool Overlaps(PolygonCollision polygon, CircleCollision circle)
        {
            if (IsAABBOverlaps(polygon, circle))
            {
                //return GeometryHelper.IsPolygonOverlapsWithCircle(ref polygon.geometry, ref circle.geometry);
                return GJK.Overlaps(ref polygon.transform.position, polygon.vertics, 
                                    ref circle.transform.position, circle.geometry.radius);
            }
            return false;
        }

        // 多边形和矩形是否碰撞
        public static bool Overlaps(PolygonCollision polygon, RectangleCollision rectangle)
        {
            if (IsAABBOverlaps(polygon, rectangle))
            {
                return GJK.Overlaps(ref polygon.transform.position, polygon.vertics, 
                                    ref rectangle.transform.position, rectangle.vertics);
            }
            return false;
        }

        // 多边形和胶囊是否碰撞
        public static bool Overlaps(PolygonCollision polygon, CapsuleCollision capsule)
        {
            if (IsAABBOverlaps(polygon, capsule))
            {
                return GJK.Overlaps(ref polygon.transform.position, polygon.vertics, 
                                    ref capsule.transform.position, capsule.geometry.length, capsule.geometry.radius, capsule.transform.rotation);
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
