namespace SimpleX.Collision2D
{
    internal class CircleCollider : BaseCollider<Circle>
    {
        public CircleCollider(Circle circle)
            : base(circle)
        {

        }

        public override void RefreshGeometry(float rotation)
        {
            var circle = (Circle)_geometry;

            _boundingBox.Set(-circle.radius,
                             -circle.radius,
                              circle.radius,
                              circle.radius);
        }
    }
}
