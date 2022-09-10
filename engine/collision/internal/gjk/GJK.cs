using System.Collections.Generic;

namespace SimpleX.Collision2D.Engine
{
    static class GJK
    {
        // 检测凸多边形（position1, points1）与凸多边形（position2, points2）是否重叠
        public static bool Overlays(ref Vector position1, Vector[] points1, ref Vector position2, Vector[] points2)
        {
            var simplex = new Simplex()
            {
                vertics = new List<Vector>(3)
            };
            var dir = position1 - position2;

            var pt = Support(points1, points2, ref dir);
            simplex.Add(ref pt);

            dir.Negative();

            while (true)
            {
                pt = Support(points1, points2, ref dir);
                simplex.Add(ref pt);

                if (simplex.a.Dot(ref dir) <= 0.0f)
                {
                    return false;
                }

                if (IsContainsOrigin(ref simplex, ref dir))
                {
                    return true;
                }
            }

            return false;
        }

        // 检测凸多边形（position1, points）与圆（position2，radius）是否重叠
        public static bool Overlays(ref Vector position1, Vector[] points, ref Vector position2, float radius)
        {
            var simplex = new Simplex()
            {
                vertics = new List<Vector>(3)
            };
            var dir = position1 - position2;

            var pt = Support(points, ref position2, radius, ref dir);
            simplex.Add(ref pt);

            dir.Negative();

            while (true)
            {
                pt = Support(points, ref position2, radius, ref dir);
                simplex.Add(ref pt);

                if (simplex.a.Dot(ref dir) <= 0.0f)
                {
                    return false;
                }

                if (IsContainsOrigin(ref simplex, ref dir))
                {
                    return true;
                }
            }

            return false;
        }

        // 两个多边形的闵可夫斯基差
        private static Vector Support(Vector[] a, Vector[] b, ref Vector dir)
        {
            var p1 = Support(a, ref dir);
            var neg = -dir;
            var p2 = Support(b, ref neg);

            return p1 - p2;
        }

        // 闵可夫斯基差
        private static Vector Support(Vector[] vertics, ref Vector position, float radius, ref Vector dir)
        {
            var p1 = Support(vertics, ref dir);
            var neg = -dir;
            var p2 = Support(ref position, radius, ref neg);

            return p1 - p2;
        }

        // 获取顶点集合（vertics）中在方向（dir）上最大投影的点
        private static Vector Support(Vector[] vertics, ref Vector dir)
        {
            var point = vertics[0];
            var max = Vector.Dot(ref point, ref dir);
            
            for (int i=1; i<vertics.Length; i++)
            {
                var dot = Vector.Dot(ref vertics[i], ref dir);
                if (dot > max)
                {
                    max = dot;
                    point = vertics[i];
                }
            }

            return point;
        }

        // 获取圆（）在方向（dir）上最大投影的点
        private static Vector Support(ref Vector position, float radius, ref Vector dir)
        {
            return position + radius * dir.normalized;
        }

        // 简单形是否包含原点
        private static bool IsContainsOrigin(ref Simplex simplex, ref Vector dir)
        {
            var a = simplex.a;
            var ao = -a;
            var b = simplex.b;
            var ab = b - a;

            if (simplex.count == 3)
            {
                var c = simplex.c;
                var ac = c - a;

                var u = Mul3(ref ac, ref ab, ref ab);
                var v = Mul3(ref ab, ref ac, ref ac);

                if (Vector.Dot(ref u, ref ao) > 0.0f)
                {
                    simplex.Remove('c');
                    dir = u;
                }
                else
                {
                    if (Vector.Dot(ref v, ref ao) <= 0.0f)
                    {
                        return true;
                    }

                    simplex.Remove('b');
                    dir = v;
                }
            }
            else
            {
                dir = Mul3(ref ab, ref ao, ref ab);
            }

            return false;
        }

        // 向量三重积
        private static Vector Mul3(ref Vector a, ref Vector b, ref Vector c)
        {
            var n = Vector.Cross(ref a, ref b);
            return Vector.Cross(ref n, ref c);
        }
    }
}
