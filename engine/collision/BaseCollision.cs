using System;

namespace SimpleX.Collision2D
{
    public abstract class BaseCollision
    {
        // 类型
        public CollisionType type { get; }
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

        //
        protected BaseCollision(CollisionType type)
        {
            this.type = type;
            dirty = DirtyFlag.Position;
        }

        // 移动
        public void Move(ref Vector2 delta)
        {
            transform.position += delta;
            dirty |= DirtyFlag.Position;
        }

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

        // 刷新几何信息
        public abstract void RefreshGeometry();

        // 是否包含点pt
        public abstract bool Contains(ref Vector2 pt);

        // 是否与collision产生碰撞
        public abstract bool Overlaps(BaseCollision collision);

        //
        public abstract Vector2 GetFarthestProjectionPoint(ref Vector2 dir);

        // AABB是否包含点pt
        protected bool IsAABBContains(ref Vector2 pt)
        {
            return boundingBox.minx <= pt.x && boundingBox.maxx >= pt.x &&
                   boundingBox.miny <= pt.y && boundingBox.maxy >= pt.y;
        }
    }
}
