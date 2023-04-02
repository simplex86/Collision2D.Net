namespace SimpleX.Collision2D
{
    class PolygonCollider : BaseCollider<Polygon>
    {
        public PolygonCollider(Polygon polygon, Vector2 position, float rotation)
            : base(position, rotation)
        {
            geometry = polygon;
        }
    }
}
