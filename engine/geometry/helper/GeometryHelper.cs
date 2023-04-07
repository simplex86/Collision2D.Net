namespace SimpleX.Collision2D
{ 
    public static class GeometryHelper
    {
        // 获取两点间距离的平方
        public static float GetDistance2(Vector2 a, Vector2 b)
        {
            var dx = a.x - b.x;
            var dy = a.y - b.y;

            return dx * dx + dy * dy;
        }

        // 获取点（pt）到线段（pa，pb）的最小距离的平方
        public static float GetDistance2(Vector2 pa, Vector2 pb, Vector2 pt)
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
                Matrix.Transform(p1, mt),
                Matrix.Transform(p2, mt),
                Matrix.Transform(p3, mt),
                Matrix.Transform(p4, mt),
            };

            return points;
        }

        // 获取胶囊（length, rotation）的端点
        public static Vector2[] GetCapsulePoints(float length, float rotation)
        {
            var mt = Matrix.CreateRotationMatrix(rotation * MathX.DEG2RAD);

            var p1 = new Vector2(length * 0.5f, 0);
            p1 = Matrix.Transform(p1, mt);

            var p2 = new Vector2(length * -0.5f, 0);
            p2 = Matrix.Transform(p2, mt);

            return new Vector2[] { p1, p2, };
        }

        // 图形（geometry)是否包含点（pt)
        public static bool Contains(IGeometry geometry, Transform transform, Vector2 pt)
        {
            var m1 = Matrix.CreateTranslationMatrix(-transform.position);
            var m2 = Matrix.CreateRotationMatrix(-transform.rotation * MathX.DEG2RAD);
            var mt = m1 * m2;
            pt = Matrix.Transform(pt, mt);

            return geometry.Contains(pt);
        }

        // 检测图形（geometry1, transform1）与图形（geometry2, transform2）是否重叠
        public static bool Overlaps(IGeometry geometry1, Transform transform1,
                                    IGeometry geometry2, Transform transform2)
        {
            return GJK.Overlaps(geometry1, transform1, geometry2, transform2);
        }

        // 圆（pa, ra)是否和圆（pb, rb）重叠
        public static bool IsCircleOverlapsWithCircle(Circle circle1, Transform transform1, 
                                                      Circle circle2, Transform transform2)
        {
            var dist2 = GetDistance2(transform1.position, transform2.position);
            var radius2 = (circle1.radius + circle2.radius) * (circle1.radius + circle2.radius);

            return dist2 <= radius2;
        }

        // 圆（circle）是否和矩形（rectangle)重叠
        public static bool IsCircleOverlapsWithRectangle(Circle    circle,    Transform circleTransform, 
                                                         Rectangle rectangle, Transform rectangleTransform)
        {
            var m = Matrix.CreateRotationMatrix(rectangleTransform.rotation * MathX.DEG2RAD * -1f);
            var v = circleTransform.position - rectangleTransform.position;

            v = Matrix.Transform(v, m);
            v.x = MathX.Abs(v.x);
            v.y = MathX.Abs(v.y);

            var h = new Vector2(rectangle.width * 0.5f, rectangle.height * 0.5f);
            var u = v - h;

            u.x = MathX.Max(0, u.x);
            u.y = MathX.Max(0, u.y);

            return u.magnitude2 <= circle.radius * circle.radius;
        }

        // 两个矩形是否重叠
        public static bool IsRectangleOverlapsWithRectangle(Rectangle rectangle1, Transform transform1,
                                                            Rectangle rectangle2, Transform transform2)
        {
            if (!IsRectangleProjectionOverlaps(rectangle1, transform1, rectangle2, transform2)) return false;
            if (!IsRectangleProjectionOverlaps(rectangle2, transform2, rectangle1, transform1)) return false;

            return true;
        }

        // 圆形（circle）和多边形（polygon）是否重叠
        public static bool IsPolygonOverlapsWithCircle(Circle  circle,  Transform circleTransform, 
                                                       Polygon polygon, Transform polygonTransform)
        {
            var r2 = circle.radius * circle.radius;

            int n = polygon.vertics.Length;
            for (int i = 0; i < n; i++)
            {
                var p1 = polygon.vertics[i] - polygonTransform.position;
                var p2 = polygon.vertics[(i + 1) % n] - polygonTransform.position;

                var d2 = GetDistance2(p1, p2, circleTransform.position);
                if (d2 <= r2) return true;
            }

            return Contains(polygon, polygonTransform, circleTransform.position);
        }

        // 矩形投影是否重叠
        private static bool IsRectangleProjectionOverlaps(Rectangle rectangle1, Transform transform1,
                                                          Rectangle rectangle2, Transform transform2)
        {
            var w = rectangle1.width * 0.5f;
            var h = rectangle1.height * 0.5f;

            var m1 = Matrix.CreateTranslationMatrix(-transform1.position);
            var m2 = Matrix.CreateRotationMatrix(transform1.rotation * MathX.DEG2RAD * -1f);
            var mt = m1 * m2;

            var ps = GetRectanglePoints(rectangle2.width, rectangle2.height, transform2.rotation);
            var p5 = transform2.position + Matrix.Transform(ps[0], mt);
            var p6 = transform2.position + Matrix.Transform(ps[1], mt);
            var p7 = transform2.position + Matrix.Transform(ps[2], mt);
            var p8 = transform2.position + Matrix.Transform(ps[3], mt);

            var x1 = MathX.Min(p5.x, p6.x, p7.x, p8.x);
            var x2 = MathX.Max(p5.x, p6.x, p7.x, p8.x);
            var y1 = MathX.Min(p5.y, p6.y, p7.y, p8.y);
            var y2 = MathX.Max(p5.y, p6.y, p7.y, p8.y);

            if (x2 < -w || x1 > w) return false;
            if (y2 < -h || y1 > h) return false;

            return true;
        }

        // 线段（p1, p2）是否和线段（q1, q2）相交
        public static bool IsSegmentsIntersected(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
        {
            Vector2 a1 = p1 - q2, b1 = p2 - p2, c1 = q1 - q2;
            if (Vector2.Cross(a1, c1) * Vector2.Cross(b1, c1) > 0) return false;

            Vector2 a2 = q2 - p2, b2 = q1 - p2, c2 = p1 - p2;
            if (Vector2.Cross(a2, c2) * Vector2.Cross(b2, c2) > 0) return false;

            return true;
        }

        // 获取图形（geomotry）旋转（rotation）后在方向（dir）上最大投影的点
        public static Vector2 GetFarthestProjectionPoint(IGeometry geometry, float rotation, Vector2 dir)
        {
            var m1 = Matrix.CreateRotationMatrix(-rotation * MathX.DEG2RAD);
            dir = Matrix.Transform(dir, m1);

            var pt = geometry.GetFarthestProjectionPoint(dir);

            var m2 = Matrix.CreateRotationMatrix(rotation * MathX.DEG2RAD);
            return Matrix.Transform(pt, m2);
        }
    }
}
