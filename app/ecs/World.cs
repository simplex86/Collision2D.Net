using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D.App
{
    class World
    {
        private PositionSystem positionSystem = null;
        private RotationSystem rotationSystem = null;
        private CollisionSystem collisionSystem = null;
        
        private List<Entity> entities = new List<Entity>(20);
        private object mutex = new object();

        public World()
        {
            positionSystem = new PositionSystem(this);
            rotationSystem = new RotationSystem(this);
            collisionSystem = new CollisionSystem(this);
        }

        public void AddEntity(Entity entity)
        {
            lock (mutex)
            {
                entities.Add(entity);
            }
        }

        public void Update(float dt)
        {
            positionSystem.Tick(dt);
            rotationSystem.Tick(dt);
        }

        public void LateUpdate(float dt)
        {
            collisionSystem.Tick(dt);
        }

        public void Each(Action<Entity> callback)
        {
            if (callback == null) return;

            lock (mutex)
            {
                foreach (var entity in entities)
                {
                    callback(entity);
                }
            }
        }

        public void Each2(Action<Entity, Entity> callback)
        {
            if (callback == null) return;

            lock (mutex)
            {
                for (int i=0; i<entities.Count; i++)
                {
                    var a = entities[i];
                    for (int j=i+1; j<entities.Count; j++)
                    {
                        var b = entities[j];
                        callback(a, b);
                    }
                }
            }
        }

        public void Destroy()
        {
            lock (mutex)
            {
                entities.Clear();
            }
        }
    }
}
