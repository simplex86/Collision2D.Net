namespace SimpleX.Collision2D
{
    // 圆形
    public struct Circle : IGeometry
    {
        public float radius;

        public bool Contains(Vector2 pt)
        {
            return pt.magnitude2 <= radius * radius;
        }

        public Vector2 GetFarthestProjectionPoint(Vector2 dir)
        {
            return radius * dir.normalized;
        }
    }
}
