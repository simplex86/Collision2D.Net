﻿namespace SimpleX.Collision2D
{
    public struct Vector2
    {
        public float x;
        public float y;
        internal readonly float w;

        public readonly static Vector2 zero  = new Vector2( 0,  0);
        public readonly static Vector2 one   = new Vector2( 1,  1);
        public readonly static Vector2 left  = new Vector2(-1,  0);
        public readonly static Vector2 right = new Vector2( 1,  0);
        public readonly static Vector2 up    = new Vector2( 0, -1);
        public readonly static Vector2 down  = new Vector2( 0,  1);

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.w = 1;
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
        public Vector2 normalized
        {
            get
            {
                var m = magnitude;
                var u = x / m;
                var v = y / m;
                return new Vector2(u, v);
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

        public static Vector2 Normalize(Vector2 v)
        {
            return v.normalized;
        }

        public static Vector2 Normalize(float x, float y)
        {
            var v = new Vector2(x, y);
            return v.normalized;
        }

        // 两点间的距离
        public float Distance(ref Vector2 v)
        {
            return Distance(ref this, ref v);
        }

        // 两点间的距离
        public static float Distance(ref Vector2 a, ref Vector2 b)
        {
            var d = Distance2(ref a, ref b);
            return MathX.Sqrt(d);
        }

        // 两点间距离的平方
        public float Distance2(ref Vector2 v)
        {
            return Distance2(ref this, ref v);
        }

        // 两点间距离的平方
        public static float Distance2(ref Vector2 a, ref Vector2 b)
        {
            var dx = a.x - b.x;
            var dy = a.y - b.y;
            return dx * dx + dy * dy;
        }

        // 向量点乘
        public float Dot(ref Vector2 v)
        {
            return Dot(ref this, ref v);
        }

        // 向量点乘
        public static float Dot(ref Vector2 a, ref Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        // 向量叉乘
        // 在2D中，向量叉乘没有意义。但为了方便某些计算，定义了叉乘。由W分量表示Z轴
        public float Cross(ref Vector2 v)
        {
            return Cross(ref this, ref v);
        }

        // 向量叉乘
        // 在2D中，向量叉乘没有意义。但为了方便某些计算，定义了叉乘。由W分量表示Z轴
        public static float Cross(ref Vector2 a, ref Vector2 b)
        {
            return a.x * b.y - a.y * b.x;
        }

        // 向量三重积
        public static Vector2 Mul3(ref Vector2 a, ref Vector2 b, ref Vector2 c)
        {
            var z = a.x * b.y - a.y * b.x;
            return new Vector2(-z * c.y, z * c.x);
        }

        // 向量夹角，度
        public float Angle(ref Vector2 v)
        {
            return Angle(ref this, ref v);
        }

        // 向量夹角，度
        public static float Angle(ref Vector2 a, ref Vector2 b)
        {
            var u = a.normalized;
            var v = b.normalized;
            var d = Dot(ref u, ref v);

            return MathX.ACos(d);
        }

        // 反射向量
        public static Vector2 Reflect(ref Vector2 input, ref Vector2 normal)
        {
            var I = input;
            var N = normal.normalized;
            var R = I - 2 * Vector2.Dot(ref I, ref N) * N;

            return R.normalized;
        }

        // 反向向量
        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.x, -v.y);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator *(Vector2 v, float k)
        {
            return new Vector2(v.x * k, v.y * k);
        }

        public static Vector2 operator *(float k, Vector2 v)
        {
            return v * k;
        }

        public static Vector2 operator /(Vector2 v, float k)
        {
            return new Vector2(v.x / k, v.y / k);
        }

        // 通过矩阵M变换向量V
        public static Vector2 Transform(ref Vector2 v, ref Matrix m)
        {
            var x = v.x * m.m11 + v.y * m.m21;
            var y = v.x * m.m12 + v.y * m.m22;

            return new Vector2(x, y);
        }
    }
}
