namespace SimpleX.Collision2D
{
    public struct Penetration
    {
        public Vector2 normal;
        public float depth;

        public Penetration(Vector2 normal, float depth)
        {
            this.normal = normal;
            this.depth = depth;
        }

        public void Clear()
        {
            normal = Vector2.zero;
            depth = 0;
        }
    }
}
