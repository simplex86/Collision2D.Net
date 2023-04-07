namespace SimpleX.Collision2D
{
    internal class CircleCollider : ICollider
    {
        public CircleCollider(Circle circle)
            : base(circle)
        {

        }

        protected override void OnRefreshGeometry()
        {
            var circle = (Circle)geometry;

            _boundingBox.Set(-circle.radius,
                             -circle.radius,
                              circle.radius,
                              circle.radius);
        }
    }
}
