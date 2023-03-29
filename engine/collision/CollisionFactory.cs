using System;

namespace SimpleX.Collision2D
{
    public static class CollisionFactory
    {
        // 创建圆形碰撞器
        public static BaseCollision CreateCircleCollision(ref Vector2 position, ref Circle circle)
        {
            var collision = new CircleCollision(circle, position);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建矩形碰撞器
        public static BaseCollision CreateRectangleCollision(ref Vector2 position, ref Rectangle rectangle, float rotation)
        {
            var collision = new RectangleCollision(rectangle, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建胶囊碰撞器
        public static BaseCollision CreateCapsuleCollision(ref Vector2 position, ref Capsule capsule, float rotation)
        {
            var collision = new CapsuleCollision(capsule, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建凸多边形碰撞器
        public static BaseCollision CreatePolygonCollision(ref Vector2 position, ref Polygon polygon, float rotation)
        {
            var collision = new PolygonCollision(polygon, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建椭圆碰撞器
        public static BaseCollision CreateEllipseCollision(ref Vector2 position, ref Ellipse ellipse, float rotation)
        {
            var collision = new EllipseCollision(ellipse, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }
    }
}
