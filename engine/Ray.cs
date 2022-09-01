using System;

namespace SimpleX.Collision2D.Engine
{
    // 射线
    public class Ray
    {
        public Vector position { get; private set; } = Vector.zero;
        public Vector direction { get; private set; } = Vector.left;

        public Ray(Vector position, Vector direction)
        {
            this.position = position;
            this.direction = direction;

            this.direction.Normalized();
        }

        // 是否和碰撞体相交（不限检测长度）
        public bool Cast(BaseCollision collision)
        {
            switch (collision.type)
            {
                case CollisionType.Circle:
                    return Cast(collision as CircleCollision);
                case CollisionType.Rectangle:
                    return Cast(collision as RectangleCollision);
                case CollisionType.Capsule:
                    return Cast(collision as CapsuleCollision);
                default:
                    break;
            }
            return false;
        }

        // 是否和碰撞体相交（限制检测长度不大于length）
        public bool Cast(BaseCollision collision, float length)
        {
            length = MathX.Max(0.001f, length);

            switch (collision.type)
            {
                case CollisionType.Circle:
                    return Cast(collision as CircleCollision, length);
                case CollisionType.Rectangle:
                    return Cast(collision as RectangleCollision, length);
                case CollisionType.Capsule:
                    return Cast(collision as CapsuleCollision, length);
                default:
                    break;
            }
            return false;
        }

        private bool Cast(CircleCollision collision, float length = float.MaxValue)
        {
            var p = collision.position;
            var r = collision.radius;
            var d = direction;

            var m = position - p;
            var c = Vector.Dot(ref m, ref m) - r * r;
            var b = Vector.Dot(ref m, ref d);

            var disc = b * b - c;
            if (disc >= 0)
            {
                var t = -b - MathX.Sqrt(disc);
                if (t >= 0 && t <= length) return true;
            }

            return false;
        }

        private bool Cast(RectangleCollision collision, float length = float.MaxValue)
        {
            return false;
        }

        private bool Cast(CapsuleCollision collision, float length = float.MaxValue)
        {
            var m = new Matrix();
            return false;
        }

        private bool Cast(ref AABB box, float length)
        {
            if (MathX.Equals(direction.x, 0.0f))
            {
                return position.x >= box.minx && position.x <= box.maxx;
            }

            if (MathX.Equals(direction.y, 0.0f))
            {
                return position.y >= box.miny && position.y <= box.maxy;
            }

            var iv = new Vector(1.0f / direction.x, 1.0f / direction.y);
            var s1 = new Vector(box.minx - position.x, box.miny - position.y);
            var s2 = new Vector(box.maxx - position.x, box.maxy - position.y);

            var d1 = Vector.Mul(ref s1, ref iv);
            var d2 = Vector.Mul(ref s2, ref iv);
            var v1 = Vector.Min(ref d1, ref d2);
            var v2 = Vector.Max(ref d1, ref d2);

            var L = MathX.Max(v1.x, v1.y);
            var H = MathX.Min(v2.x, v2.y);

            if (H >= 0 && H >= L && L <= length)
            {
                return true;
            }

            return false;
        }
    }
}
