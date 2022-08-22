using System;

namespace SimpleX.Collision2D.Engine
{
    public class CapsuleCollision : BaseCollision
    {
        public CapsuleCollision(Vector position)
            : base(position)
        {

        }

        public override void RefreshBoundingBox()
        {
            throw new NotImplementedException();
        }

        public override bool Contains(ref Vector pt)
        {
            throw new NotImplementedException();
        }

        public override bool Collides(BaseCollision collision)
        {
            throw new NotImplementedException();
        }
    }
}
