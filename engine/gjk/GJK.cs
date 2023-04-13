namespace SimpleX.Collision2D
{
    public class GJK
    {
        private EPA _epa = null;
        private EPA epa
        {
            get
            {
                if (_epa == null) _epa = new EPA();
                return _epa;
            }
        }

        private const int MAX_DETECT_ITERATIONS = 100;

        // 检测图形（geometry1, transform1）与图形（geometry2, transform2）是否碰撞
        public bool Detect(ICollider collider1, Transform transform1,
                           ICollider collider2, Transform transform2)
        {
            if (collider1.geometryType == GeometryType.Circle && collider2.geometryType == GeometryType.Circle)
            {
                var c1 = collider1 as BaseCollider<Circle>;
                var c2 = collider2 as BaseCollider<Circle>;
                return GeometryHelper.IsCircleOverlapsWithCircle(c1.geometry, transform1, c2.geometry, transform2);
            }

            var simplex = new Simplex(3);
            var minkowski = new Minkowski(collider1, transform1, collider2, transform2);
            var dir = transform1.position - transform2.position;

            return Detect(ref simplex, ref minkowski, dir);
        }

        // 检测图形（geometry1, transform1）与图形（geometry2, transform2）是否碰撞
        // 如果碰撞，则通过 penetration 返回穿透数据
        public bool Detect(ICollider collider1, Transform transform1,
                           ICollider collider2, Transform transform2,
                           ref Penetration penetration)
        {
            var simplex = new Simplex(3);
            var minkowski = new Minkowski(collider1, transform1, collider2, transform2);
            var dir = transform1.position - transform2.position;

            if (Detect(ref simplex, ref minkowski, dir))
            {
                epa.CheckPenetration(simplex, minkowski, ref penetration);
                return true;
            }

            return false;
        }

        // 检测碰撞
        private bool Detect(ref Simplex simplex, ref Minkowski minkowski, Vector2 dir)
        {
            if (dir.magnitude2 < MathX.EPSILON)
            {
                dir = Vector2.right;
            }

            var pt = minkowski.Support(dir);
            simplex.Add(pt);

            if (pt.Dot(dir) <= 0.0f)
            {
                return false;
            }

            dir.Negative();

            for (int i = 0; i < MAX_DETECT_ITERATIONS; i++)
            {
                pt = minkowski.Support(dir);
                simplex.Add(pt);

                if (pt.Dot(dir) <= 0.0f)
                {
                    return false;
                }

                if (simplex.Check(ref dir))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
