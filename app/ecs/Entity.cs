using System;

namespace SimpleX.Collision2D.App
{
    class Entity
    {
        public CollisionComponent collisionComponent { get; } = new CollisionComponent();
        public ColorComponent colorComponent { get; } = new ColorComponent();
    }
}
