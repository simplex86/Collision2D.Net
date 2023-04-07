﻿namespace SimpleX.Collision2D
{
    public abstract class ICollider
    {
        // 几何体
        public IGeometry geometry { get; }
        // 包围盒
        public AABB boundingBox => _boundingBox;

        // 
        public Transform transform { get; protected set; } = new Transform()
        {
            position = Vector2.zero,
            rotation = 0.0f,
            scale = 1.0f,
        };

        protected AABB _boundingBox = new AABB();

        // 主方向
        protected readonly static Vector2[] CARDINAL_DIRS =
        {
            Vector2.up,
            Vector2.right,
            Vector2.down,
            Vector2.left,
        };

        protected ICollider(IGeometry geometry)
        {
            this.geometry = geometry;
        }

        // 刷新几何信息
        public void RefreshGeometry(Transform transform)
        {
            this.transform = transform;
            OnRefreshGeometry();
        }

        // 是否包含点pt
        public bool Contains(Vector2 pt)
        {
            if (BoundingBoxHelper.Contains(boundingBox, transform, pt))
            {
                return GeometryHelper.Contains(geometry, transform, pt);
            }
            return false;
        }

        // 是否与collider产生碰撞
        public bool Overlaps(ICollider collider)
        {
            if (BoundingBoxHelper.Overlaps(boundingBox, transform, collider.boundingBox, collider.transform))
            {
                return GeometryHelper.Overlaps(geometry, transform, collider.geometry, collider.transform);
            }
            return false;
        }

        // 刷新几何信息
        protected virtual void OnRefreshGeometry()
        {
            var p1 = GeometryHelper.GetFarthestProjectionPoint(geometry, transform.rotation, CARDINAL_DIRS[0]);
            var p2 = GeometryHelper.GetFarthestProjectionPoint(geometry, transform.rotation, CARDINAL_DIRS[1]);
            var p3 = GeometryHelper.GetFarthestProjectionPoint(geometry, transform.rotation, CARDINAL_DIRS[2]);
            var p4 = GeometryHelper.GetFarthestProjectionPoint(geometry, transform.rotation, CARDINAL_DIRS[3]);
            // 设置包围盒
            _boundingBox.Set(MathX.Min(p1.x, p2.x, p3.x, p4.x),
                             MathX.Min(p1.y, p2.y, p3.y, p4.y),
                             MathX.Max(p1.x, p2.x, p3.x, p4.x),
                             MathX.Max(p1.y, p2.y, p3.y, p4.y));
        }
    }
}
