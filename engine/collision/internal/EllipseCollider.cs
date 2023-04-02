namespace SimpleX.Collision2D
{
    internal class EllipseCollider : BaseCollider<Ellipse>
    {
        public EllipseCollider(Ellipse ellipse, Vector2 position, float rotation)
            : base(position, rotation)
        {
            geometry = ellipse;
        }
    }
}
