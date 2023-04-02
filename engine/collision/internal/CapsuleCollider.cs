namespace SimpleX.Collision2D
{
    internal class CapsuleCollider : BaseCollider<Capsule>
    {
        public CapsuleCollider(Capsule capsule, Vector2 position, float rotation)
            : base(position, rotation)
        {
            geometry = capsule;
        }
    }
}
