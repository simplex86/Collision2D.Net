namespace SimpleX.Collision2D
{
    // 
    public abstract class BaseCollider<T> : ICollider where T :IGeometry
    {
        protected static class DirtyFlag
        {
            public const byte None = 0x00;
            public const byte Position = 0x01;
            public const byte Rotation = 0x02;
        }
        // 脏标记
        protected byte dirty = DirtyFlag.None;
        // 主方向
        protected readonly static Vector2[] CARDINAL_DIRS =
        {
            Vector2.up,
            Vector2.right,
            Vector2.down,
            Vector2.left,
        };

        //
        protected BaseCollider(Vector2 position, float rotation)
        {
            this.transform.position = position;
            this.transform.rotation = rotation;
            this.dirty = DirtyFlag.Position | DirtyFlag.Rotation;
        }

        // 移动
        public override void Move(Vector2 delta)
        {
            transform.position += delta;
            dirty |= DirtyFlag.Position;
        }

        // 移动到
        public override void MoveTo(Vector2 position)
        {
            transform.position = position;
            dirty |= DirtyFlag.Position;
        }

        // 旋转
        public override void Rotate(float delta)
        {
            transform.rotation += delta;
            dirty |= DirtyFlag.Rotation;
        }

        // 旋转到
        public override void RotateTo(float angle)
        {
            transform.rotation = angle;
            dirty |= DirtyFlag.Rotation;
        }

        // 刷新几何信息
        public override void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                var p1 = GeometryHelper.GetFarthestProjectionPoint(geometry, transform, CARDINAL_DIRS[0]);
                var p2 = GeometryHelper.GetFarthestProjectionPoint(geometry, transform, CARDINAL_DIRS[1]);
                var p3 = GeometryHelper.GetFarthestProjectionPoint(geometry, transform, CARDINAL_DIRS[2]);
                var p4 = GeometryHelper.GetFarthestProjectionPoint(geometry, transform, CARDINAL_DIRS[3]);

                boundingBox.minx = MathX.Min(p1.x, p2.x, p3.x, p4.x);
                boundingBox.maxx = MathX.Max(p1.x, p2.x, p3.x, p4.x);
                boundingBox.miny = MathX.Min(p1.y, p2.y, p3.y, p4.y);
                boundingBox.maxy = MathX.Max(p1.y, p2.y, p3.y, p4.y);

                dirty = DirtyFlag.None;
            }
        }

        // 是否包含点pt
        public override bool Contains(Vector2 pt)
        {
            if (boundingBox.Contains(pt))
            {
                return GeometryHelper.IsGeometryContains(geometry, transform, pt);
            }
            return false;
        }

        // 是否与collider产生碰撞
        public override bool Overlaps(ICollider collider)
        {
            if (boundingBox.Overlaps(collider.boundingBox))
            {
                return GJK.Overlaps(geometry, transform, collider.geometry, collider.transform);
            }
            return false;
        }
    }
}
