using System;

namespace SimpleX.Collision2D.Engine
{
    public abstract class BaseCollision
    {
        // 类型
        public CollisionType type { get; private set; }
        // 位置
        public Vector position;
        // 包围盒
        public AABB boundingBox;
        // 脏标记
        public bool dirty = true;

        // 刷新几何信息
        public abstract void RefreshGeometry();

        // 是否包含点pt
        public abstract bool Contains(ref Vector pt);

        // 是否与collision产生碰撞
        public abstract bool Collides(BaseCollision collision);
        
        // 顶点
        internal Vector[] points;

        //
        protected BaseCollision(CollisionType type)
        {
            this.type = type;
        }
    }
}
