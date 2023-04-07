namespace SimpleX.Collision2D
{
    public static class BoundingBoxHelper
    {
        public static bool Contains(AABB boundingBox, Transform transform, Vector2 pt)
        {
            var minx = boundingBox.minx + transform.position.x;
            var miny = boundingBox.miny + transform.position.y;
            var maxx = boundingBox.maxx + transform.position.x;
            var maxy = boundingBox.maxy + transform.position.y;

            return minx <= pt.x && maxx >= pt.x &&
                   miny <= pt.y && maxy >= pt.y;
        }

        public static bool Overlaps(AABB boundingBox1, Transform transform1,
                                    AABB boundingBox2, Transform transform2)
        {
            var minx1 = boundingBox1.minx + transform1.position.x;
            var miny1 = boundingBox1.miny + transform1.position.y;
            var maxx1 = boundingBox1.maxx + transform1.position.x;
            var maxy1 = boundingBox1.maxy + transform1.position.y;

            var minx2 = boundingBox2.minx + transform2.position.x;
            var miny2 = boundingBox2.miny + transform2.position.y;
            var maxx2 = boundingBox2.maxx + transform2.position.x;
            var maxy2 = boundingBox2.maxy + transform2.position.y;

            if (minx1 > maxx2 || maxx1 < minx2) return false;
            if (miny1 > maxy2 || maxy1 < miny2) return false;

            return true;
        }
    }
}
