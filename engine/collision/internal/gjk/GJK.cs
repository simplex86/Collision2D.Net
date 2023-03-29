using System.Collections.Generic;

namespace SimpleX.Collision2D
{
    static class GJK
    {
        // 检测图形（geometry1, transform1）与图形（geometry2, transform2）是否重叠
        public static bool Overlaps(ref Transform transform1, IGeometry geometry1,
                                    ref Transform transform2, IGeometry geometry2)
        {
            var simplex = new Simplex()
            {
                vertics = new List<Vector2>(3)
            };
            var dir = transform1.position - transform2.position;

            var pt = Support(ref transform1, ref geometry1, ref transform2, ref geometry2, ref dir);
            simplex.Add(ref pt);

            dir.Negative();

            while (true)
            {
                pt = Support(ref transform1, ref geometry1, ref transform2, ref geometry2, ref dir);
                simplex.Add(ref pt);

                if (simplex.a.Dot(ref dir) <= 0.0f)
                {
                    return false;
                }

                if (simplex.IsContainsOrigin(ref dir))
                {
                    return true;
                }
            }

            return false;
        }

        // 两个图形的闵可夫斯基差
        private static Vector2 Support(ref Transform transform1, ref IGeometry geometry1,
                                       ref Transform transform2, ref IGeometry geometry2, 
                                       ref Vector2 dir)
        {
            var p1 = GeometryHelper.GetFarthestProjectionPoint(geometry1, ref transform1, ref dir);
            var neg = -dir;
            var p2 = GeometryHelper.GetFarthestProjectionPoint(geometry2, ref transform2, ref neg);

            return p1 - p2;
        }
    }
}
