namespace SimpleX.Collision2D
{
    // AABB 的几何数据，不包含位置信息
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
    }
}
