using System;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    class MovementComponent
    {
        public Vector direction { get; set; } = Vector.right;
        public float speed { get; set; } = 1.0f;
    }
}
