namespace SimpleX
{
    class Entity
    {
        public CollisionComponent collisionComponent { get; } = new CollisionComponent();
        public TransformComponent transformComponent { get; } = new TransformComponent();
        public VelocityComponent velocityComponent { get; } = new VelocityComponent();
        public RotationComponent rotationComponent { get; } = new RotationComponent();

        public ColorComponent colorComponent { get; } = new ColorComponent();

        public GeomotryRendererComponent geometryRendererComponent { get; } = new GeomotryRendererComponent();
        public BoundingRendererComponent boundingRendererComponent { get; } = new BoundingRendererComponent();
        public VelocityRendererComponent velocityRendererComponent { get; } = new VelocityRendererComponent();
    }
}
