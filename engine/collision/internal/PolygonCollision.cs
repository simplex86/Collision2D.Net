using System;

namespace SimpleX.Collision2D
{
    class PolygonCollision : BaseCollision
    {
        internal Polygon geometry;

        internal Vector2[] vertics { get; private set; } = null;

        public PolygonCollision(Vector2 position, Vector2[] vertics)
            : base(CollisionType.Polygon)
        {
            geometry = new Polygon()
            {
                vertics = vertics
            };
            transform.position = position;

            this.vertics = new Vector2[vertics.Length];
        }

        public override void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                //if ((dirty & DirtyFlag.Rotation) == DirtyFlag.Rotation)
                //{
                //    for (int i = 0; i < geometry.vertics.Length; i++)
                //    {
                //        var m1 = Matrix.CreateRotationMatrix(transform.rotation * MathX.DEG2RAD);
                //        var m2 = Matrix.CreateTranslationMatrix(ref transform.position);
                //        var mt = m1 * m2;
                //        vertics[i] = Matrix.Transform(ref geometry.vertics[i], ref mt);
                //    }
                //}

                for (int i = 0; i < vertics.Length; i++)
                {
                    vertics[i] = geometry.vertics[i] + transform.position;
                }

                boundingBox.minx = MinX();
                boundingBox.miny = MinY();
                boundingBox.maxx = MaxX();
                boundingBox.maxy = MaxY();

                dirty = DirtyFlag.None;
            }
        }

        public override bool Contains(ref Vector2 pt)
        {
            if (IsAABBContains(ref pt))
            {
                return GeometryHelper.IsPolygonContains(ref geometry, ref transform, ref pt);
            }
            return false;
        }

        public override bool Overlaps(BaseCollision collision)
        {
            switch (collision.type)
            {
                case CollisionType.Circle:
                    return CollisionHelper.Overlaps(this, collision as CircleCollision);
                case CollisionType.Rectangle:
                    return CollisionHelper.Overlaps(this, collision as RectangleCollision);
                case CollisionType.Capsule:
                    return CollisionHelper.Overlaps(this, collision as CapsuleCollision);
                case CollisionType.Polygon:
                    return CollisionHelper.Overlaps(this, collision as PolygonCollision);
                default:
                    break;
            }

            return false;
        }

        public override Vector2 GetFarthestProjectionPoint(ref Vector2 dir)
        {
            var point = vertics[0];
            var max = Vector2.Dot(ref point, ref dir);

            for (int i = 1; i < vertics.Length; i++)
            {
                var dot = Vector2.Dot(ref vertics[i], ref dir);
                if (dot > max)
                {
                    max = dot;
                    point = vertics[i];
                }
            }

            return point;
        }

        private float MinX()
        {
            var x = vertics[0].x;

            for (int i=1; i< vertics.Length; i++)
            {
                x = MathX.Min(vertics[i].x, x);
            }

            return x;
        }

        private float MaxX()
        {
            var x = vertics[0].x;

            for (int i = 1; i < vertics.Length; i++)
            {
                x = MathX.Max(vertics[i].x, x);
            }

            return x;
        }

        private float MinY()
        {
            var y = vertics[0].y;

            for (int i = 1; i < vertics.Length; i++)
            {
                y = MathX.Min(vertics[i].y, y);
            }

            return y;
        }

        private float MaxY()
        {
            var y = vertics[0].y;

            for (int i = 1; i < vertics.Length; i++)
            {
                y = MathX.Max(vertics[i].y, y);
            }

            return y;
        }
    }
}
