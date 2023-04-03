namespace SimpleX.Collision2D
{
    public abstract class ICollider
    {
        // 图形
        public IGeometry geometry { get; protected set; }
        // 
        public Transform transform = new Transform()
        {
            position = Vector2.zero,
            rotation = 0.0f,
            scale = 1.0f,
        };
        // 包围盒
        public AABB boundingBox = new AABB();

        protected ICollider()
        {
        }

        public abstract void Move(Vector2 delta);
        public abstract void MoveTo(Vector2 position);
        public abstract void Rotate(float delta);
        public abstract void RotateTo(float rotation);

        public abstract void RefreshGeometry();

        public abstract bool Contains(Vector2 pt);
        public abstract bool Overlaps(ICollider collider);
    }
}
