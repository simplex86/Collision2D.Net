﻿namespace SimpleX.Collision2D
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

        // 获取矩形（x, y, width, height, angle）的顶点
        private static Vector2[] GetRectanglePoints(float x, float y, float width, float height, float angle)
        {
            var w = width * 0.5f;
            var h = height * 0.5f;

            var p1 = new Vector2(-w, -h);
            var p2 = new Vector2(w, -h);
            var p3 = new Vector2(w, h);
            var p4 = new Vector2(-w, h);

            var m1 = Matrix.CreateRotationMatrix(angle * MathX.DEG2RAD);
            var m2 = Matrix.CreateTranslationMatrix(x, y);
            var mt = m1 * m2;

            var points = new Vector2[] {
                Matrix.Transform(ref p1, ref mt),
                Matrix.Transform(ref p2, ref mt),
                Matrix.Transform(ref p3, ref mt),
                Matrix.Transform(ref p4, ref mt),
            };

            return points;
        }

        // 获取矩形（pos, width, height, angle）的顶点
        public static Vector2[] GetRectanglePoints(ref Vector2 pos, float width, float height, float angle)
        {
            return GetRectanglePoints(pos.x, pos.y, width, height, angle);
        }

        // 获取胶囊（x, y, length, angle）的端点
        private static Vector2[] GetCapsulePoints(float x, float y, float length, float angle)
        {
            var m1 = Matrix.CreateRotationMatrix(angle * MathX.DEG2RAD);
            var m2 = Matrix.CreateTranslationMatrix(x, y);
            var mt = m1 * m2;

            var p1 = new Vector2(length * 0.5f, 0);
            p1 = Matrix.Transform(ref p1, ref mt);

            var p2 = new Vector2(length * -0.5f, 0);
            p2 = Matrix.Transform(ref p2, ref mt);

            return new Vector2[] { p1, p2, };
        }

        // 获取胶囊（pos, length, angle）的端点
        public static Vector2[] GetCapsulePoints(ref Vector2 pos, float length, float angle)
        {
            return GetCapsulePoints(pos.x, pos.y, length, angle);
        }

        // 圆（circle）是否包含点（pt）
        public static bool IsCircleContains(ref Circle circle, ref Vector2 pt)
        {
            var d = circle.center - pt;
            var r = circle.radius;

            return d.magnitude2 <= r * r; 
        }

        // 矩形（rectangle)是否包含点（pt)
        public static bool IsRectangleContains(ref Rectangle rectangle, ref Vector2 pt)
        {
            var p = (rectangle.vertics[0] + rectangle.vertics[2]) * 0.5f;
            var m1 = Matrix.CreateTranslationMatrix(-p.x, -p.y);
            var m2 = Matrix.CreateRotationMatrix(-rectangle.angle * MathX.DEG2RAD);
            var mt = m1 * m2;
            
            var t = Matrix.Transform(ref pt, ref mt);

            var w = rectangle.width * 0.5f;
            var h = rectangle.height * 0.5f;

            if (t.x < -w || t.x > w) return false;
            if (t.y < -h || t.y > h) return false;

            return true;
        }

        // 胶囊（pa，pb，radius）是否包含点（pt）
        public static bool IsCapsuleContains(ref Capsule capsule, ref Vector2 pt)
        {
            var dist2 = GetDistance2(ref capsule.points[0], ref capsule.points[1], ref pt);
            var radius2 = capsule.radius * capsule.radius;

            return dist2 <= radius2;
        }

        // 三角形（polygon）是否包含点（pt）
        public static bool IsPolygonContains(ref Polygon polygon, ref Vector2 pt)
        {
            int n = polygon.vertics.Length;

            var u = polygon.vertics[n-1] - pt;
            var v = polygon.vertics[0] - pt;
            var z = Vector2.Cross(ref u, ref v);

            for (int i=0; i<n-1; i++)
            {
                u = polygon.vertics[i] - pt;
                v = polygon.vertics[i+1] - pt;
                var w = Vector2.Cross(ref u, ref v);

                if (z * w < 0) return false;
            }

            return true;
        }

        // 圆（pa, ra)是否和圆（pb, rb）重叠
        public static bool IsCircleOverlapsWithCircle(ref Circle a, ref Circle b)
        {
            var dist2 = GetDistance2(ref a.center, ref b.center);
            var radius2 = (a.radius + b.radius) * (a.radius + b.radius);

            return dist2 <= radius2;
        }

        // 圆（cp，radius）是否和矩形（rp，width, height, angle)重叠
        public static bool IsCircleOverlapsWithRectangle(ref Circle circle, ref Rectangle rectangle)
        {
            var p = (rectangle.vertics[0] + rectangle.vertics[2]) * 0.5f;
            var m = Matrix.CreateRotationMatrix(-rectangle.angle * MathX.DEG2RAD);
            var v = circle.center - p;

            v = Matrix.Transform(ref v, ref m);
            v.x = MathX.Abs(v.x);
            v.y = MathX.Abs(v.y);

            var h = new Vector2(rectangle.width * 0.5f, rectangle.height * 0.5f);
            var u = v - h;

            u.x = MathX.Max(0, u.x);
            u.y = MathX.Max(0, u.y);

            return u.magnitude2 <= circle.radius * circle.radius;
        }

        // 矩形（p1, w1, h1, a1)和矩形（p2, w2, h2, a2）是否重叠
        public static bool IsRectangleOverlapsWithRectangle(ref Rectangle a, ref Rectangle b)
        {
            if (!IsRectangleProjectionOverlaps(ref a, ref b)) return false;
            if (!IsRectangleProjectionOverlaps(ref b, ref a)) return false;
            
            return true;
        }

        // 矩形投影是否重叠
        private static bool IsRectangleProjectionOverlaps(ref Rectangle a, ref Rectangle b)
        {
            var w = a.width * 0.5f;
            var h = a.height * 0.5f;
            var d = (a.vertics[0] + a.vertics[2]) * -0.5f;

            var m1 = Matrix.CreateTranslationMatrix(d.x, d.y);
            var m2 = Matrix.CreateRotationMatrix(-a.angle * MathX.DEG2RAD);
            var m3 = m1 * m2;

            var p5 = Matrix.Transform(ref b.vertics[0], ref m3);
            var p6 = Matrix.Transform(ref b.vertics[1], ref m3);
            var p7 = Matrix.Transform(ref b.vertics[2], ref m3);
            var p8 = Matrix.Transform(ref b.vertics[3], ref m3);

            var x1 = MathX.Min(p5.x, p6.x, p7.x, p8.x);
            var x2 = MathX.Max(p5.x, p6.x, p7.x, p8.x);
            var y1 = MathX.Min(p5.y, p6.y, p7.y, p8.y);
            var y2 = MathX.Max(p5.y, p6.y, p7.y, p8.y);

            if (x2 < -w || x1 > w) return false;
            if (y2 < -h || y1 > h) return false;

            return true;
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

        // 三角形（polygon）和圆形（circle）是否重叠
        public static bool IsPolygonOverlapsWithCircle(ref Polygon polygon, ref Circle circle)
        {
            var v = polygon.vertics;
            var p = circle.center;
            var r = circle.radius * circle.radius;

            int i = 0;
            int n = v.Length;
            for (; i < n - 1; i++)
            {
                if (GeometryHelper.GetDistance2(ref v[i], ref v[i + 1], ref p) <= r) return true;
            }
            if (GeometryHelper.GetDistance2(ref v[n - 1], ref v[0], ref p) <= r) return true;

            return GeometryHelper.IsPolygonContains(ref polygon, ref circle.center);
        }
    }
}
