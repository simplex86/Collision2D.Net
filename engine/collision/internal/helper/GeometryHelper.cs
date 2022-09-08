﻿using System;

namespace SimpleX.Collision2D.Engine
{ 
    internal static class GeometryHelper
    {
        // 获取两点间距离的平方
        public static float GetDistance2(ref Vector a, ref Vector b)
        {
            var dx = a.x - b.x;
            var dy = a.y - b.y;

            return dx * dx + dy * dy;
        }

        // 获取点（pt）到线段（pa，pb）的最小距离的平方
        public static float GetDistance2(ref Vector pa, ref Vector pb, ref Vector pt)
        {
            float px = pt.x - pa.x, py = pt.y - pa.y;
            float xx = pb.x - pa.x, yy = pb.y - pa.y;
            float h = MathX.Clamp((px * xx + py * yy) / (xx * xx + yy * yy), 0.0f, 1.0f);
            float dx = px - xx * h, dy = py - yy * h;

            return dx * dx + dy * dy;
        }

        // 获取矩形（pos, width, height, angle）的顶点
        public static Vector[] GetRectanglePoints(float x, float y, float width, float height, float angle)
        {
            var w = width * 0.5f;
            var h = height * 0.5f;

            var p1 = new Vector(-w, -h);
            var p2 = new Vector(w, -h);
            var p3 = new Vector(w, h);
            var p4 = new Vector(-w, h);

            var m1 = Matrix.CreateRotationMatrix(angle * MathX.DEG2RAD);
            var m2 = Matrix.CreateTranslationMatrix(x, y);
            var mt = m1 * m2;

            var points = new Vector[] {
                Matrix.Transform(ref p1, ref mt),
                Matrix.Transform(ref p2, ref mt),
                Matrix.Transform(ref p3, ref mt),
                Matrix.Transform(ref p4, ref mt),
            };

            return points;
        }

        // 获取矩形（pos, width, height, angle）的顶点
        public static Vector[] GetRectanglePoints(ref Vector pos, float width, float height, float angle)
        {
            return GetRectanglePoints(pos.x, pos.y, width, height, angle);
        }

        // 获取胶囊（pos, length, angle）的端点
        public static Vector[] GetCapsulePoints(float x, float y, float length, float angle)
        {
            var m1 = Matrix.CreateRotationMatrix(angle * MathX.DEG2RAD);
            var m2 = Matrix.CreateTranslationMatrix(x, y);
            var mt = m1 * m2;

            var p1 = new Vector(length * 0.5f, 0);
            p1 = Matrix.Transform(ref p1, ref mt);

            var p2 = new Vector(length * -0.5f, 0);
            p2 = Matrix.Transform(ref p2, ref mt);

            return new Vector[] { p1, p2, };
        }

        // 获取胶囊（pos, length, angle）的端点
        public static Vector[] GetCapsulePoints(ref Vector pos, float length, float angle)
        {
            return GetCapsulePoints(pos.x, pos.y, length, angle);
        }

        // 圆（pos，radius）是否包含点（pt）
        public static bool IsCircleContains(ref Vector pos, float radius, ref Vector pt)
        {
            var dx = pos.x - pt.x;
            var dy = pos.y - pt.y;

            return dx * dx + dy * dy <= radius * radius; 
        }

        // 矩形（x, y, width, height, angle)是否包含点（px, py)
        public static bool IsRectangleContains(float x, float y, float width, float height, float angle, float px, float py)
        {
            var m1 = Matrix.CreateRotationMatrix(-angle * MathX.DEG2RAD);
            var m2 = Matrix.CreateTranslationMatrix(-x, -y);
            var mt = m2 * m1;

            var pt = new Vector(px, py);
            pt = Matrix.Transform(ref pt, ref mt);

            var w = width * 0.5f;
            var h = height * 0.5f;

            if (pt.x < -w || pt.x > w) return false;
            if (pt.y < -h || pt.y > h) return false;

            return true;
        }

        // 矩形（pos, width, height, angle)是否包含点（pt)
        public static bool IsRectangleContains(ref Vector pos, float width, float height, float angle, ref Vector pt)
        {
            return IsRectangleContains(pos.x, pos.y, width, height, angle, pt.x, pt.y);
        }

