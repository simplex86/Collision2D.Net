namespace SimpleX.Collision2D
{
    class PolygonCollision : BaseCollision<Polygon>
    {
        public PolygonCollision(Polygon polygon, Vector2 position, float rotation)
            : base(position, rotation)
        {
            geometry = polygon;
        }
    }
}
