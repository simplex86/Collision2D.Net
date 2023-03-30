using System.Text.RegularExpressions;

namespace SimpleX.Collision2D
{
    public struct Capsule : IGeometry
    {
        public float length;
        public float radius;

        public bool Contains(Vector2 pt)
        {
            var p1 = new Vector2(length *  0.5f, 0);
            var p2 = new Vector2(length * -0.5f, 0);

            var dist2 = GeometryHelper.GetDistance2(p1, p2, pt);
            var radius2 = radius * radius;

            return dist2 <= radius2;
        }

        public Vector2 GetFarthestProjectionPoint(Vector2 dir)
        {
            var p = radius * dir.normalized;
            var x = dir.x >= 0 ? length * 0.5f : -length * 0.5f;
            p.x += x;

            return p;
        }
    }
}
