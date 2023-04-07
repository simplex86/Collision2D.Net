namespace SimpleX
{
    abstract class LogicSystem
    {
        protected World world { get; private set; } = null;

        protected LogicSystem(World world)
        {
            this.world = world;
        }

        public abstract void Tick(float dt);
    }
}
