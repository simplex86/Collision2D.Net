namespace SimpleX.Collision2D
{
    internal class CircleCollider : BaseCollider<Circle>
    {
        public CircleCollider(Circle circle)
            : base(circle)
        {

        }

        protected override void OnRefreshGeometry()
        {
            var circle = (Circle)geometry;

            SetBoundingBox(transform.position.x - circle.radius,
                           transform.position.y - circle.radius,
                           transform.position.x + circle.radius,
                           transform.position.y + circle.radius);
        }
    }
}
