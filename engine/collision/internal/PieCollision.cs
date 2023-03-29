namespace SimpleX.Collision2D
{
    internal class PieCollision : BaseCollision
    {
        public PieCollision(Pie pie, Vector2 position, float rotation)
            : base(CollisionType.Pie, position, rotation)
        {
            geometry = pie;
        }
    }
}
