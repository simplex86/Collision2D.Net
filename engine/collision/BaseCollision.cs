using System;

namespace SimpleX.Collision2D.Engine
{
    public abstract class BaseCollision
    {
        //
        public Vector position;

        //
        public abstract bool Contains(ref Vector pt);

        //
        public abstract bool Collides(BaseCollision collision);

        protected BaseCollision(Vector position)
        {
            this.position = position;
        }
    }
}
