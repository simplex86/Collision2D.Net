using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D.App
{
    class World
    {
        private List<BaseSystem> systems = new List<BaseSystem>();
        private List<BaseSystem> lateSystems = new List<BaseSystem>();

        private List<Entity> entities = new List<Entity>(20);
        private object mutex = new object();

        public Boundary left;
        public Boundary right;
        public Boundary top;
        public Boundary bottom;

        public World()
        {
            systems.Add(new PositionSystem(this));
            systems.Add(new RotationSystem(this));
            systems.Add(new GeometrySystem(this));

            lateSystems.Add(new CollisionSystem(this));
            lateSystems.Add(new BoundarySystem(this));
            // 最后执行的一个LateSystem
            lateSystems.Add(new LatePostSystem(this));
        }

        public void AddEntity(Entity entity)
        {
            lock (mutex)
            {
                entities.Add(entity);
            }
        }

        public int GetEntityCount()
        {
            return entities.Count;
        }

        public void Update(float dt)
        {
            foreach (var system in systems)
            {
                system.Tick(dt);
            }
        }

        public void LateUpdate(float dt)
        {
            foreach (var system in lateSystems)
            {
                system.Tick(dt);
            }
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

        public void Each(Func<Entity, bool> callback)
        {
            if (callback == null) return;

            lock (mutex)
            {
                foreach (var entity in entities)
                {
                    if (!callback(entity)) break;
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
