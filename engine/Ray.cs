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
        public bool Hit(BaseCollision collision)
        {
            switch (collision.type)
            {
                case CollisionType.Circle:
                    return Hit(collision as CircleCollision);
                case CollisionType.Rectangle:
                    return Hit(collision as RectangleCollision);
                case CollisionType.Capsule:
                    return Hit(collision as CapsuleCollision);
                default:
                    break;
            }
            return false;
        }

        // 是否和碰撞体相交（限制检测长度不大于length）
        public bool Hit(BaseCollision collision, float length)
        {
            length = MathX.Max(0.001f, length);

            switch (collision.type)
            {
                case CollisionType.Circle:
                    return Hit(collision as CircleCollision, length);
                case CollisionType.Rectangle:
                    return Hit(collision as RectangleCollision);
                case CollisionType.Capsule:
                    return Hit(collision as CapsuleCollision);
                default:
                    break;
            }
            return false;
        }

        private bool Hit(CircleCollision collision)
        {
            return Hit(collision, float.MaxValue);
        }

        private bool Hit(CircleCollision collision, float length)
        {
            var p = collision.position;
            var r = collision.radius;
            var d = direction;

            var m = position - p;
            var c = Vector.Dot(ref m, ref m) - r * r;
            var b = Vector.Dot(ref m, ref d);
            var disc = b * b - c;

            if (disc < 0) return false;

            var t = -b - MathX.Sqrt(disc);
            if (t >= 0 && t <= length) return true;

            return false;
        }

        private bool Hit(RectangleCollision collision)
        {
            return false;
        }

        private bool Hit(CapsuleCollision collision)
        {
            return false;
        }
    }
}
