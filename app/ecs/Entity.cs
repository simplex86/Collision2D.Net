using System;

namespace SimpleX
{
    class Entity
    {
        public CollisionComponent collisionComponent { get; } = new CollisionComponent();
        public ColorComponent colorComponent { get; } = new ColorComponent();
        public MovementComponent movementComponent { get; } = new MovementComponent();
        public RotationComponent rotationComponent { get; } = new RotationComponent();
        public RenderComponent renderComponent { get; } = new RenderComponent();
    }
}
