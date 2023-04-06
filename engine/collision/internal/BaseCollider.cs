namespace SimpleX.Collision2D
{
    // 
    public abstract class BaseCollider<T> : ICollider where T : IGeometry
    {
        //
        protected BaseCollider(T geometry)
        {
            this.geometry = geometry;
        }
    }
}
