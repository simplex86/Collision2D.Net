using System;

namespace SimpleX.Collision2D
{
    public static class CollisionFactory
    {
        // 创建圆形碰撞器
        public static BaseCollision CreateCircleCollision(Vector2 position, Circle circle)
        {
            var collision = new CircleCollision(circle, position);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建矩形碰撞器
        public static BaseCollision CreateRectangleCollision(Vector2 position, Rectangle rectangle, float rotation)
        {
            var collision = new RectangleCollision(rectangle, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建胶囊碰撞器
        public static BaseCollision CreateCapsuleCollision(Vector2 position, Capsule capsule, float rotation)
        {
            var collision = new CapsuleCollision(capsule, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建凸多边形碰撞器
        public static BaseCollision CreatePolygonCollision(Vector2 position, Polygon polygon, float rotation)
        {
            var collision = new PolygonCollision(polygon, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建椭圆碰撞器
        public static BaseCollision CreateEllipseCollision(Vector2 position, Ellipse ellipse, float rotation)
        {
            var collision = new EllipseCollision(ellipse, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建椭圆碰撞器
        public static BaseCollision CreatePieCollision(Vector2 position, Pie pie, float rotation)
        {
            var collision = new PieCollision(pie, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }
    }
}
