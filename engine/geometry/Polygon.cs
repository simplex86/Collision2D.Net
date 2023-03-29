namespace SimpleX.Collision2D
{
    // 凸多边形
    public struct Polygon : IGeometry
    {
        public Vector2[] vertics;

        public bool Contains(ref Vector2 pt)
        {
            int n = vertics.Length;

            var u = vertics[n - 1] - pt;
            var v = vertics[0] - pt;
            var z = Vector2.Cross(ref u, ref v);

            for (int i = 0; i < n - 1; i++)
            {
                u = vertics[i] - pt;
                v = vertics[i + 1] - pt;
                var w = Vector2.Cross(ref u, ref v);

                if (z * w < 0) return false;
            }

            return true;
        }

        public Vector2 GetFarthestProjectionPoint(ref Vector2 dir)
        {
            var point = vertics[0];
            var max = Vector2.Dot(ref point, ref dir);

            for (int i = 1; i < vertics.Length; i++)
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
    }
}
