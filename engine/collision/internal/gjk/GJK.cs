using System.Collections.Generic;

namespace SimpleX.Collision2D
{
    static class GJK
    {
        // 检测圆形（position1, radius1）与圆形（position2, radius2）是否重叠
        public static bool Overlaps(ref Vector2 position1, float radius1,
                                    ref Vector2 position2, float radius2)
        {
            var simplex = new Simplex()
            {
                vertics = new List<Vector2>(3)
            };
            var dir = position1 - position2;

            var pt = Support(ref position1, radius1, ref position2, radius2, ref dir);
            simplex.Add(ref pt);

            dir.Negative();

            while (true)
            {
                pt = Support(ref position1, radius1, ref position2, radius2, ref dir);
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

        // 检测凸多边形（position1, vertics）与圆（position2，radius）是否重叠
        public static bool Overlaps(ref Vector2 position1, Vector2[] vertics,
                                    ref Vector2 position2, float radius)
        {
            var simplex = new Simplex()
            {
                vertics = new List<Vector2>(3)
            };
            var dir = position1 - position2;

            var pt = Support(vertics, ref position2, radius, ref dir);
            simplex.Add(ref pt);

            dir.Negative();

            while (true)
            {
                pt = Support(vertics, ref position2, radius, ref dir);
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

        // 检测凸多边形（position1, vertics1）与凸多边形（position2, vertics2）是否重叠
        public static bool Overlaps(ref Vector2 position1, Vector2[] vertics1, 
                                    ref Vector2 position2, Vector2[] vertics2)
        {
            var simplex = new Simplex()
            {
                vertics = new List<Vector2>(3)
            };
            var dir = position1 - position2;

            var pt = Support(vertics1, vertics2, ref dir);
            simplex.Add(ref pt);

            dir.Negative();

            while (true)
            {
                pt = Support(vertics1, vertics2, ref dir);
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

        // 检测凸多边形（position1, vertics）与胶囊（position2，length，radius，angle）是否重叠
        public static bool Overlaps(ref Vector2 position1, Vector2[] vertics, 
                                    ref Vector2 position2, float length, float radius, float angle)
        {
            var simplex = new Simplex()
            {
                vertics = new List<Vector2>(3)
            };
            var dir = position1 - position2;

            var pt = Support(vertics, ref position2, length, radius, angle, ref dir);
            simplex.Add(ref pt);

            dir.Negative();

            while (true)
            {
                pt = Support(vertics, ref position2, length, radius, angle, ref dir);
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

        // 检测胶囊（position1，length，radius1，angle）与圆形（position2，radius2）是否重叠
        public static bool Overlaps(ref Vector2 position1, float length, float radius1, float angle,
                                    ref Vector2 position2, float radius2)
        {
            var simplex = new Simplex()
            {
                vertics = new List<Vector2>(3)
            };
            var dir = position1 - position2;

            var pt = Support(ref position1, length, radius1, angle, ref position2, radius2, ref dir);
            simplex.Add(ref pt);

            dir.Negative();

            while (true)
            {
                pt = Support(ref position1, length, radius1, angle, ref position2, radius2, ref dir);
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

        // 检测胶囊（position1，length1，radius1，angle1）与胶囊（position2，length2，radius2，angle2）是否重叠
        public static bool Overlaps(ref Vector2 position1, float length1, float radius1, float angle1,
                                    ref Vector2 position2, float length2, float radius2, float angle2)
        {
            var simplex = new Simplex()
            {
                vertics = new List<Vector2>(3)
            };
            var dir = position1 - position2;

            var pt = Support(ref position1, length1, radius1, angle1, ref position2, length2, radius2, angle2, ref dir);
            simplex.Add(ref pt);

            dir.Negative();

            while (true)
            {
                pt = Support(ref position1, length1, radius1, angle1, ref position2, length2, radius2, angle2, ref dir);
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

        // 两个圆形的闵可夫斯基差
        private static Vector2 Support(ref Vector2 position1, float radius1,
                                       ref Vector2 position2, float radius2, 
                                       ref Vector2 dir)
        {
            var p1 = GetFarthestProjectionPoint(ref position1, radius1, ref dir);
            var neg = -dir;
            var p2 = GetFarthestProjectionPoint(ref position2, radius2, ref neg);

            return p1 - p2;
        }

        // 两个多边形的闵可夫斯基差
        private static Vector2 Support(Vector2[] a, Vector2[] b, ref Vector2 dir)
        {
            var p1 = GetFarthestProjectionPoint(a, ref dir);
            var neg = -dir;
            var p2 = GetFarthestProjectionPoint(b, ref neg);

            return p1 - p2;
        }

        // 多边形和圆的闵可夫斯基差
        private static Vector2 Support(Vector2[] vertics, 
                                       ref Vector2 position, float radius, 
                                       ref Vector2 dir)
        {
            var p1 = GetFarthestProjectionPoint(vertics, ref dir);
            var neg = -dir;
            var p2 = GetFarthestProjectionPoint(ref position, radius, ref neg);

            return p1 - p2;
        }

        // 多边形和胶囊的闵可夫斯基差
        private static Vector2 Support(Vector2[] vertics, 
                                       ref Vector2 position, float length, float radius, float angle, 
                                       ref Vector2 dir)
        {
            var p1 = GetFarthestProjectionPoint(vertics, ref dir);
            var neg = -dir;
            var p2 = GetFarthestProjectionPoint(ref position, length, radius, angle, ref neg);

            return p1 - p2;
        }

        // 胶囊和圆形的闵可夫斯基差
        private static Vector2 Support(ref Vector2 position1, float length, float radius1, float angle,
                                       ref Vector2 position2, float radius2,
                                       ref Vector2 dir)
        {
            var p1 = GetFarthestProjectionPoint(ref position1, length, radius1, angle, ref dir);
            var neg = -dir;
            var p2 = GetFarthestProjectionPoint(ref position2, radius2, ref neg);

            return p1 - p2;
        }

        // 两个胶囊的闵可夫斯基差
        private static Vector2 Support(ref Vector2 position1, float length1, float radius1, float angle1,
                                       ref Vector2 position2, float length2, float radius2, float angle2,
                                       ref Vector2 dir)
        {
            var p1 = GetFarthestProjectionPoint(ref position1, length1, radius1, angle1, ref dir);
            var neg = -dir;
            var p2 = GetFarthestProjectionPoint(ref position2, length2, radius2, angle2, ref neg);

            return p1 - p2;
        }

        // 获取顶点集合（vertics）中在方向（dir）上最大投影的点
        private static Vector2 GetFarthestProjectionPoint(Vector2[] vertics, ref Vector2 dir)
        {
            var point = vertics[0];
            var max = Vector2.Dot(ref point, ref dir);
            
            for (int i=1; i<vertics.Length; i++)
            {
                var dot = Vector2.Dot(ref vertics[i], ref dir);
                if (dot > max)
                {
                    max = dot;
                    point = vertics[i];
                }
            }

            return point;
        }

        // 获取圆（position, radius）在方向（dir）上最大投影的点
        private static Vector2 GetFarthestProjectionPoint(ref Vector2 position, float radius, ref Vector2 dir)
        {
            return position + radius * dir.normalized;
        }

        // 获取胶囊（position, length, radius, angle）在方向（dir）上最大投影的点
        private static Vector2 GetFarthestProjectionPoint(ref Vector2 position, float length, float radius, float angle, ref Vector2 dir)
        {
            var m1 = Matrix.CreateRotationMatrix(-angle * MathX.DEG2RAD);
            var d1 = Matrix.Transform(ref dir, ref m1);

            var p1 = radius * d1.normalized;
            var dx = d1.x >= 0 ? length * 0.5f : -length * 0.5f;
            p1.x += dx;

            var m2 = Matrix.CreateRotationMatrix(angle * MathX.DEG2RAD);
            var p2 = Matrix.Transform(ref p1, ref m2);

            return p2 + position;
        }
    }
}
