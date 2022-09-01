using System;

namespace SimpleX.Collision2D.Engine
{
    public struct Vector
    {
        public float x;
        public float y;
        public float w { get; private set; }

        public static Vector zero   { get; } = new Vector(0, 0);
        public static Vector one    { get; } = new Vector(1, 1);
        public static Vector left   { get; } = new Vector(-1, 0);
        public static Vector right  { get; } = new Vector(1, 0);
        public static Vector up     { get; } = new Vector(0, -1);
        public static Vector down   { get; } = new Vector(0, 1);

        public Vector(float x, float y)
            : this(x, y, 1)
        {

        }

        public Vector(Vector v)
            : this(v.x, v.y, v.w)
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

        // 归一化
        public void Normalized()
        {
            var m = magnitude;
            x = x / m;
            y = y / m;
        }

        public static Vector Normalize(ref Vector v)
        {
            return v.normalized;
        }

        public static Vector Normalize(float x, float y)
        {
            var v = new Vector(x, y);
            v.Normalized();

            return v;
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
        public Vector Cross(ref Vector v)
        {
            return Cross(ref this, ref v);
        }

        // 向量叉乘
        // 在2D中，向量叉乘没有意义。但为了方便某些计算，定义了叉乘。由W分量表示Z轴
        public static Vector Cross(ref Vector a, ref Vector b)
        {
            var x = a.y * b.w - b.y * a.w;
            var y = a.w * b.x - a.x * b.w;
            var w = a.x * b.y - a.y * b.x;

            return new Vector(x, y, w);
        }

        public Vector Mul(ref Vector v)
        {
            return Mul(ref this, ref v);
        }

        public static Vector Mul(ref Vector a, ref Vector b)
        {
            var x = a.x * b.x;
            var y = a.y * b.y;

            return new Vector(x, y);
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

        public static Vector Min(ref Vector a, ref Vector b)
        {
            var x = MathX.Min(a.x, b.x);
            var y = MathX.Min(a.y, b.y);
            return new Vector(x, y);
        }

        public static Vector Max(ref Vector a, ref Vector b)
        {
            var x = MathX.Max(a.x, b.x);
            var y = MathX.Max(a.y, b.y);
            return new Vector(x, y);
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

        // 逆时针旋转90度
        public void CounterClockwise90()
        {
            var X = x;
            x = y;
            y = -X;
        }

        // 逆时针旋转90度
        public static Vector CounterClockwise90(ref Vector v)
        {
            var u = new Vector(v);
            u.CounterClockwise90();

            return u;
        }

        public void Skew()
        {
            var X = x;
            x = -y;
            y = x;
        }

        public static Vector Skew(ref Vector v)
        {
            var u = new Vector(v);
            u.Skew();

            return u;
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
