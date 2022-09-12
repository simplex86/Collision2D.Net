using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D.App
{
    class Stats
    {
        //
        public int collisionCount { get; set; }
        //
        public float collideCost { get => (frames[0] == 0) ? float.NaN : costs[0] / frames[0]; }
        //
        public float renderCost { get => (frames[1] == 0) ? float.NaN : costs[1] / frames[1]; }

        private int[] frames = { 0, 0};
        private float[] costs = { 0.0f, 0.0f };

        public void OnCollideFrame(float dt)
        {
            frames[0] += 1;
            costs[0] += dt;
        }

        public void OnRenderFrame(float dt)
        {
            frames[1] += 1;
            costs[1] += dt;
        }

        public void Reset()
        {
            frames[0] = 0;
            frames[1] = 0;
            costs[0] = 0.0f;
            costs[1] = 0.0f;
        }
    }
}
