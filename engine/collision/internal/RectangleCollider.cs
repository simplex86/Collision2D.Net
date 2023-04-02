namespace SimpleX.Collision2D
{
    internal class RectangleCollider : BaseCollider<Rectangle>
    {
        public RectangleCollider(Rectangle rectangle, Vector2 position, float rotation)
            : base(position, rotation)
        {
            geometry = rectangle;
        }
    }
}
