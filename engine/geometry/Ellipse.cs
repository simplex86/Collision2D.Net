namespace SimpleX.Collision2D
{
    // 椭圆方程：x²/a²﹢y²/b²=1
    public class Ellipse : IGeometry
    {
        public float width;
        public float height;

        public float A => width * 0.5f;
        public float B => height * 0.5f;

        public bool Contains(Vector2 pt)
        {
            var x2 = MathX.Pow2(pt.x);
            var y2 = MathX.Pow2(pt.y);
            var a2 = MathX.Pow2(A);
            var b2 = MathX.Pow2(B);

            return x2 / a2 + y2 / b2 <= 1;
        }

        public Vector2 GetFarthestProjectionPoint(Vector2 dir)
        {
            var x = 0f;
            var y = 0f;

            if (MathX.Equals(dir.x, 0.0f))
            {
                var sign = dir.y < 0.0f ? -1 : 1;
                y = sign * B;
            }
            else if (MathX.Equals(dir.y, 0.0f))
            {
                var sign = dir.x < 0.0f ? -1 : 1;
                x = sign * A;
            }
            else
            {
                var k = dir.y / dir.x;

                var a2 = MathX.Pow2(A);
                var b2 = MathX.Pow2(B);
                var k2 = MathX.Pow2(k);

                var t = MathX.Sqrt((a2 + b2 * k2) / k2);
                var v = new Vector2(0.0f, t);
                if (Vector2.Dot(v, dir) < 0)
                {
                    t *= -1;
                }

                x = k * t - (b2 * k2 * k * t) / (a2 + b2 * k2);
                y = (b2 * k2 * t) / (a2 + b2 * k2);
            }

            return new Vector2(x, y);
        }
    }
}
