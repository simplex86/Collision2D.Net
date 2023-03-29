using System;

namespace SimpleX.Collision2D
{
    public abstract class BaseCollision
    {
        // 类型
        public CollisionType type { get; }
        //
        public IGeometry geometry;
        //
        public Transform transform = new Transform()
        {
            position = Vector2.zero,
            rotation = 0.0f,
            scale = 1.0f,
        };
        // 包围盒
        public AABB boundingBox = new AABB();

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
        protected BaseCollision(CollisionType type, Vector2 position, float rotation)
        {
            this.type = type;
            this.transform.position = position;
            this.transform.rotation = rotation;
            this.dirty = DirtyFlag.Position;
        }

        // 移动
        public void Move(ref Vector2 delta)
        {
            transform.position += delta;
            dirty |= DirtyFlag.Position;
        }

        // 移动到
        public void MoveTo(ref Vector2 position)
        {
            transform.position = position;
            dirty |= DirtyFlag.Position;
        }

        // 旋转
        public void Rotate(float delta)
        {
            transform.rotation += delta;
            dirty |= DirtyFlag.Rotation;
        }

        // 旋转到
        public void RotateTo(float angle)
        {
            transform.rotation = angle;
            dirty |= DirtyFlag.Rotation;
        }

        // 刷新几何信息
        public virtual void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                var p1 = GeometryHelper.GetFarthestProjectionPoint(geometry, ref transform, ref CARDINAL_DIRS[0]);
                var p2 = GeometryHelper.GetFarthestProjectionPoint(geometry, ref transform, ref CARDINAL_DIRS[1]);
                var p3 = GeometryHelper.GetFarthestProjectionPoint(geometry, ref transform, ref CARDINAL_DIRS[2]);
                var p4 = GeometryHelper.GetFarthestProjectionPoint(geometry, ref transform, ref CARDINAL_DIRS[3]);

                boundingBox.minx = MathX.Min(p1.x, p2.x, p3.x, p4.x);
                boundingBox.maxx = MathX.Max(p1.x, p2.x, p3.x, p4.x);
                boundingBox.miny = MathX.Min(p1.y, p2.y, p3.y, p4.y);
                boundingBox.maxy = MathX.Max(p1.y, p2.y, p3.y, p4.y);

                dirty = DirtyFlag.None;
            }
        }

        // 是否包含点pt
        public bool Contains(ref Vector2 pt)
        {
            if (IsAABBContains(ref pt))
            {
                return GeometryHelper.IsGeometryContains(geometry, ref transform, ref pt);
            }
            return false;
        }

        // 是否与collision产生碰撞
        public bool Overlaps(BaseCollision collision)
        {
            return CollisionHelper.Overlaps(this, collision);
        }

        // AABB是否包含点pt
        protected bool IsAABBContains(ref Vector2 pt)
        {
            return boundingBox.minx <= pt.x && boundingBox.maxx >= pt.x &&
                   boundingBox.miny <= pt.y && boundingBox.maxy >= pt.y;
        }
    }
}
