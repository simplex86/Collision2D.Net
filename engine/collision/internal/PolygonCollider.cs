namespace SimpleX.Collision2D
{
    class PolygonCollider : BaseCollider<Polygon>
    {
        public PolygonCollider(Polygon polygon)
            : base()
        {
            geometry = polygon;
        }
    }
}
