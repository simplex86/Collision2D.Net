namespace SimpleX.Collision2D
{
    public abstract class ICollider
    {
        // 几何体
        public IGeometry geometry { get; protected set; }
        // 包围盒
        public AABB boundingBox = new AABB();

        // 
        public Transform transform { get; private set; } = new Transform()
        {
            position = Vector2.zero,
            rotation = 0.0f,
            scale = 1.0f,
        };

        protected ICollider()
        {

        }

        public void RefreshGeometry(Transform transform)
        {
            this.transform = transform;
            OnRefreshGeometry();
        }

        public abstract bool Contains(Vector2 pt);
        public abstract bool Overlaps(ICollider collider);

        protected abstract void OnRefreshGeometry();
    }
}