        // 胶囊（pa，pb，radius）是否包含点（pt）
        public static bool IsCapsuleContains(ref Vector pa, ref Vector pb, float radius, ref Vector pt)
        {
            var dist2 = GetDistance2(ref pa, ref pb, ref pt);
            return dist2 < radius * radius;
        }

        // 圆（pa, ra)是否和圆（pb, rb）碰撞
        public static bool IsCircleOverlayWidthCircle(ref Vector pa, float ra, ref Vector pb, float rb)
        {
            var dist2 = GetDistance2(ref pa, ref pb);
            var radius2 = (ra + rb) * (ra + rb);

            return dist2 <= radius2;
        }

        // 圆（cp，radius）是否和矩形（rp，width, height, angle)重叠
        public static bool IsCircleOverlayWithRectangle(ref Vector cp, float radius, 
                                                        ref Vector rp, float width, float height, float angle)
        {
            var m = Matrix.CreateRotationMatrix(-angle * MathX.DEG2RAD);
            var p = cp - rp;

            p = Matrix.Transform(ref p, ref m);
            p.x = MathX.Abs(p.x);
            p.y = MathX.Abs(p.y);

            var h = new Vector(width * 0.5f, height * 0.5f);
            var u = p - h;

            u.x = MathX.Max(0, u.x);
            u.y = MathX.Max(0, u.y);

            return u.magnitude2 <= radius * radius;
        }

        // 矩形（p1, w1, h1, a1)和矩形（p2, w2, h2, a2）是否碰撞
        public static bool IsRectangleOverlayWithRectangle(Vector[] ps1, float w1, float h1, float a1,
                                                           Vector[] ps2, float w2, float h2, float a2)
        {
            if (!IsRectangleProjectionOverlays(ps1, w1, h1, a1, ps2, w2, h2, a2)) return false;
            if (!IsRectangleProjectionOverlays(ps2, w2, h2, a2, ps1, w1, h1, a1)) return false;
            
            return true;
        }

        // 矩形投影是否重叠
        private static bool IsRectangleProjectionOverlays(Vector[] p1, float w1, float h1, float a1,
                                                          Vector[] p2, float w2, float h2, float a2)
        {
            var w = w1 * 0.5f;
            var h = h1 * 0.5f;
            var d = (p1[0] + p1[2]) * -0.5f;

            var m1 = Matrix.CreateTranslationMatrix(d.x, d.y);
            var m2 = Matrix.CreateRotationMatrix(-a1 * MathX.DEG2RAD);
            var m3 = m1 * m2;

            var p5 = Matrix.Transform(ref p2[0], ref m3);
            var p6 = Matrix.Transform(ref p2[1], ref m3);
            var p7 = Matrix.Transform(ref p2[2], ref m3);
            var p8 = Matrix.Transform(ref p2[3], ref m3);

            var x1 = MathX.Min(p5.x, p6.x, p7.x, p8.x);
            var x2 = MathX.Max(p5.x, p6.x, p7.x, p8.x);
            var y1 = MathX.Min(p5.y, p6.y, p7.y, p8.y);
            var y2 = MathX.Max(p5.y, p6.y, p7.y, p8.y);

            if (x2 < -w || x1 > w) return false;
            if (y2 < -h || y1 > h) return false;

            return true;
        }

        // 线段（p1, p2）是否和线段（q1, q2）相交
        public static bool IsSegmentIntersected(ref Vector p1, ref Vector p2, ref Vector q1, ref Vector q2)
        {
            Vector a1 = p1 - q2, b1 = p2 - p2, c1 = q1 - q2;
            if (Vector.Cross(ref a1, ref c1).w * Vector.Cross(ref b1, ref c1).w > 0) return false;

            Vector a2 = q2 - p2, b2 = q1 - p2, c2 = p1 - p2;
            if (Vector.Cross(ref a2, ref c2).w * Vector.Cross(ref b2, ref c2).w > 0) return false;

            return true;
        }
    }
}