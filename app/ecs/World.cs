using System;
using System.Collections.Generic;
using System.Drawing;

namespace SimpleX
{
    class World
    {
        private List<LogicSystem> logicSystems = new List<LogicSystem>();
        private List<LogicSystem> lateLogicSystems = new List<LogicSystem>();
        private List<RenderSystem> renderSystems = new List<RenderSystem>();

        private List<Entity> entities = new List<Entity>(100);
        private object mutex = new object();

        public Boundary left;
        public Boundary right;
        public Boundary top;
        public Boundary bottom;

        public World()
        {
            logicSystems.Add(new PositionSystem(this));
            logicSystems.Add(new RotationSystem(this));
            logicSystems.Add(new GeometrySystem(this));

            lateLogicSystems.Add(new CollisionSystem(this));
            lateLogicSystems.Add(new BoundarySystem(this));
            lateLogicSystems.Add(new LatePostSystem(this));

            renderSystems.Add(new GeometryRenderSystem(this));
            renderSystems.Add(new BoundingRenderSystem(this));
            renderSystems.Add(new VelocityRenderSystem(this));
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
            foreach (var system in logicSystems)
            {
                system.Tick(dt);
            }
        }

        public void LateUpdate(float dt)
        {
            foreach (var system in lateLogicSystems)
            {
                system.Tick(dt);
            }
        }

        public void Render(Graphics grap)
        {
            foreach (var system in renderSystems)
            {
                system.Render(grap);
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
