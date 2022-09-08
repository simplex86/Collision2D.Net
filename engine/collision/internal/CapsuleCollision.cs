using System;

namespace SimpleX.Collision2D.Engine
{
    internal class CapsuleCollision : BaseCollision
    {
        public float length = 20.0f;
        public float radius = 10.0f;
        public float angle = 0.0f;
        // AABB的顶点
        private Vector[] vertics;

        public CapsuleCollision(Vector position, float length, float radius, float angle)
            : base(CollisionType.Capsule)
        {
            this.position = position;
            this.length = length;
            this.radius = radius;
            this.angle = angle;

            RefreshGeometry();
        }

        public override void Move(Vector delta)
        {   
            for (int i=0; i<vertics.Length; i++)
            {
                vertics[i] += delta;
            }

            base.Move(delta);
        }

        // 旋转
        public override void Rotate(float delta)
        {
            angle += delta;
            dirty |= DirtyFlag.Rotation;
        }

        public override void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                if ((dirty & DirtyFlag.Rotation) == DirtyFlag.Rotation)
                {
                    points = GeometryHelper.GetCapsulePoints(ref position, length, angle);
                    vertics = GeometryHelper.GetRectanglePoints(ref position, length + radius * 2, radius * 2, angle);
                }

                var p1 = vertics[0];
                var p2 = vertics[1];
                var p3 = vertics[2];
                var p4 = vertics[3];

                boundingBox.minx = MathX.Min(p1.x, p2.x, p3.x, p4.x);
                boundingBox.maxx = MathX.Max(p1.x, p2.x, p3.x, p4.x);
                boundingBox.miny = MathX.Min(p1.y, p2.y, p3.y, p4.y);
                boundingBox.maxy = MathX.Max(p1.y, p2.y, p3.y, p4.y);

                dirty = DirtyFlag.None;
            }
        }

        public override bool Contains(ref Vector pt)
        {
            return CollisionHelper.Contains(this, ref pt);
        }

        public override bool Collides(BaseCollision collision)
        {
            switch (collision.type)
            {
                case CollisionType.Circle:
                    return CollisionHelper.Collides(this, collision as CircleCollision);
                case CollisionType.Rectangle:
                    return CollisionHelper.Collides(this, collision as RectangleCollision);
                case CollisionType.Capsule:
                    return CollisionHelper.Collides(this, collision as CapsuleCollision);
                default:
                    break;
            }

            return false;
        }
    }
}
