namespace SimpleX.Collision2D
{
    struct Minkowski
    {
        private ICollider collider1;
        private Transform transform1;
        private ICollider collider2;
        private Transform transform2;

        public Minkowski(ICollider collider1, Transform transform1,
                         ICollider collider2, Transform transform2)
        {
            this.collider1  = collider1;
            this.transform1 = transform1;
            this.collider2  = collider2;
            this.transform2 = transform2;
        }

        public Vector2 Support(Vector2 dir)
        {
            var p1 = collider1.GetFarthestProjectionPoint(transform1, dir);
            dir.Negative();
            var p2 = collider2.GetFarthestProjectionPoint(transform2, dir);

            return p1 - p2;
        }
    }
}
