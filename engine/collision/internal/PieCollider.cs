namespace SimpleX.Collision2D
{
    internal class PieCollider : BaseCollider<Pie>
    {
        public PieCollider(Pie pie, Vector2 position, float rotation)
            : base(position, rotation)
        {
            geometry = pie;
        }
    }
}
