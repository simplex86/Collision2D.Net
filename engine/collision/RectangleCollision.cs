using System;

namespace SimpleX.Collision2D.Engine
{
    public class RectangleCollision : BaseCollision
    {
        public float width;
        public float height;
        public float angle;

        public RectangleCollision(ref Vector position, float width, float height, float angle)
            : base(position)
        {
            this.width = width;
            this.height = height;
            this.angle = angle;
        }

        public override void RefreshBoundingBox()
        {
            var w = width * 0.5f;
            var h = height * 0.5f;

            var p1 = new Vector(-w, -h);
            var p2 = new Vector( w, -h);
            var p3 = new Vector( w, h);
            var p4 = new Vector(-w, h);

            var mt = Matrix.CreateRotationMatrix(angle * MathX.DEG2RAD);

            p1 = Matrix.Transform(ref p1, ref mt) + position;
            p2 = Matrix.Transform(ref p2, ref mt) + position;
            p3 = Matrix.Transform(ref p3, ref mt) + position;
            p4 = Matrix.Transform(ref p4, ref mt) + position;

            boundingBox.minx = MathX.Min(p1.x, p2.x, p3.x, p4.x);
            boundingBox.maxx = MathX.Max(p1.x, p2.x, p3.x, p4.x);
            boundingBox.miny = MathX.Min(p1.y, p2.y, p3.y, p4.y);
            boundingBox.maxy = MathX.Max(p1.y, p2.y, p3.y, p4.y);
        }

        public override bool Contains(ref Vector pt)
        {
            return CollisionHelper.Contains(this, ref pt);
        }

        public override bool Collides(BaseCollision collision)
        {
            if (collision is CircleCollision)
            {
                var other = collision as CircleCollision;
                return CollisionHelper.Collides(this, other);
            }

            if (collision is RectangleCollision)
            {
                var other = collision as RectangleCollision;
                return CollisionHelper.Collides(this, other);
            }

            return false;
        }
    }
}
