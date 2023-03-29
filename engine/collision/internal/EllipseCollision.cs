namespace SimpleX.Collision2D
{
    internal class EllipseCollision : BaseCollision
    {
        public EllipseCollision(Ellipse ellipse, Vector2 position, float rotation)
            : base(CollisionType.Ellipse, position, rotation)
        {
            geometry = ellipse;
        }
    }
}
