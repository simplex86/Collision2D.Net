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
            return false;
        }
    }
}
