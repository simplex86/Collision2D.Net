using System;

namespace SimpleX.Collision2D.Engine
{
    public abstract class BaseCollision
    {
        // 类型
        public CollisionType type { get; private set; }
        // 包围盒
        public AABB boundingBox;

        // 位置
        internal abstract Vector position { get; }
        // 顶点
        internal abstract Vector[] points { get; }

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
        public virtual void Move(ref Vector delta)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] += delta;
            }
            dirty |= DirtyFlag.Position;
        }

        public void MoveTo(ref Vector position)
        {
            var delta = position - this.position;
            Move(ref delta);
        }

        // 旋转
        public abstract void Rotate(float delta);

        // 刷新几何信息
        public abstract void RefreshGeometry();

        // 是否包含点pt
        public abstract bool Contains(ref Vector pt);

        // 是否与collision产生碰撞
        public abstract bool Overlaps(BaseCollision collision);

        // AABB是否包含点pt
        protected bool IsAABBContains(ref Vector pt)
        {
            return boundingBox.minx <= pt.x && boundingBox.maxx >= pt.x &&
                   boundingBox.miny <= pt.y && boundingBox.maxy >= pt.y;
        }
    }
}
