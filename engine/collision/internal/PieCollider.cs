namespace SimpleX.Collision2D
{
    internal class PieCollider : BaseCollider<Pie>
    {
        public PieCollider(Pie pie)
            : base()
        {
            geometry = pie;
        }
    }
}
