using System;

namespace SimpleX.Collision2D.Engine
{
    public static class CollisionFactory
    {
        public static BaseCollision CreateCircleCollision(ref Vector position, float radius)
        {
            var collision = new CircleCollision(position, radius);
            collision.RefreshGeometry();

            return collision;
        }

        public static BaseCollision CreateRectangleCollision(ref Vector position, float width, float height, float angle)
        {
            var collision = new RectangleCollision(position, width, height, angle);
            collision.RefreshGeometry();

            return collision;
        } 

        public static BaseCollision CreateCapsuleCollision(ref Vector position, float length, float radius, float angle)
        {
            var collision = new CapsuleCollision(position, length, radius, angle);
            collision.RefreshGeometry();

            return collision;
        }

        public static BaseCollision CreateTriangleCollision(ref Vector a, ref Vector b, ref Vector c)
        {
            var collision = new TriangleCollision(a, b, c);
            collision.RefreshGeometry();

            return collision;
        }
    }
}
