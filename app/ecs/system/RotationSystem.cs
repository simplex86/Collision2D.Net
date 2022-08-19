﻿using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D.App
{
    using SimpleX.Collision2D.Engine;

    class RotationSystem
    {
        private World world = null;

        public RotationSystem(World world)
        {
            this.world = world;
        }

        public void Tick(float dt)
        {
            world.Each((entity) =>
            {
                var collision = entity.collisionComponent.collision;
                if (collision == null || collision is CircleCollision) return;

                if (collision is RectangleCollision)
                {
                    var rectangle = collision as RectangleCollision;

                    var rotation = rectangle.angle + 0.1f;
                    rectangle.angle = rotation;
                }
            });
        }
    }
}