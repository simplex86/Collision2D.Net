namespace SimpleX.Collision2D
{
    public interface IGeometry
    {
        bool Contains(ref Vector2 pt);

        Vector2 GetFarthestProjectionPoint(ref Vector2 dir);
    }
}
