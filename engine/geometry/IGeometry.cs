namespace SimpleX.Collision2D
{
    // 几何体
    // 只包含几何体本身的数据，比如圆形的半径、矩形的长宽等
    // 不包含位置、旋转等数据
    public interface IGeometry
    {
        GeometryType type { get; }

        // 是否包含点（pt）
        bool Contains(Vector2 pt);

        // 获取在指定方向（dir）上投影最远的点
        Vector2 GetFarthestProjectionPoint(Vector2 dir);
    }
}
