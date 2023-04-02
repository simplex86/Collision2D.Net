using System;

namespace SimpleX.Collision2D
{
    public static class CollisionFactory
    {
        // 创建圆形碰撞器
        public static IBaseCollision CreateCircleCollision(Circle circle, Vector2 position)
        {
            var collision = new CircleCollision(circle, position);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建矩形碰撞器
        public static IBaseCollision CreateRectangleCollision(Rectangle rectangle, Vector2 position, float rotation)
        {
            var collision = new RectangleCollision(rectangle, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建胶囊碰撞器
        public static IBaseCollision CreateCapsuleCollision(Capsule capsule, Vector2 position, float rotation)
        {
            var collision = new CapsuleCollision(capsule, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建凸多边形碰撞器
        public static IBaseCollision CreatePolygonCollision(Polygon polygon, Vector2 position, float rotation)
        {
            var collision = new PolygonCollision(polygon, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建椭圆碰撞器
        public static IBaseCollision CreateEllipseCollision(Ellipse ellipse, Vector2 position, float rotation)
        {
            var collision = new EllipseCollision(ellipse, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }

        // 创建椭圆碰撞器
        public static IBaseCollision CreatePieCollision(Pie pie, Vector2 position, float rotation)
        {
            var collision = new PieCollision(pie, position, rotation);
            collision.RefreshGeometry();

            return collision;
        }
    }
}
