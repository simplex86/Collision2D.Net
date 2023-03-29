using System.Drawing;

namespace SimpleX.Collision2D
{
    public struct Rectangle : IGeometry
    {
        public float width;
        public float height;

        public bool Contains(ref Vector2 pt)
        {
            var w = width * 0.5f;
            var h = height * 0.5f;

            if (pt.x < -w || pt.x > w) return false;
            if (pt.y < -h || pt.y > h) return false;

            return true;
        }

        public Vector2 GetFarthestProjectionPoint(ref Vector2 dir)
        {
            var w = width * 0.5f;
            var h = height * 0.5f;

            var vertics = new Vector2[]
            {
                new Vector2(-w, -h),
                new Vector2( w, -h),
                new Vector2( w,  h),
                new Vector2(-w,  h),
            };

            var point = vertics[0];
            var max = Vector2.Dot(ref point, ref dir);

            for (int i = 1; i < vertics.Length; i++)
            {
                var dot = Vector2.Dot(ref vertics[i], ref dir);
                if (dot > max)
                {
                    max = dot;
                    point = vertics[i];
                }
            }

            return point;
        }
    }
}
