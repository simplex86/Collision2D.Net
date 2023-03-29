namespace SimpleX.Collision2D
{
    public class Pie : IGeometry
    {
        public float radius;
        public float sweep;

        public bool Contains(ref Vector2 pt)
        {
            var o = Vector2.zero;
            var r = Vector2.right;

            var d = GeometryHelper.GetDistance2(ref pt, ref o);
            if (d > radius * radius) return false;

            var p = new Vector2(pt.x, MathX.Abs(pt.y));
            var c = Vector2.Dot(ref p, ref r);
            var t = MathX.ACos(c);

            return t <= sweep * 0.5f;
        }

        public Vector2 GetFarthestProjectionPoint(ref Vector2 dir)
        {
            return Vector2.zero;
        }
    }
}
