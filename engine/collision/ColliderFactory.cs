using System;

namespace SimpleX.Collision2D
{
    public static class ColliderFactory
    {
        // 创建圆形碰撞器
        public static IBaseCollider CreateCircleCollider(Circle circle, Vector2 position)
        {
            var collider = new CircleCollider(circle, position);
            collider.RefreshGeometry();

            return collider;
        }

        // 创建矩形碰撞器
        public static IBaseCollider CreateRectangleCollider(Rectangle rectangle, Vector2 position, float rotation)
        {
            var collider = new RectangleCollider(rectangle, position, rotation);
            collider.RefreshGeometry();

            return collider;
        }

        // 创建胶囊碰撞器
        public static IBaseCollider CreateCapsuleCollider(Capsule capsule, Vector2 position, float rotation)
        {
            var collider = new CapsuleCollider(capsule, position, rotation);
            collider.RefreshGeometry();

            return collider;
        }

        // 创建凸多边形碰撞器
        public static IBaseCollider CreatePolygonCollider(Polygon polygon, Vector2 position, float rotation)
        {
            var collider = new PolygonCollider(polygon, position, rotation);
            collider.RefreshGeometry();

            return collider;
        }

        // 创建椭圆碰撞器
        public static IBaseCollider CreateEllipseCollider(Ellipse ellipse, Vector2 position, float rotation)
        {
            var collider = new EllipseCollider(ellipse, position, rotation);
            collider.RefreshGeometry();

            return collider;
        }

        // 创建椭圆碰撞器
        public static IBaseCollider CreatePieCollider(Pie pie, Vector2 position, float rotation)
        {
            var collider = new PieCollider(pie, position, rotation);
            collider.RefreshGeometry();

            return collider;
        }
    }
}
