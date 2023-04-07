namespace SimpleX.Collision2D
{
    // 线段
    public class Segment : IGeometry
    {
        public float length;
        public Vector2 normal;

        public GeometryType type => GeometryType.Segment;

        public bool Contains(Vector2 pt)
        {
            var pa = new Vector2(length *  0.5f, 0f);
            var pb = new Vector2(length * -0.5f, 0f);

            var t = Vector2.Dot(pa - pt, pb - pt);
            return MathX.Equals(t, -1.0f);
        }

        public Vector2 GetFarthestProjectionPoint(Vector2 dir)
        {
            return (dir.x >= 0) ? new Vector2(length *  0.5f, 0f)
                                : new Vector2(length * -0.5f, 0f);
        }
    }
}
