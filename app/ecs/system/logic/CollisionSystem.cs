namespace SimpleX
{
    using SimpleX.Collision2D;

    class CollisionSystem : LogicSystem
    {
        public GJK detector = new GJK();

        public CollisionSystem(World world)
            : base(world)
        {

        }

        public override void Tick(float dt)
        {
            world.Each2((a, b) =>
            {
                var collider1  = a.collisionComponent.collider;
                var collider2  = b.collisionComponent.collider;
                var transform1 = a.transformComponent.transform;
                var transform2 = b.transformComponent.transform;

                if (Detect(collider1, transform1, collider2, transform2))
                {
                    var direction = Vector2.Normalize(transform1.position - transform2.position);
                    a.velocityComponent.velocity.direction =  direction;
                    b.velocityComponent.velocity.direction = -direction;
                }
            });
        }

        private bool Detect(ICollider collider1, Transform transform1, ICollider collider2, Transform transform2)
        {
            if (BoundingBoxHelper.Overlaps(collider1.boundingBox, transform1, collider2.boundingBox, transform2))
            {
                return detector.Detect(collider1, transform1, collider2, transform2);
            }
            return false;
        }
    }
}
