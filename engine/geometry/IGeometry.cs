namespace SimpleX.Collision2D
{
    public interface IGeometry
    {
        bool Contains(Vector2 pt);

        Vector2 GetFarthestProjectionPoint(Vector2 dir);
    }
}
