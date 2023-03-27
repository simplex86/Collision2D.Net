namespace SimpleX.Collision2D
{
    public struct Matrix
    {
        public float m11;
        public float m12;
        public float m13;
        public float m21;
        public float m22;
        public float m23;
        public float m31;
        public float m32;
        public float m33;

        public Matrix(float m11, float m12, float m13,
                      float m21, float m22, float m23,
                      float m31, float m32, float m33)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
        }

        public Matrix(Matrix matrix)
        {
            m11 = matrix.m11;
            m12 = matrix.m12;
            m13 = matrix.m13;
            m21 = matrix.m21;
            m22 = matrix.m22;
            m23 = matrix.m23;
            m31 = matrix.m31;
            m32 = matrix.m32;
            m33 = matrix.m33;
        }

        public static Matrix operator* (Matrix a, Matrix b)
        {
            var m11 = a.m11 * b.m11 + a.m12 * b.m21 + a.m13 * b.m31;
            var m12 = a.m11 * b.m12 + a.m12 * b.m22 + a.m13 * b.m32;
            var m13 = a.m11 * b.m13 + a.m12 * b.m23 + a.m13 * b.m33;
            var m21 = a.m21 * b.m11 + a.m22 * b.m21 + a.m23 * b.m31;
            var m22 = a.m21 * b.m12 + a.m22 * b.m22 + a.m23 * b.m32;
            var m23 = a.m21 * b.m13 + a.m22 * b.m23 + a.m23 * b.m33;
            var m31 = a.m31 * b.m11 + a.m32 * b.m21 + a.m33 * b.m31;
            var m32 = a.m31 * b.m12 + a.m32 * b.m22 + a.m33 * b.m32;
            var m33 = a.m31 * b.m13 + a.m32 * b.m23 + a.m13 * b.m33;

            return new Matrix(m11, m12, m13,
                              m21, m22, m23,
                              m31, m32, m33);
        }

        public static Matrix CreateTranslationMatrix(ref Vector2 p)
        {
            return CreateTranslationMatrix(p.x, p.y);
        }

        public static Matrix CreateTranslationMatrix(float x, float y)
        {
            var matrix = new Matrix(1, 0, 0,
                                    0, 1, 0,
                                    x, y, 1);
            return matrix;
        }

        public static Matrix CreateRotationMatrix(float radian)
        {
            var s = MathX.Sin(radian);
            var c = MathX.Cos(radian);

            var matrix = new Matrix( c, s, 0,
                                    -s, c, 0,
                                     0, 0, 1);
            return matrix;
        }

        public static Vector2 Transform(ref Vector2 vector, ref Matrix matrix)
        {
            var x = ((vector.x * matrix.m11) + (vector.y * matrix.m21)) + (vector.w * matrix.m31);
            var y = ((vector.x * matrix.m12) + (vector.y * matrix.m22)) + (vector.w * matrix.m32);
            //var z = ((vector.x * matrix.m13) + (vector.y * matrix.m23)) + (vector.w * matrix.m33);

            return new Vector2(x, y);

        }
    }
}
