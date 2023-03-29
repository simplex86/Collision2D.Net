namespace SimpleX.Collision2D
{
    internal class RectangleCollision : BaseCollision
    {
        public RectangleCollision(Rectangle rectangle, Vector2 position, float rotation)
            : base(CollisionType.Rectangle, position, rotation)
        {
            geometry = rectangle;
        }
    }
}
