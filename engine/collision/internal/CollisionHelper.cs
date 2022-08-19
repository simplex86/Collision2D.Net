using System;

namespace SimpleX.Collision2D.Engine
{
    internal static class CollisionHelper
    {
        // 圆是否包含点
        public static bool Contains(CircleCollision collision, ref Vector pt)
        {
            var dist = collision.position.Distance2(ref pt);
            return dist <= MathX.Pow2(collision.radius);
        }

        // 矩形是否包含点
        public static bool Contains(RectangleCollision collision, ref Vector pt)
        {
            return false;
        }

        // 两个圆是否碰撞
        public static bool Collides(CircleCollision a, CircleCollision b)
        {
            var dist = a.position.Distance(ref b.position);
            return dist <= a.radius + b.radius;
        }

        // 两个矩形是否碰撞
        public static bool Collides(RectangleCollision a, RectangleCollision b)
        {
            if (!IsRectangleProjectionOverlays(a, b)) return false;
            if (!IsRectangleProjectionOverlays(b, a)) return false;

            return true;
        }

        // 圆和矩形是否碰撞
        public static bool Collides(CircleCollision c, RectangleCollision r)
        {
            var m = Matrix.CreateRotationMatrix(-r.angle);
            var p = c.position - r.position;

            p = Matrix.Transform(ref p, ref m);
            p.x = MathX.Abs(p.x);
            p.y = MathX.Abs(p.y);

            var h = new Vector(r.width * 0.5f, r.height * 0.5f);
            var u = p - h;

            u.x = MathX.Max(0, u.x);
            u.y = MathX.Max(0, u.y);

            return u.magnitude2 <= c.radius * c.radius;
        }

        // 圆和矩形是否碰撞
        public static bool Collides(RectangleCollision r, CircleCollision c)
        {
            return Collides(c, r);
        }

        private static bool IsRectangleProjectionOverlays(RectangleCollision a, RectangleCollision b)
        {
            var w = a.width * 0.5f;
            var h = a.height * 0.5f;

            var p1 = new Vector(-w, -h);
            var p3 = new Vector( w, h);
            
            w = b.width * 0.5f;
            h = b.height * 0.5f;

            var p5 = new Vector(-w, -h);
            var p6 = new Vector( w, -h);
            var p7 = new Vector( w,  h);
            var p8 = new Vector(-w,  h);

            var m1 = Matrix.CreateRotationMatrix(b.angle);
            var dp = b.position - a.position;

            p5 = Matrix.Transform(ref p5, ref m1) + dp;
            p6 = Matrix.Transform(ref p6, ref m1) + dp;
            p7 = Matrix.Transform(ref p7, ref m1) + dp;
            p8 = Matrix.Transform(ref p8, ref m1) + dp;

            var m2 = Matrix.CreateRotationMatrix(-a.angle);

            p5 = Matrix.Transform(ref p5, ref m2);
            p6 = Matrix.Transform(ref p6, ref m2);
            p7 = Matrix.Transform(ref p7, ref m2);
            p8 = Matrix.Transform(ref p8, ref m2);

            var x1 = MathX.Min(p5.x, p6.x, p7.x, p8.x);
            var x2 = MathX.Max(p5.x, p6.x, p7.x, p8.x);
            var z1 = MathX.Min(p5.y, p6.y, p7.y, p8.y);
            var z2 = MathX.Max(p5.y, p6.y, p7.y, p8.y);

            if (x2 < p1.x || x1 > p3.x) return false;
            if (z2 < p1.y || z1 > p3.y) return false;

            return true;
        }
    }
}
