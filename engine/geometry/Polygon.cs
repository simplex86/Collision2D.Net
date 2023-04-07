namespace SimpleX.Collision2D
{
    // 凸多边形
    public class Polygon : IGeometry
    {
        public Vector2[] vertics;

        public GeometryType type => GeometryType.Polygon;

        public bool Contains(Vector2 pt)
        {
            int n = vertics.Length;

            var u = vertics[n - 1] - pt;
            var v = vertics[0] - pt;
            var z = Vector2.Cross(u, v);

            for (int i = 0; i < n - 1; i++)
            {
                u = vertics[i] - pt;
                v = vertics[i + 1] - pt;
                var w = Vector2.Cross(u, v);

                if (z * w < 0) return false;
            }

            return true;
        }

        public Vector2 GetFarthestProjectionPoint(Vector2 dir)
        {
            var point = vertics[0];
            var max = Vector2.Dot(point, dir);

            for (int i = 1; i < vertics.Length; i++)
            {
                var dot = Vector2.Dot(vertics[i], dir);
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
