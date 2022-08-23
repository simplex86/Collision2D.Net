using System;

namespace SimpleX.Collision2D.Engine
{
    public static class CollisionFactory
    {
        public static BaseCollision CreateCircleCollision(ref Vector position, float radius)
        {
            return new CircleCollision(ref position, radius);
        }

        public static BaseCollision CreateRectangleCollision(ref Vector position, float width, float height, float angle)
        {
            return new RectangleCollision(ref position, width, height, angle);
        } 

        public static BaseCollision CreateCapsuleCollision(ref Vector position, float length, float radius, float angle)
        {
            return new CapsuleCollision(ref position, length, radius, angle);
        }
    }
}
