namespace SimpleX.Collision2D
{
    internal class PieCollision : BaseCollision<Pie>
    {
        public PieCollision(Pie pie, Vector2 position, float rotation)
            : base(position, rotation)
        {
            geometry = pie;
        }
    }
}
