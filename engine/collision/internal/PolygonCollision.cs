namespace SimpleX.Collision2D
{
    class PolygonCollision : BaseCollision
    {
        public PolygonCollision(Polygon polygon, Vector2 position, float rotation)
            : base(CollisionType.Polygon, position, rotation)
        {
            geometry = polygon;
        }
    }
}
