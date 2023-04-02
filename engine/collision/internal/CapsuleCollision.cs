namespace SimpleX.Collision2D
{
    internal class CapsuleCollision : BaseCollision<Capsule>
    {
        public CapsuleCollision(Capsule capsule, Vector2 position, float rotation)
            : base(position, rotation)
        {
            geometry = capsule;
        }
    }
}
