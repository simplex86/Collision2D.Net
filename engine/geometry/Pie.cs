namespace SimpleX.Collision2D
{
    // 扇形
    public class Pie : IGeometry
    {
        public float radius;
        public float sweep;

        public GeometryType type => GeometryType.Pie;

        public bool Contains(Vector2 pt)
        {
            if (pt.magnitude2 > radius * radius)
            {
                return false;
            }

            pt.y = MathX.Abs(pt.y);
            var a0 = pt.Angle() * MathX.RAD2DEG;
            var a1 = MathX.Clamp360(sweep * 0.5f);

            return a0 <= a1;
        }

        public Vector2 GetFarthestProjectionPoint(Vector2 dir)
        {
            var s = MathX.Sign(dir.y);

            dir.y = MathX.Abs(dir.y);
            var a0 = dir.Angle() * MathX.RAD2DEG;
            var a1 = MathX.Clamp360(sweep * 0.5f);

            var pt = Vector2.zero;
            if (a0 <= a1)
            {
                pt = dir.normalized * radius;
            }
            else if (a0 <= 90 + a1)
            {
                var mt = Matrix.CreateRotationMatrix(a1 * MathX.DEG2RAD);
                pt = Matrix.Transform(Vector2.right, mt) * radius;
            }
            pt.y *= s;

            return pt;
        }
    }
}
