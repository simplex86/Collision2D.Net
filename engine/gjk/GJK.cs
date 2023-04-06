using System.Collections.Generic;

namespace SimpleX.Collision2D
{
    static class GJK
    {
        // 检测图形（geometry1, transform1）与图形（geometry2, transform2）是否重叠
        public static bool Overlaps(IGeometry geometry1, Transform transform1,
                                    IGeometry geometry2, Transform transform2)
        {
            var simplex = new Simplex()
            {
                vertics = new List<Vector2>(3)
            };
            var dir = transform1.position - transform2.position;

            var pt = Support(geometry1, transform1, geometry2, transform2, dir);
            simplex.Add(pt);

            dir.Negative();

            while (true)
            {
                pt = Support(geometry1, transform1, geometry2, transform2, dir);
                simplex.Add(pt);

                if (simplex.a.Dot(dir) <= 0.0f)
                {
                    return false;
                }

                if (simplex.IsContainsOrigin(dir))
                {
                    return true;
                }
            }

            return false;
        }

        // 两个图形的闵可夫斯基差
        private static Vector2 Support(IGeometry geometry1, Transform transform1, 
                                       IGeometry geometry2, Transform transform2,  
                                       Vector2 dir)
        {
            var p1 = GeometryHelper.GetFarthestProjectionPoint(geometry1, transform1, dir);
            dir.Negative();
            var p2 = GeometryHelper.GetFarthestProjectionPoint(geometry2, transform2, dir);

            return p1 - p2;
        }
    }
}
