using System;

namespace SimpleX.Collision2D.Engine
{
    internal static class CollisionHelper
    {
        // 两个圆是否碰撞
        public static bool Overlays(CircleCollision a, CircleCollision b)
        {
            if (!IsAABBOverlays(a, b)) return false;
            return GeometryHelper.IsCircleOverlayWithCircle(ref a.geometry, ref b.geometry);
        }

        // 两个矩形是否碰撞
        public static bool Overlays(RectangleCollision a, RectangleCollision b)
        {
            if (IsAABBOverlays(a, b))
            {
                return GeometryHelper.IsRectangleOverlayWithRectangle(ref a.geometry, ref b.geometry);
            }
            return false;
        }

        // 矩形和圆是否碰撞
        public static bool Overlays(RectangleCollision r, CircleCollision c)
        {
            if (IsAABBOverlays(c, r))
            {
                return GeometryHelper.IsCircleOverlayWithRectangle(ref c.geometry, ref r.geometry);
            }
            return false;
        }

        // 两个胶囊是否碰撞
        public static bool Overlays(CapsuleCollision a, CapsuleCollision b)
        {
            if (IsAABBOverlays(a, b))
            {
                var pas = a.points;
                var pbs = b.points;

                var dr = a.radius + b.radius;
                if (GeometryHelper.GetDistance2(ref pas[0], ref pas[1], ref pbs[0]) <= dr * dr) return true;
                if (GeometryHelper.GetDistance2(ref pas[0], ref pas[1], ref pbs[1]) <= dr * dr) return true;
                if (GeometryHelper.IsSegmentIntersected(ref pas[0], ref pas[1], ref pbs[0], ref pbs[1])) return true;
            }
            return false;
        }

        // 胶囊和圆是否碰撞
        public static bool Overlays(CapsuleCollision a, CircleCollision b)
        {
            if (IsAABBOverlays(a, b))
            {
                var capsule = new Capsule()
                {
                    length = a.length,
                    radius = a.radius + b.radius,
                    points = new Vector[] { a.points[0], a.points[1] },
                };
                var position = b.position;
                return GeometryHelper.IsCapsuleContains(ref capsule, ref position);
            }
            return false;
        }

        // 胶囊和矩形是否碰撞
        public static bool Overlays(CapsuleCollision a, RectangleCollision b)
        {
            if (IsAABBOverlays(a, b))
            {
                var circle = new Circle()
                {
                    center = a.points[0],
                    radius = a.radius,
                };
                if (GeometryHelper.IsCircleOverlayWithRectangle(ref circle, ref b.geometry)) return true;

                circle = new Circle()
                {
                    center = a.points[1],
                    radius = a.radius,
                };
                if (GeometryHelper.IsCircleOverlayWithRectangle(ref circle, ref b.geometry)) return true;

                var position = (a.points[0] + a.points[1]) * 0.5f;
                var rectangle = new Rectangle()
                {
                    width = a.length,
                    height = a.radius * 2,
                    angle = a.angle,
                    vertics = GeometryHelper.GetRectanglePoints(ref position, a.length, a.radius * 2, a.angle),
                };
                if (GeometryHelper.IsRectangleOverlayWithRectangle(ref rectangle, ref b.geometry)) return true;
            }
            return false;
        }

        // 两个三角形是否碰撞
        public static bool Overlays(PolygonCollision a, PolygonCollision b)
        {
            if (IsAABBOverlays(a, b))
            {
                var p1 = a.position;
                var p2 = b.position;
                return GJK.Overlays(ref p1, a.points, ref p2, b.points);
            }
            return false;
        }

        // 三角形和圆形是否碰撞
        public static bool Overlays(PolygonCollision a, CircleCollision b)
        {
            if (IsAABBOverlays(a, b))
            {
                //var p1 = a.position;
                //var p2 = b.position;
                //return GJK.Overlays(ref p1, a.points, ref p2, b.radius);

                return GeometryHelper.IsPolygonOverlayWithCircle(ref a.geometry, ref b.geometry);
            }
            return false;
        }

        // 三角形和矩形是否碰撞
        public static bool Overlays(PolygonCollision a, RectangleCollision b)
        {
            if (IsAABBOverlays(a, b))
            {
                var p1 = a.position;
                var p2 = b.position;
                return GJK.Overlays(ref p1, a.points, ref p2, b.points);
            }
            return false;
        }

        // 三角形和胶囊是否碰撞
        public static bool Overlays(PolygonCollision a, CapsuleCollision b)
        {
            if (IsAABBOverlays(a, b))
            {
                var polygon = a.geometry;

                var c1 = new Circle()
                {
                    center = b.points[0],
                    radius = b.radius,
                };
                if (GeometryHelper.IsPolygonOverlayWithCircle(ref polygon, ref c1)) return true;

                var c2 = new Circle()
                {
                    center = b.points[1],
                    radius = b.radius,
                };
                if (GeometryHelper.IsPolygonOverlayWithCircle(ref polygon, ref c2)) return true;
                
                var p1 = b.position;
                var v1 = GeometryHelper.GetRectanglePoints(ref p1, b.length, b.radius * 2, b.angle);
                var v2 = polygon.vertics;
                var p2 = (v2[0] + v2[1] + v2[2]) / 3.0f;

                return GJK.Overlays(ref p1, v1, ref p2, v2);
            }
            return false;
        }

        // AABB是否重叠
        private static bool IsAABBOverlays(BaseCollision a, BaseCollision b)
        {
            var abb = a.boundingBox;
            var bbb = b.boundingBox;

            if (abb.minx > bbb.maxx || abb.maxx < bbb.minx) return false;
            if (abb.miny > bbb.maxy || abb.maxy < bbb.miny) return false;

            return true;
        }
    }
}
