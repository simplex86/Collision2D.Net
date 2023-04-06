namespace SimpleX.Collision2D
{
    public struct AABB
    {
        public float minx;
        public float miny;
        public float maxx;
        public float maxy;

        public float width  => maxx - minx;   
        public float height => maxy - miny;

        public void Set(float minx, float miny, float maxx, float maxy)
        {
            this.minx = minx;
            this.miny = miny;
            this.maxx = maxx;
            this.maxy = maxy;
        }

        public bool Contains(Vector2 pt)
        {
            return minx <= pt.x && maxx >= pt.x &&
                   miny <= pt.y && maxy >= pt.y;
        }

        public bool Overlaps(AABB other)
        {
            if (minx > other.maxx || maxx < other.minx) return false;
            if (miny > other.maxy || maxy < other.miny) return false;

            return true;
        }
    }
}
