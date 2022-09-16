using System;

namespace SimpleX
{
    abstract class BaseSystem
    {
        protected World world { get; private set; } = null;

        protected BaseSystem(World world)
        {
            this.world = world;
        }

        public abstract void Tick(float dt);
    }
}
