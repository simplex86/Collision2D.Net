纯C#手撸的2D凸多边形碰撞检测算法，目前支持**圆盘、矩形和胶囊**三种图形。为方便测试，基于.Net Framework写了个简单的应用，把碰撞计算和渲染放在两个独立的线程中。应用中使用了**并不完善的**ECS系统来实现图形的数据更新。

PS.因为渲染并不是我关注的问题，直接用GDI+来完成图形的可视化

## 支持
1. 圆盘
2. 矩形
3. 胶囊形

## TODO
1. 三角形
2. 射线
3. BPS

## 示例
100个图形在双核CPU，4G内存的机器上跑出来的结果
![collision](https://github.com/simplex86/Collision2D.Net/blob/main/doc/collision.gif)
