using System;

namespace SimpleX.Collision2D.Engine
{
    public static class MathX
    {
        // 圆周率
        public const float PI = 3.14159265f;
        // 角度转弧度的参数
        public const float DEG2RAD = PI / 180.0f;

        // 绝对值
        public static float Abs(float v)
        {
            return (float)Math.Abs(v);
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

        // （2个值中的）最大值
        public static float Max(float a, float b)
        {
            return (float)Math.Max(a, b);
        }

        // （3个值中的）最大值
        public static float Max(float a, float b, float c)
        {
            return Max(Max(a, b), c);
        }

        // （4个值中的）最大值
        public static float Max(float a, float b, float c, float d)
        {
            return Max(Max(a, b, c), d);
        }

        // （2个值中的）最小值
        public static float Min(float a, float b)
        {
            return (float)Math.Min(a, b);
        }

        // （3个值中的）最小值
        public static float Min(float a, float b, float c)
        {
            return Min(Min(a, b), c);
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
            return (float)Math.Pow(v, 2);
        }

        
        public static bool Equals(int a, int b)
        {
            return a == b;
        }

        public static bool Equals(float a, float b)
        {
            return Abs(a - b) <= float.Epsilon;
        }

        public static float Clamp(float num, float min, float max)
        {
            return Min(max, Max(num, min));
        }
    }
}
