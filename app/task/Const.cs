namespace SimpleX.Collision2D
{
    // 碰撞体类型
    public enum CollisionType
    {
        BOT,

        // 圆盘
        Circle = BOT,
        // 矩形
        Rectangle,
        // 凸多边形
        Polygon,
        // 胶囊
        Capsule,
        // 椭圆
        Ellipse,
        // 扇形
        Pie,

        EOT,
    }
}
