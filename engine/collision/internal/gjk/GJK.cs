using System.Collections.Generic;

namespace SimpleX.Collision2D.Engine
{
    static class GJK
    {
        public static bool Overlays(BaseCollision a, BaseCollision b)
        {
            var simplex = new Simplex()
            {
                vertics = new List<Vector>(3)
            };
            var dir = a.position - b.position;

            var pt = Support(a.points, b.points, ref dir);
            simplex.Add(ref pt);

            dir.Negative();

            while (true)
            {
                pt = Support(a.points, b.points, ref dir);
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

        private static Vector Support(Vector[] a, Vector[] b, ref Vector dir)
        {
            var p1 = Support(a, ref dir);
            var neg = -dir;
            var p2 = Support(b, ref neg);

            return p1 - p2;
        }

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
            var z = a.x * b.y - a.y * b.x;
            return new Vector(-z * c.y, z * c.x);
        }
    }
}
