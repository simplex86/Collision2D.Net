using System;

namespace SimpleX.Collision2D
{
    internal class CircleCollision : BaseCollision
    {
        public CircleCollision(Circle circle, Vector2 position)
            : base(CollisionType.Circle, position, 0)
        {
            geometry = circle;
        }

        public override void RefreshGeometry()
        {
            if (dirty != DirtyFlag.None)
            {
                var circle = (Circle)geometry;
                boundingBox.minx = transform.position.x - circle.radius;
                boundingBox.maxx = transform.position.x + circle.radius;
                boundingBox.miny = transform.position.y - circle.radius;
                boundingBox.maxy = transform.position.y + circle.radius;

                dirty = DirtyFlag.None;
            }
        }
    }
}
