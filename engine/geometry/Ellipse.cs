using System;

namespace SimpleX.Collision2D
{
    public struct Ellipse
    {
        public float width;
        public float height;

        public float A => width * 0.5f;
        public float B => height * 0.5f;

        //public Vector2[] vertics;
    }
}
