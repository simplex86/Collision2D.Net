using System;

namespace SimpleX.Collision2D
{
    struct Ellipse
    {
        public float width;
        public float height;
        public float angle;

        public float A => width * 0.5f;
        public float B => height * 0.5f;

        //public Vector2[] vertics;
    }
}
