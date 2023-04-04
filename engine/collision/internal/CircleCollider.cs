namespace SimpleX.Collision2D
{
    internal class CircleCollider : BaseCollider<Circle>
    {
        public CircleCollider(Circle circle)
            : base()
        {
            geometry = circle;
        }

        protected override void OnRefreshGeometry()
        {
            var circle = (Circle)geometry;
            boundingBox.minx = transform.position.x - circle.radius;
            boundingBox.maxx = transform.position.x + circle.radius;
            boundingBox.miny = transform.position.y - circle.radius;
            boundingBox.maxy = transform.position.y + circle.radius;
        }
    }
}
