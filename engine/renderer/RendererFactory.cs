using System.Drawing;

namespace SimpleX.Collision2D
{
    public static class RendererFactory
    {
        public static IRenderer CreateAABBRenderer()
        {
            return new AABBRenderer();
        }

        public static IRenderer CreateVelocityRenderer()
        {
            return new VelocityRenderer();
        }

        // 创建圆形的渲染器
        public static IRenderer CreateCircleRenderer(Circle circle)
        {
            var renderer = new CircleRenderer(circle);
            return renderer;
        }

        // 创建圆形的渲染器
        public static IRenderer CreateCircleRenderer(Circle circle, Color color)
        {
            var renderer = new CircleRenderer(circle, color);
            return renderer;
        }

        // 创建矩形的渲染器
        public static IRenderer CreateRectangleRenderer(Rectangle rectangle)
        {
            var renderer = new RectangleRenderer(rectangle);
            return renderer;
        }

        // 创建矩形的渲染器
        public static IRenderer CreateRectangleRenderer(Rectangle rectangle, Color color)
        {
            var renderer = new RectangleRenderer(rectangle, color);
            return renderer;
        }

        // 创建胶囊的渲染器
        public static IRenderer CreateCapsuleRenderer(Capsule capsule)
        {
            var renderer = new CapsuleRenderer(capsule);
            return renderer;
        }

        // 创建胶囊的渲染器
        public static IRenderer CreateCapsuleRenderer(Capsule capsule, Color color)
        {
            var renderer = new CapsuleRenderer(capsule, color);
            return renderer;
        }

        // 创建椭圆的渲染器
        public static IRenderer CreatePolygonRenderer(Polygon polygon)
        {
            var renderer = new PolygonRenderer(polygon);
            return renderer;
        }

        // 创建椭圆的渲染器
        public static IRenderer CreatePolygonRenderer(Polygon polygon, Color color)
        {
            var renderer = new PolygonRenderer(polygon, color);
            return renderer;
        }

        // 创建椭圆的渲染器
        public static IRenderer CreateEllipseRenderer(Ellipse ellipse)
        {
            var renderer = new EllipseRenderer(ellipse);
            return renderer;
        }

        // 创建椭圆的渲染器
        public static IRenderer CreateEllipseRenderer(Ellipse ellipse, Color color)
        {
            var renderer = new EllipseRenderer(ellipse, color);
            return renderer;
        }

        // 创建扇形的渲染器
        public static IRenderer CreatePieRenderer(Pie pie)
        {
            var renderer = new PieRenderer(pie);
            return renderer;
        }

        // 创建扇形的渲染器
        public static IRenderer CreatePieRenderer(Pie pie, Color color)
        {
            var renderer = new PieRenderer(pie, color);
            return renderer;
        }
    }
}
