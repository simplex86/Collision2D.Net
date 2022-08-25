using System;

namespace SimpleX.Collision2D.Engine
{
    internal static class CollisionHelper
    {
        // 圆是否包含点
        public static bool Contains(CircleCollision collision, ref Vector pt)
        {
            if (IsAABBContains(ref collision.boundingBox, ref pt))
            {
                return GeometryHelper.IsCircleContains(ref collision.position, collision.radius, ref pt);
            }
            return false;
        }

        // 矩形是否包含点
        public static bool Contains(RectangleCollision collision, ref Vector pt)
        {
            if (IsAABBContains(ref collision.boundingBox, ref pt))
            {
                return GeometryHelper.IsRectangleContains(ref collision.position, collision.width, collision.height, collision.angle, ref pt);
            }
            return false;
        }

        // 胶囊是否包含点
        public static bool Contains(CapsuleCollision collision, ref Vector pt)
        {
            if (IsAABBContains(ref collision.boundingBox, ref pt))
            {
                var position = collision.position;

                var m1 = Matrix.CreateRotationMatrix(collision.angle * MathX.DEG2RAD);
                var m2 = Matrix.CreateTranslationMatrix(position.x, position.y);
                var mt = m1 * m2;

                var p1 = new Vector(collision.length * 0.5f, 0);
                p1 = Matrix.Transform(ref p1, ref mt);

                var p2 = new Vector(collision.length * -0.5f, 0);
                p2 = Matrix.Transform(ref p2, ref mt);

                return GeometryHelper.IsCapsuleContains(ref p1, ref p2, collision.radius, ref pt);
            }
            return false;
        }

        // 两个圆是否碰撞
        public static bool Collides(CircleCollision a, CircleCollision b)
        {
            if (!IsAABBOverlays(a, b)) return false;

            var dist = a.position.Distance(ref b.position);
            return dist <= a.radius + b.radius;
        }

        // 圆和矩形是否碰撞
        public static bool Collides(CircleCollision c, RectangleCollision r)
        {
            if (IsAABBOverlays(c, r))
            {
                return GeometryHelper.IsCircleOverlayWithRectangle(ref c.position, c.radius,
                                                                   ref r.position, r.width, r.height, r.angle);
            }
            return false;
        }

        // 圆和胶囊是否碰撞
        public static bool Collides(CircleCollision a, CapsuleCollision b)
        {
            return Collides(b, a);
        }

        // 两个矩形是否碰撞
        public static bool Collides(RectangleCollision a, RectangleCollision b)
        {
            if (IsAABBOverlays(a, b))
            {
                return GeometryHelper.IsRectangleOverlayWithRectangle(ref a.position, a.width, a.height, a.angle,
                                                                      ref b.position, b.width, b.height, b.angle);
            }
            return false;
        }

        // 矩形和圆是否碰撞
        public static bool Collides(RectangleCollision r, CircleCollision c)
        {
            return Collides(c, r);
        }

        // 矩形和胶囊是否碰撞
        public static bool Collides(RectangleCollision a, CapsuleCollision b)
        {
            return Collides(b, a);
        }

        // 两个胶囊是否碰撞
        public static bool Collides(CapsuleCollision a, CapsuleCollision b)
        {
            if (IsAABBOverlays(a, b))
            {
                var pas = GeometryHelper.GetCapsulePoints(ref a.position, a.length, a.angle);
                var pbs = GeometryHelper.GetCapsulePoints(ref b.position, b.length, b.angle);

                var dr = a.radius + b.radius;
                if (GeometryHelper.GetDistance2(ref pas[0], ref pas[1], ref pbs[0]) <= dr * dr) return true;
                if (GeometryHelper.GetDistance2(ref pas[0], ref pas[1], ref pbs[1]) <= dr * dr) return true;
                if (GeometryHelper.IsSegmentIntersected(ref pas[0], ref pas[1], ref pbs[0], ref pbs[1])) return true;
            }
            return false;
        }

        // 胶囊和圆是否碰撞
        public static bool Collides(CapsuleCollision a, CircleCollision b)
        {
            if (IsAABBOverlays(a, b))
            {
                var position = a.position;
                var m1 = Matrix.CreateRotationMatrix(a.angle * MathX.DEG2RAD);
                var m2 = Matrix.CreateTranslationMatrix(a.position.x, a.position.y);
                var mt = m1 * m2;

                var p1 = new Vector(a.length * 0.5f, 0);
                p1 = Matrix.Transform(ref p1, ref mt);

                var p2 = new Vector(a.length * -0.5f, 0);
                p2 = Matrix.Transform(ref p2, ref mt);

                var radius = a.radius + b.radius;
                return GeometryHelper.IsCapsuleContains(ref p1, ref p2, radius, ref b.position);
            }
            return false;
        }

        // 胶囊和矩形是否碰撞
        public static bool Collides(CapsuleCollision a, RectangleCollision b)
        {
            if (IsAABBOverlays(a, b))
            {
                var ps = GeometryHelper.GetCapsulePoints(ref a.position, a.length, a.angle);
                if (GeometryHelper.IsCircleOverlayWithRectangle(ref ps[0], a.radius, ref b.position, b.width, b.height, b.angle)) return true;
                if (GeometryHelper.IsCircleOverlayWithRectangle(ref ps[1], a.radius, ref b.position, b.width, b.height, b.angle)) return true;
                if (GeometryHelper.IsRectangleOverlayWithRectangle(ref a.position, a.length, a.radius * 2, a.angle, ref b.position, b.width, b.height, b.angle)) return true;
            }
            return false;
        }

        // AABB是否包含点pt
        private static bool IsAABBContains(ref AABB box, ref Vector pt)
        {
            return box.minx <= pt.x && box.maxx >= pt.x &&
                   box.miny <= pt.y && box.maxy >= pt.y;
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
