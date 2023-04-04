namespace SimpleX.Collision2D
{
    public static class ColliderFactory
    {
        // 创建圆形碰撞器
        public static ICollider CreateCircleCollider(Circle circle)
        {
            return new CircleCollider(circle);
        }

        // 创建圆形碰撞器
        public static ICollider CreateCircleCollider(Circle circle, Transform transform)
        {
            var collider = new CircleCollider(circle);
            collider.RefreshGeometry(transform);

            return collider;
        }

        // 创建矩形碰撞器
        public static ICollider CreateRectangleCollider(Rectangle rectangle)
        {
            return new RectangleCollider(rectangle);
        }

        // 创建矩形碰撞器
        public static ICollider CreateRectangleCollider(Rectangle rectangle, Transform transform)
        {
            var collider = new RectangleCollider(rectangle);
            collider.RefreshGeometry(transform);

            return collider;
        }

        // 创建胶囊碰撞器
        public static ICollider CreateCapsuleCollider(Capsule capsule)
        {
            return new CapsuleCollider(capsule);
        }

        // 创建胶囊碰撞器
        public static ICollider CreateCapsuleCollider(Capsule capsule, Transform transform)
        {
            var collider = new CapsuleCollider(capsule);
            collider.RefreshGeometry(transform);

            return collider;
        }

        // 创建凸多边形碰撞器
        public static ICollider CreatePolygonCollider(Polygon polygon)
        {
            return new PolygonCollider(polygon);
        }

        // 创建凸多边形碰撞器
        public static ICollider CreatePolygonCollider(Polygon polygon, Transform transform)
        {
            var collider = new PolygonCollider(polygon);
            collider.RefreshGeometry(transform);

            return collider;
        }

        // 创建椭圆碰撞器
        public static ICollider CreateEllipseCollider(Ellipse ellipse)
        {
            return new EllipseCollider(ellipse);
        }

        // 创建椭圆碰撞器
        public static ICollider CreateEllipseCollider(Ellipse ellipse, Transform transform)
        {
            var collider = new EllipseCollider(ellipse);
            collider.RefreshGeometry(transform);

            return collider;
        }

        // 创建椭圆碰撞器
        public static ICollider CreatePieCollider(Pie pie)
        {
            return new PieCollider(pie);
        }

        // 创建椭圆碰撞器
        public static ICollider CreatePieCollider(Pie pie, Transform transform)
        {
            var collider = new PieCollider(pie);
            collider.RefreshGeometry(transform);

            return collider;
        }
    }
}
