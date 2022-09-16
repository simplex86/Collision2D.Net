using System;

namespace SimpleX.Collision2D
{
    public static class CollisionFactory
    {
        // 创建圆形碰撞器
        public static BaseCollision CreateCircleCollision(ref Vector2 position, float radius)
        {
            var collision = new CircleCollision(position, radius);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建矩形碰撞器
        public static BaseCollision CreateRectangleCollision(ref Vector2 position, float width, float height, float angle)
        {
            var collision = new RectangleCollision(position, width, height, angle);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建胶囊碰撞器
        public static BaseCollision CreateCapsuleCollision(ref Vector2 position, float length, float radius, float angle)
        {
            var collision = new CapsuleCollision(position, length, radius, angle);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建凸多边形碰撞器
        public static BaseCollision CreatePolygonCollision(Vector2[] vertics)
        {
            var collision = new PolygonCollision(vertics);
            collision.RefreshGeometry();

            return collision;
        }
    }
}
