using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D.App
{
    class World
    {
        private PositionSystem positionSystem = null;
        private RotationSystem rotationSystem = null;
        private object mutex = new object();

        public List<Entity> entities { get; } = new List<Entity>(20);

        public World()
        {
            positionSystem = new PositionSystem(this);
            rotationSystem = new RotationSystem(this);
        }

        public void AddEntity(Entity entity)
        {
            lock (mutex)
            {
                entities.Add(entity);
            }
        }

        public void Tick(float dt)
        {
            lock (mutex)
            {
                positionSystem.Tick(dt);
                rotationSystem.Tick(dt);
            }
        }

        public void Each(Action<Entity> callback)
        {
            if (callback == null) return;

            foreach (var entity in entities)
            {
                callback(entity);
            }
        }
    }
}
