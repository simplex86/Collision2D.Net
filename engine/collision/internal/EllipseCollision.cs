namespace SimpleX.Collision2D
{
    internal class EllipseCollision : BaseCollision<Ellipse>
    {
        public EllipseCollision(Ellipse ellipse, Vector2 position, float rotation)
            : base(position, rotation)
        {
            geometry = ellipse;
        }
    }
}
