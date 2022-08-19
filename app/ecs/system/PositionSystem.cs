using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D.App
{
    class PositionSystem
    {
        private World world = null;

        public PositionSystem(World world)
        {
            this.world = world;
        }

        public void Tick(float dt)
        {
            world.Each((entity) =>
            {

            });
        }
    }
}
