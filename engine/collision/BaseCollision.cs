using System;

namespace SimpleX.Collision2D.Engine
{
    public abstract class BaseCollision
    {
        // 位置
        public Vector position;
        // 包围盒
        public AABB boundingBox;

        // 刷新包围盒
        public abstract void RefreshBoundingBox();

        // 是否包含点pt
        public abstract bool Contains(ref Vector pt);

        // 是否与collision产生碰撞
        public abstract bool Collides(BaseCollision collision);

        //
        protected BaseCollision(Vector position)
        {
            this.position = position;
        }
    }
}
