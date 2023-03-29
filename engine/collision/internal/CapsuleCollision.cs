namespace SimpleX.Collision2D
{
    internal class CapsuleCollision : BaseCollision
    {
        public CapsuleCollision(Capsule capsule, Vector2 position, float rotation)
            : base(CollisionType.Capsule, position, rotation)
        {
            geometry = capsule;
        }
    }
}
