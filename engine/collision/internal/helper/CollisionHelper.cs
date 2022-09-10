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

        //
        public static bool Overlays(TriangleCollision a, TriangleCollision b)
        {
            var p1 = a.position;
            var p2 = b.position;
            return GJK.Overlays(ref p1, a.points, ref p2, b.points);
        }

        //
        public static bool Overlays(TriangleCollision a, CircleCollision b)
        {
            var p1 = a.position;
            var p2 = b.position;
            return GJK.Overlays(ref p1, a.points, ref p2, b.radius);
        }

        //
        public static bool Overlays(TriangleCollision a, RectangleCollision b)
        {
            var p1 = a.position;
            var p2 = b.position;
            return GJK.Overlays(ref p1, a.points, ref p2, b.points);
        }

        //
        public static bool Overlays(TriangleCollision a, CapsuleCollision b)
        {
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
