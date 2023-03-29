namespace SimpleX.Collision2D
{
    public struct Circle : IGeometry
    {
        public float radius;

        public bool Contains(ref Vector2 pt)
        {
            return pt.magnitude2 <= radius * radius;
        }

        public Vector2 GetFarthestProjectionPoint(ref Vector2 dir)
        {
            return radius * dir.normalized;
        }
    }
}
