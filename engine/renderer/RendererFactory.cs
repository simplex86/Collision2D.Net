using System.Drawing;

namespace SimpleX.Collision2D
{
    public static class RendererFactory
    {
        // AABB渲染器
        public static IRenderer CreateAABBRenderer()
        {
            return new AABBRenderer();
        }

        // 速度渲染器
        public static IRenderer CreateVelocityRenderer()
        {
            return new VelocityRenderer();
        }

        // 创建圆形渲染器
        public static IRenderer CreateRenderer(Circle circle)
        {
            var renderer = new CircleRenderer(circle);
            return renderer;
        }

        // 创建圆形渲染器
        public static IRenderer CreateRenderer(Circle circle, Color color)
        {
            var renderer = new CircleRenderer(circle, color);
            return renderer;
        }

        // 创建矩形渲染器
        public static IRenderer CreateRenderer(Rectangle rectangle)
        {
            var renderer = new RectangleRenderer(rectangle);
            return renderer;
        }

        // 创建矩形渲染器
        public static IRenderer CreateRenderer(Rectangle rectangle, Color color)
        {
            var renderer = new RectangleRenderer(rectangle, color);
            return renderer;
        }

        // 创建胶囊渲染器
        public static IRenderer CreateRenderer(Capsule capsule)
        {
            var renderer = new CapsuleRenderer(capsule);
            return renderer;
        }

        // 创建胶囊渲染器
        public static IRenderer CreateRenderer(Capsule capsule, Color color)
        {
            var renderer = new CapsuleRenderer(capsule, color);
            return renderer;
        }

        // 创建椭圆渲染器
        public static IRenderer CreateRenderer(Polygon polygon)
        {
            var renderer = new PolygonRenderer(polygon);
            return renderer;
        }

        // 创建椭圆渲染器
        public static IRenderer CreateRenderer(Polygon polygon, Color color)
        {
            var renderer = new PolygonRenderer(polygon, color);
            return renderer;
        }

        // 创建椭圆渲染器
        public static IRenderer CreateRenderer(Ellipse ellipse)
        {
            var renderer = new EllipseRenderer(ellipse);
            return renderer;
        }

        // 创建椭圆渲染器
        public static IRenderer CreateRenderer(Ellipse ellipse, Color color)
        {
            var renderer = new EllipseRenderer(ellipse, color);
            return renderer;
        }

        // 创建扇形渲染器
        public static IRenderer CreateRenderer(Pie pie)
        {
            var renderer = new PieRenderer(pie);
            return renderer;
        }

        // 创建扇形渲染器
        public static IRenderer CreateRenderer(Pie pie, Color color)
        {
            var renderer = new PieRenderer(pie, color);
            return renderer;
        }

        // 创建线段渲染器
        public static IRenderer CreateRenderer(Segment segment)
        {
            var renderer = new SegmentRenderer(segment);
            return renderer;
        }

        // 创建线段渲染器
        public static IRenderer CreateRenderer(Segment segment, Color color)
        {
            var renderer = new SegmentRenderer(segment, color);
            return renderer;
        }
    }
}
