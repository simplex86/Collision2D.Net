namespace SimpleX.Collision2D
{
    internal class EllipseCollider : BaseCollider<Ellipse>
    {
        public EllipseCollider(Ellipse ellipse)
            : base()
        {
            geometry = ellipse;
        }
    }
}
