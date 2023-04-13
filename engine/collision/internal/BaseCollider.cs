namespace SimpleX.Collision2D
{
    internal class BaseCollider<T> : ICollider where T : IGeometry
    {
        // 几何体类型
        public GeometryType geometryType => _geometry.type;
        // 包围盒
        public AABB boundingBox => _boundingBox;
        // 几何体
        public T geometry => _geometry;

        // 几何体
        protected T _geometry;
        // 包围盒
        protected AABB _boundingBox = new AABB();

        // 主方向
        protected readonly static Vector2[] CARDINAL_DIRS =
        {
            Vector2.up,
            Vector2.right,
            Vector2.down,
            Vector2.left,
        };

        protected BaseCollider(T geometry)
        {
            _geometry = geometry;
        }

        // 刷新几何信息
        public virtual void RefreshGeometry(float rotation)
        {
            var p1 = GetFarthestProjectionPoint(rotation, CARDINAL_DIRS[0]);
            var p2 = GetFarthestProjectionPoint(rotation, CARDINAL_DIRS[1]);
            var p3 = GetFarthestProjectionPoint(rotation, CARDINAL_DIRS[2]);
            var p4 = GetFarthestProjectionPoint(rotation, CARDINAL_DIRS[3]);
            // 设置包围盒
            _boundingBox.Set(MathX.Min(p1.x, p2.x, p3.x, p4.x),
                             MathX.Min(p1.y, p2.y, p3.y, p4.y),
                             MathX.Max(p1.x, p2.x, p3.x, p4.x),
                             MathX.Max(p1.y, p2.y, p3.y, p4.y));
        }

        // 获取在指定方向（dir）上投影最远的点
        public Vector2 GetFarthestProjectionPoint(float rotation, Vector2 dir)
        {
            return GeometryHelper.GetFarthestProjectionPoint<T>(_geometry, rotation, dir);
        }

        // 获取在指定方向（dir）上投影最远的点
        public Vector2 GetFarthestProjectionPoint(Transform transform, Vector2 dir)
        {
            return GeometryHelper.GetFarthestProjectionPoint<T>(_geometry, transform, dir);
        }
    }
}
