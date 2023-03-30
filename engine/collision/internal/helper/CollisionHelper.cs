using System;

namespace SimpleX.Collision2D
{
    internal static class CollisionHelper
    {
        // 两个圆是否碰撞
        public static bool Overlaps(BaseCollision a, BaseCollision b)
        {
            if (IsAABBOverlaps(a, b))
            {
                return GJK.Overlaps(a.transform, a.geometry,
                                    b.transform, b.geometry);
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
