using System.Security.Policy;

namespace SimpleX.Collision2D
{
    public struct Vector2
    {
        public float x;
        public float y;
        internal readonly float w;

        public readonly static Vector2 zero = new Vector2(0, 0);
        public readonly static Vector2 one = new Vector2(1, 1);
        public readonly static Vector2 left = new Vector2(-1, 0);
        public readonly static Vector2 right = new Vector2(1, 0);
        public readonly static Vector2 up = new Vector2(0, -1);
        public readonly static Vector2 down = new Vector2(0, 1);

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

        // 反向向量
        public Vector2 negatived
        {
            get { return new Vector2(-x, -y); }
        }

        public Vector2 perpendicular
        {
            get { return Perpendicular(); }
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

        // 归一化
        public static Vector2 Normalize(Vector2 v)
        {
            return v.normalized;
        }

        // 归一化
        public static Vector2 Normalize(float x, float y)
        {
            var v = new Vector2(x, y);
            return v.normalized;
        }

        // 垂直向量
        public Vector2 Perpendicular()
        {
            return Perpendicular(this);
        }

        // 垂直向量
        public static Vector2 Perpendicular(Vector2 v)
        {
            return Perpendicular(v.x, v.y);
        }

        // 垂直向量
        public static Vector2 Perpendicular(float x, float y)
        {
            return new Vector2(y, -x);
        }

        // 两点间的距离
        public float Distance(Vector2 v)
        {
            return Distance(this, v);
        }

        // 两点间的距离
        public static float Distance(Vector2 a, Vector2 b)
        {
            var d = Distance2(a, b);
            return MathX.Sqrt(d);
        }

        // 两点间距离的平方
        public float Distance2(Vector2 v)
        {
            return Distance2(this, v);
        }

        // 两点间距离的平方
        public static float Distance2(Vector2 a, Vector2 b)
        {
            var dx = a.x - b.x;
            var dy = a.y - b.y;
            return dx * dx + dy * dy;
        }

        // 向量点乘
        public float Dot(Vector2 v)
        {
            return Dot(this, v);
        }

        // 向量点乘
        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        // 向量叉乘
        // 在2D中，向量叉乘没有意义。但为了方便某些计算，定义了叉乘。由W分量表示Z轴
        public float Cross(Vector2 v)
        {
            return Cross(this, v);
        }

        // 向量叉乘
        // 在2D中，向量叉乘没有意义。但为了方便某些计算，定义了叉乘。由W分量表示Z轴
        public static float Cross(Vector2 a, Vector2 b)
        {
            return a.x * b.y - a.y * b.x;
        }

        // 向量三重积
        public static Vector2 Mul3(Vector2 a, Vector2 b, Vector2 c)
        {
            float ac = a.x * c.x + a.y * c.y; // a.dot(c)
            float bc = b.x * c.x + b.y * c.y; // b.dot(c)

            // b * a.dot(c) - a * b.dot(c)
            var x = b.x * ac - a.x * bc;
            var y = b.y * ac - a.y * bc;

            return new Vector2(x, y);
        }

        // 向量与X轴的夹角，弧度
        public float Angle()
        {
            return Angle(right);
        }

        // 向量夹角，弧度
        public float Angle(Vector2 v)
        {
            return Angle(this, v);
        }

        // 向量夹角，弧度
        public static float Angle(Vector2 a, Vector2 b)
        {
            var u = a.normalized;
            var v = b.normalized;
            var d = Dot(u, v);

            return MathX.ACos(d);
        }

        public float Theta()
        {
            return MathX.Atan2(y, x);
        }

        // 反射向量
        public static Vector2 Reflect(Vector2 input, Vector2 normal)
        {
            var I = input;
            var N = normal.normalized;
            var R = I - 2 * Dot(I, N) * N;

            return R.normalized;
        }

        public void CW90()
        {
            var t = x;
            x = -y;
            y = t;
        }

        public void CCW90()
        {
            var t = x;
            x = t;
            y = -t;
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
        public static Vector2 Transform(Vector2 v, Matrix m)
        {
            var x = v.x * m.m11 + v.y * m.m21;
            var y = v.x * m.m12 + v.y * m.m22;

            return new Vector2(x, y);
        }
    }
}
