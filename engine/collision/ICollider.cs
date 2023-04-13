namespace SimpleX.Collision2D
{
    public interface ICollider
    {
        // 几何体类型
        GeometryType geometryType { get; }
        // 包围盒
        AABB boundingBox { get; }

        // 刷新几何信息
        void RefreshGeometry(float rotation);

        // 获取在指定方向（dir）上投影最远的点
        Vector2 GetFarthestProjectionPoint(float rotation, Vector2 dir);

        // 获取在指定方向（dir）上投影最远的点
        Vector2 GetFarthestProjectionPoint(Transform transform, Vector2 dir);
    }
}
