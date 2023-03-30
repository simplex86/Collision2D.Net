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
    }
}
