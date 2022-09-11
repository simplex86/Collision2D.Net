using System;

namespace SimpleX.Collision2D.Engine
{
    public struct Vector
    {
        public float x;
        public float y;
        public float w;

        public readonly static Vector zero  = new Vector( 0,  0);
        public readonly static Vector one   = new Vector( 1,  1);
        public readonly static Vector left  = new Vector(-1,  0);
        public readonly static Vector right = new Vector( 1,  0);
        public readonly static Vector up    = new Vector( 0, -1);
        public readonly static Vector down  = new Vector( 0,  1);

        public Vector(float x, float y)
            : this(x, y, 1)
        {

        }

        private Vector(float x, float y, float w)
        {
            this.x = x;
            this.y = y;
            this.w = w;
        }

        // 向量的模
        public float magnitude
        {
            get { return MathX.Sqrt(magnitude2); }
        }

        // 向量模的平方
        public float magnitude2
        {
            get { return x * x + y * y; }
        }

        // 获得归一化后的单位向量
        public Vector normalized
        {
            get
            {
                var m = magnitude;
                var u = x / m;
                var v = y / m;
                return new Vector(u, v);
            }
        }

        // 取反
        public void Negative()
        {
            x *= -1;
            y *= -1;
        }

        // 归一化
        public void Normalized()
        {
            var m = magnitude;
            x = x / m;
            y = y / m;
        }

        public static Vector Normalize(Vector v)
        {
            return v.normalized;
        }

        public static Vector Normalize(float x, float y)
        {
            var v = new Vector(x, y);
            return v.normalized;
        }

        // 两点间的距离
        public float Distance(ref Vector v)
        {
            return Distance(ref this, ref v);
        }

        // 两点间的距离
        public static float Distance(ref Vector a, ref Vector b)
        {
            var d = Distance2(ref a, ref b);
            return MathX.Sqrt(d);
        }

        // 两点间距离的平方
        public float Distance2(ref Vector v)
        {
            return Distance2(ref this, ref v);
        }

        // 两点间距离的平方
        public static float Distance2(ref Vector a, ref Vector b)
        {
            var dx = a.x - b.x;
            var dy = a.y - b.y;
            return dx * dx + dy * dy;
        }

        // 向量点乘
        public float Dot(ref Vector v)
        {
            return Dot(ref this, ref v);
        }

        // 向量点乘
        public static float Dot(ref Vector a, ref Vector b)
        {
            return a.x * b.x + a.y * b.y;
        }

        // 向量叉乘
        // 在2D中，向量叉乘没有意义。但为了方便某些计算，定义了叉乘。由W分量表示Z轴
        public float Cross(ref Vector v)
        {
            return Cross(ref this, ref v);
        }

        // 向量叉乘
        // 在2D中，向量叉乘没有意义。但为了方便某些计算，定义了叉乘。由W分量表示Z轴
        public static float Cross(ref Vector a, ref Vector b)
        {
            return a.x * b.y - a.y * b.x;
        }

        // 向量夹角，度
        public float Angle(ref Vector v)
        {
            return Angle(ref this, ref v);
        }

        // 向量夹角，度
        public static float Angle(ref Vector a, ref Vector b)
        {
            var u = a.normalized;
            var v = b.normalized;
            var d = Dot(ref u, ref v);

            return MathX.ACos(d);
        }

        // 反射向量
        public static Vector Reflect(ref Vector input, ref Vector normal)
        {
            var I = input;
            var N = normal.normalized;
            var R = I - 2 * Vector.Dot(ref I, ref N) * N;

            return R.normalized;
        }

        // 反向向量
        public static Vector operator -(Vector v)
        {
            return new Vector(-v.x, -v.y);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y);
        }

        public static Vector operator *(Vector v, float k)
        {
            return new Vector(v.x * k, v.y * k);
        }

        public static Vector operator *(float k, Vector v)
        {
            return v * k;
        }

        public static Vector operator /(Vector v, float k)
        {
            return new Vector(v.x / k, v.y / k);
        }

        // 通过矩阵M变换向量V
        public static Vector Transform(ref Vector v, ref Matrix m)
        {
            var x = v.x * m.m11 + v.y * m.m21;
            var y = v.x * m.m12 + v.y * m.m22;

            return new Vector(x, y);
        }
    }
}
