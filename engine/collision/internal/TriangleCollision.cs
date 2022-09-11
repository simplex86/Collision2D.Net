using System;

namespace SimpleX.Collision2D.Engine
{
    class PolygonCollision : BaseCollision
    {
        internal Polygon geometry;

        //
        internal override Vector position
        {
            get
            {
                var p = points[0];
                for (int i=1; i<points.Length; i++)
                {
                    p += points[i];
                }
                return p / points.Length;
            }
        }
        //
        internal override Vector[] points => geometry.vertics;

        //
        public float angle { get; private set; } = 0;

        public PolygonCollision(Vector[] vertics)
            : base(CollisionType.Polygon)
        {
            geometry = new Polygon()
            {
                vertics = vertics
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

                boundingBox.minx = MinX();
                boundingBox.miny = MinY();
                boundingBox.maxx = MaxX();
                boundingBox.maxy = MaxY();

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
                case CollisionType.Polygon:
                    return CollisionHelper.Overlays(this, collision as PolygonCollision);
                default:
                    break;
            }

            return false;
        }

        private float MinX()
        {
            var x = points[0].x;

            for (int i=1; i<points.Length; i++)
            {
                x = MathX.Min(points[i].x, x);
            }

            return x;
        }

        private float MaxX()
        {
            var x = points[0].x;

            for (int i = 1; i < points.Length; i++)
            {
                x = MathX.Max(points[i].x, x);
            }

            return x;
        }

        private float MinY()
        {
            var y = points[0].y;

            for (int i = 1; i < points.Length; i++)
            {
                y = MathX.Min(points[i].y, y);
            }

            return y;
        }

        private float MaxY()
        {
            var y = points[0].y;

            for (int i = 1; i < points.Length; i++)
            {
                y = MathX.Max(points[i].y, y);
            }

            return y;
        }
    }
}
