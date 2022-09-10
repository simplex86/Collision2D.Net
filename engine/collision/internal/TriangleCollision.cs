using System;

namespace SimpleX.Collision2D.Engine
{
    class TriangleCollision : BaseCollision
    {
        internal Triangle geometry;

        //
        internal override Vector position => (points[0] + points[1] + points[2]) / 3.0f;
        //
        internal override Vector[] points => geometry.vertics;

        //
        public float angle { get; private set; } = 0;

        public TriangleCollision(Vector a, Vector b, Vector c)
            : base(CollisionType.Triangle)
        {
            geometry = new Triangle()
            {
                vertics = new Vector[] { a, b, c }
            };
        }

        public override void Rotate(float delta)
        {
            //angle += delta;
            //dirty |= DirtyFlag.Rotation;
        }

        public override void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                if ((dirty & DirtyFlag.Rotation) == DirtyFlag.Rotation)
                {

                }

                var a = geometry.vertics[0];
                var b = geometry.vertics[1];
                var c = geometry.vertics[2];

                boundingBox.minx = MathX.Min(a.x, b.x, c.x);
                boundingBox.miny = MathX.Min(a.y, b.y, c.y);
                boundingBox.maxx = MathX.Max(a.x, b.x, c.x);
                boundingBox.maxy = MathX.Max(a.y, b.y, c.y);

                dirty = DirtyFlag.None;
            }
        }

        public override bool Contains(ref Vector pt)
        {
            if (IsAABBContains(ref pt))
            {

            }
            return false;
        }

        public override bool Overlays(BaseCollision collision)
        {
            switch (collision.type)
            {
                case CollisionType.Circle:
                    return CollisionHelper.Overlays(this, collision as CircleCollision);
                case CollisionType.Rectangle:
                    return CollisionHelper.Overlays(this, collision as RectangleCollision);
                case CollisionType.Capsule:
                    return CollisionHelper.Overlays(this, collision as CapsuleCollision);
                case CollisionType.Triangle:
                    return CollisionHelper.Overlays(this, collision as TriangleCollision);
                default:
                    break;
            }

            return false;
        }
    }
}
