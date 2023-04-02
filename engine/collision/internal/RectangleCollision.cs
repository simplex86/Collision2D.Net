namespace SimpleX.Collision2D
{
    internal class RectangleCollision : BaseCollision<Rectangle>
    {
        public RectangleCollision(Rectangle rectangle, Vector2 position, float rotation)
            : base(position, rotation)
        {
            geometry = rectangle;
        }
    }
}
