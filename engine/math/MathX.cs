namespace SimpleX.Collision2D
{
    using Math = System.Math;

    public static class MathX
    {
        // Π
        public const float PI = (float)Math.PI;
        // 角度转弧度的参数
        public const float DEG2RAD = PI / 180.0f;
        // 弧度转角度的参数
        public const float RAD2DEG = 180.0f / PI;
        // 误差（数值判断精度）
        public static readonly float EPSILON = 0.000001f;

        static MathX()
        {
            EPSILON = 0.5f;
            while (1f + EPSILON > 1f)
            {
                EPSILON *= 0.5f;
            }
        }

        // 绝对值
        public static int Abs(int v)
        {
            return Math.Abs(v);
        }

        // 绝对值
        public static float Abs(float v)
        {
            return Math.Abs(v);
        }

        // 正弦
        public static float Sin(float radian)
        {
            return (float)Math.Sin(radian);
        }

        // 余弦
        public static float Cos(float radian)
        {
            return (float)Math.Cos(radian);
        }

        // 反余弦
        public static float ACos(float cos)
        {
            return (float)Math.Acos(cos);
        }

        // 
        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
        }

        // （2个值中的）最大值
        public static int Max(int a, int b)
        {
            return Math.Max(a, b);
        }

        // （2个值中的）最大值
        public static float Max(float a, float b)
        {
            return Math.Max(a, b);
        }

        // （3个值中的）最大值
        public static int Max(int a, int b, int c)
        {
            return Max(Max(a, b), c);
        }

        // （3个值中的）最大值
        public static float Max(float a, float b, float c)
        {
            return Max(Max(a, b), c);
        }

        // （4个值中的）最大值
        public static float Max(int a, int b, int c, int d)
        {
            return Max(Max(a, b, c), d);
        }

        // （4个值中的）最大值
        public static float Max(float a, float b, float c, float d)
        {
            return Max(Max(a, b, c), d);
        }

        // （2个值中的）最小值
        public static int Min(int a, int b)
        {
            return Math.Min(a, b);
        }

        // （2个值中的）最小值
        public static float Min(float a, float b)
        {
            return Math.Min(a, b);
        }

        // （3个值中的）最小值
        public static int Min(int a, int b, int c)
        {
            return Min(Min(a, b), c);
        }

        // （3个值中的）最小值
        public static float Min(float a, float b, float c)
        {
            return Min(Min(a, b), c);
        }

        // （4个值中的）最小值
        public static int Min(int a, int b, int c, int d)
        {
            return Min(Min(a, b, c), d);
        }

        // （4个值中的）最小值
        public static float Min(float a, float b, float c, float d)
        {
            return Min(Min(a, b, c), d);
        }

        // 四舍五入
        public static int Round(float v)
        {
            return (int)(v + 0.5f);
        }

        // 四舍五入
        public static float Roundf(float v)
        {
            return Floor(v + 0.5f);
        }

        // 开方
        public static float Sqrt(float v)
        {
            return (float)Math.Sqrt(v);
        }

        // N次方
        public static float Pow(float v, int n)
        {
            return (float)Math.Pow(v, n);
        }

        // 平方
        public static float Pow2(float v)
        {
            return Pow(v, 2);
        }

        // 两个整数是否相等
        public static bool Equals(int a, int b)
        {
            return a == b;
        }

        // 两个浮点数是否相等
        public static bool Equals(float a, float b)
        {
            return Abs(a - b) < EPSILON;
        }

        // 将num限制在[min, max]范围内
        public static int Clamp(int num, int min, int max)
        {
            return Min(max, Max(num, min));
        }

        // 将num限制在[min, max]范围内
        public static float Clamp(float num, float min, float max)
        {
            return Min(max, Max(num, min));
        }

        // 将num限制在[0, 1]范围内
        public static float Clamp01(float num)
        {
            return Clamp(num, 0f, 1f);
        }

        // 将angle限制在[0, 360)范围内
        public static float Clamp360(float angle)
        {
            while (angle < 0)
            {
                angle = 360 + angle;
            }
            while (angle >= 360)
            {
                angle = angle - 360;
            }

            return angle;
        }

        // 获取value的符号
        public static int Sign(float value)
        {
            return (value >= 0) ? 1 : -1;
        }

        // 获取不大于value的最大整数值
        public static float Floor(float value)
        {
            return (float)Math.Floor(value);
        }

        // 获取不小于value的最小整数值
        public static float Ceiling(float value)
        {
            return (float)Math.Ceiling(value);
        }
    }
}
