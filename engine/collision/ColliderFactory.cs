using System.Drawing;

namespace SimpleX.Collision2D
{
    public static class ColliderFactory
    {
        // 创建圆形碰撞器
        public static ICollider CreateCollider(Circle circle)
        {
            return new CircleCollider(circle);
        }

        // 创建圆形碰撞器
        public static ICollider CreateCollider(Circle circle, Transform transform)
        {
            var collider = new CircleCollider(circle);
            collider.RefreshGeometry(transform.rotation);

            return collider;
        }

        // 创建矩形碰撞器
        public static ICollider CreateCollider(Rectangle rectangle)
        {
            return new RectangleCollider(rectangle);
        }

        // 创建矩形碰撞器
        public static ICollider CreateCollider(Rectangle rectangle, Transform transform)
        {
            var collider = new RectangleCollider(rectangle);
            collider.RefreshGeometry(transform.rotation);

            return collider;
        }

        // 创建胶囊碰撞器
        public static ICollider CreateCollider(Capsule capsule)
        {
            return new CapsuleCollider(capsule);
        }

        // 创建胶囊碰撞器
        public static ICollider CreateCollider(Capsule capsule, Transform transform)
        {
            var collider = new CapsuleCollider(capsule);
            collider.RefreshGeometry(transform.rotation);

            return collider;
        }

        // 创建凸多边形碰撞器
        public static ICollider CreateCollider(Polygon polygon)
        {
            return new PolygonCollider(polygon);
        }

        // 创建凸多边形碰撞器
        public static ICollider CreateCollider(Polygon polygon, Transform transform)
        {
            var collider = new PolygonCollider(polygon);
            collider.RefreshGeometry(transform.rotation);

            return collider;
        }

        // 创建椭圆碰撞器
        public static ICollider CreateCollider(Ellipse ellipse)
        {
            return new EllipseCollider(ellipse);
        }

        // 创建椭圆碰撞器
        public static ICollider CreateCollider(Ellipse ellipse, Transform transform)
        {
            var collider = new EllipseCollider(ellipse);
            collider.RefreshGeometry(transform.rotation);

            return collider;
        }

        // 创建椭圆碰撞器
        public static ICollider CreateCollider(Pie pie)
        {
            return new PieCollider(pie);
        }

        // 创建椭圆碰撞器
        public static ICollider CreateCollider(Pie pie, Transform transform)
        {
            var collider = new PieCollider(pie);
            collider.RefreshGeometry(transform.rotation);

            return collider;
        }

        // 创建线段碰撞器
        public static ICollider CreateCollider(Segment segment)
        {
            return new SegmentCollider(segment);
        }

        // 创建线段碰撞器
        public static ICollider CreateCollider(Segment segment, Transform transform)
        {
            var collider = new SegmentCollider(segment);
            collider.RefreshGeometry(transform.rotation);

            return collider;
        }
    }
}
