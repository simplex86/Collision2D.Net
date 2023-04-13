namespace SimpleX
{
    class GeometrySystem : LogicSystem
    {
        public GeometrySystem(World world)
            : base(world)
        {

        }

        public override void Tick(float dt)
        {
            world.Each((entity) =>
            {
                var collider = entity.collisionComponent.collider;
                var transfrom = entity.transformComponent.transform;
                collider.RefreshGeometry(transfrom.rotation);
            });
        }
    }
}
