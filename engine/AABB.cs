using System;

namespace SimpleX.Collision2D
{
    public struct AABB
    {
        public float minx;
        public float miny;
        public float maxx;
        public float maxy;

        public float width
        {
            get { return maxx - minx; }
        }
        
        public float height
        {
            get { return maxy - miny; }
        }
    }
}
