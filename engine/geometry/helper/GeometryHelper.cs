namespace SimpleX.Collision2D
{ 
    internal static class GeometryHelper
    {
        // 获取两点间距离的平方
        public static float GetDistance2(ref Vector2 a, ref Vector2 b)
        {
            var dx = a.x - b.x;
            var dy = a.y - b.y;

            return dx * dx + dy * dy;
        }

        // 获取点（pt）到线段（pa，pb）的最小距离的平方
        public static float GetDistance2(ref Vector2 pa, ref Vector2 pb, ref Vector2 pt)
        {
            float px = pt.x - pa.x, py = pt.y - pa.y;
            float xx = pb.x - pa.x, yy = pb.y - pa.y;
            float h = MathX.Clamp((px * xx + py * yy) / (xx * xx + yy * yy), 0.0f, 1.0f);
            float dx = px - xx * h, dy = py - yy * h;

            return dx * dx + dy * dy;
        }

        // 获取矩形（x, y, width, height, rotation）的顶点
        public static Vector2[] GetRectanglePoints(float width, float height, float rotation)
        {
            var w = width * 0.5f;
            var h = height * 0.5f;

            var p1 = new Vector2(-w, -h);
            var p2 = new Vector2( w, -h);
            var p3 = new Vector2( w,  h);
            var p4 = new Vector2(-w,  h);

            var mt = Matrix.CreateRotationMatrix(rotation * MathX.DEG2RAD);

            var points = new Vector2[] {
                Matrix.Transform(ref p1, ref mt),
                Matrix.Transform(ref p2, ref mt),
                Matrix.Transform(ref p3, ref mt),
                Matrix.Transform(ref p4, ref mt),
            };

            return points;
        }

        // 获取胶囊（length, rotation）的端点
        public static Vector2[] GetCapsulePoints(float length, float rotation)
        {
            var mt = Matrix.CreateRotationMatrix(rotation * MathX.DEG2RAD);

            var p1 = new Vector2(length * 0.5f, 0);
            p1 = Matrix.Transform(ref p1, ref mt);

            var p2 = new Vector2(length * -0.5f, 0);
            p2 = Matrix.Transform(ref p2, ref mt);

            return new Vector2[] { p1, p2, };
        }

        // 图形（geometry)是否包含点（pt)
        public static bool IsGeometryContains(IGeometry geometry, ref Transform transform, ref Vector2 pt)
        {
            var m1 = Matrix.CreateTranslationMatrix(-transform.position.x, -transform.position.y);
            var m2 = Matrix.CreateRotationMatrix(-transform.rotation * MathX.DEG2RAD);
            var mt = m1 * m2;
            var p = Matrix.Transform(ref pt, ref mt);

            return geometry.Contains(ref p);
        }

        // 圆（pa, ra)是否和圆（pb, rb）重叠
        public static bool IsCircleOverlapsWithCircle(ref Circle circle1, ref Transform transform1, 
                                                      ref Circle circle2, ref Transform transform2)
        {
            var dist2 = GetDistance2(ref transform1.position, ref transform2.position);
            var radius2 = (circle1.radius + circle2.radius) * (circle1.radius + circle2.radius);

            return dist2 <= radius2;
        }

        // 圆（circle）是否和矩形（rectangle)重叠
        public static bool IsCircleOverlapsWithRectangle(ref Circle circle,       ref Transform circleTransform, 
                                                         ref Rectangle rectangle, ref Transform rectangleTransform)
        {
            var m = Matrix.CreateRotationMatrix(rectangleTransform.rotation * MathX.DEG2RAD * -1f);
            var v = circleTransform.position - rectangleTransform.position;

            v = Matrix.Transform(ref v, ref m);
            v.x = MathX.Abs(v.x);
            v.y = MathX.Abs(v.y);

            var h = new Vector2(rectangle.width * 0.5f, rectangle.height * 0.5f);
            var u = v - h;

            u.x = MathX.Max(0, u.x);
            u.y = MathX.Max(0, u.y);

            return u.magnitude2 <= circle.radius * circle.radius;
        }

        // 两个矩形是否重叠
        public static bool IsRectangleOverlapsWithRectangle(ref Rectangle rectangle1, ref Transform transform1,
                                                            ref Rectangle rectangle2, ref Transform transform2)
        {
            if (!IsRectangleProjectionOverlaps(ref rectangle1, ref transform1, ref rectangle2, ref transform2)) return false;
            if (!IsRectangleProjectionOverlaps(ref rectangle2, ref transform2, ref rectangle1, ref transform1)) return false;

            return true;
        }

        // 矩形投影是否重叠
        private static bool IsRectangleProjectionOverlaps(ref Rectangle rectangle1, ref Transform transform1,
                                                          ref Rectangle rectangle2, ref Transform transform2)
        {
            var w = rectangle1.width * 0.5f;
            var h = rectangle1.height * 0.5f;
            var d = transform1.position * -1f;

            var m1 = Matrix.CreateTranslationMatrix(d.x, d.y);
            var m2 = Matrix.CreateRotationMatrix(transform1.rotation * MathX.DEG2RAD * -1f);
            var m3 = m1 * m2;

            var ps = GetRectanglePoints(rectangle2.width, rectangle2.height, transform2.rotation);
            var p5 = transform2.position + Matrix.Transform(ref ps[0], ref m3);
            var p6 = transform2.position + Matrix.Transform(ref ps[1], ref m3);
            var p7 = transform2.position + Matrix.Transform(ref ps[2], ref m3);
            var p8 = transform2.position + Matrix.Transform(ref ps[3], ref m3);

            var x1 = MathX.Min(p5.x, p6.x, p7.x, p8.x);
            var x2 = MathX.Max(p5.x, p6.x, p7.x, p8.x);
            var y1 = MathX.Min(p5.y, p6.y, p7.y, p8.y);
            var y2 = MathX.Max(p5.y, p6.y, p7.y, p8.y);

            if (x2 < -w || x1 > w) return false;
            if (y2 < -h || y1 > h) return false;

            return true;
        }

        // 多边形（polygon）和圆形（circle）是否重叠
        public static bool IsPolygonOverlapsWithCircle(ref Polygon polygon, ref Transform polygonTransform, ref Circle circle, ref Transform circleTransform)
        {
            var cp = circleTransform.position;
            var r2 = circle.radius * circle.radius;

            int n = polygon.vertics.Length;
            for (int i = 0; i < n; i++)
            {
                var p1 = polygon.vertics[i] - polygonTransform.position;
                var p2 = polygon.vertics[(i + 1) % n] - polygonTransform.position;

                var d2 = GetDistance2(ref p1, ref p2, ref cp);
                if (d2 <= r2) return true;
            }

            return IsGeometryContains(polygon, ref polygonTransform, ref cp);
        }

        // 线段（p1, p2）是否和线段（q1, q2）相交
        public static bool IsSegmentIntersected(ref Vector2 p1, ref Vector2 p2, ref Vector2 q1, ref Vector2 q2)
        {
            Vector2 a1 = p1 - q2, b1 = p2 - p2, c1 = q1 - q2;
            if (Vector2.Cross(ref a1, ref c1) * Vector2.Cross(ref b1, ref c1) > 0) return false;

            Vector2 a2 = q2 - p2, b2 = q1 - p2, c2 = p1 - p2;
            if (Vector2.Cross(ref a2, ref c2) * Vector2.Cross(ref b2, ref c2) > 0) return false;

            return true;
        }

        // 获取图形（geomotry， transform）在方向（dir）上最大投影的点
        public static Vector2 GetFarthestProjectionPoint(IGeometry geometry, ref Transform transform, ref Vector2 dir)
        {
            var mt = Matrix.CreateRotationMatrix(-transform.rotation * MathX.DEG2RAD);
            var d = Matrix.Transform(ref dir, ref mt);

            var pt = geometry.GetFarthestProjectionPoint(ref d);

            var m1 = Matrix.CreateRotationMatrix(transform.rotation * MathX.DEG2RAD);
            var m2 = Matrix.CreateTranslationMatrix(ref transform.position);
            mt = m1 * m2;

            return Matrix.Transform(ref pt, ref mt);
        }
    }
}
