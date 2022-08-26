﻿using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    abstract class BaseRenderer
    {
        protected Pen pen = new Pen(Color.Red);
        protected SolidBrush brush = new SolidBrush(Color.Red);

        protected BaseRenderer()
        {
            pen.DashStyle = DashStyle.Dash;
        }

        public void Render(Graphics grap, BaseCollision collision, ref Color color)
        {
            DrawCollision(grap, collision, ref color);
            //DrawBoundingBox(grap, ref collision.boundingBox, ref color);
        }

        public abstract void DrawCollision(Graphics grap, BaseCollision collision, ref Color color);

        // 画包围盒
        protected void DrawBoundingBox(Graphics grap, ref AABB box, ref Color color)
        {
            pen.Color = color;

            var x = box.minx;
            var y = box.miny;
            var w = box.maxx - box.minx;
            var h = box.maxy - box.miny;

            grap.DrawRectangle(pen, x, y, w, h);
        }

        // 画矩形
        protected void DrawRectangle(Graphics grap, float x, float y, float width, float height, float angle, Brush brush)
        {
            var verts = GeometryHelper.GetRectanglePoints(x, y, width, height, angle);

            var points = new PointF[]
            {
                new PointF(verts[0].x, verts[0].y),
                new PointF(verts[1].x, verts[1].y),
                new PointF(verts[2].x, verts[2].y),
                new PointF(verts[3].x, verts[3].y),
                new PointF(verts[0].x, verts[0].y),
            };

            grap.FillPolygon(brush, points);
        }
    }
}
