纯C#手撸的2D凸多边形碰撞检测算法，目前支持**圆盘、矩形、胶囊和凸多边形**等多种图形。常见形状的碰撞检测用的常规的检测算法（例如圆和圆、矩形和矩形），不规则图形则使用GJK算法（都用GJK也不是不行）。同时，基于.Net Framework写了个简单的演示程序（碰撞检测和图形渲染放在两个独立的线程中）来方便测试。

>因为渲染并不是我关注的问题，直接用GDI+来完成图形的可视化。</br>
>演示程序中使用了**并不完善的**ECS系统来实现图形的数据更新。


## Feature
- [X] 圆盘
- [X] 矩形
- [X] 胶囊形
- [X] 多边形（含三角形）
- [ ] 射线
- [ ] 扇形
- [ ] ~~空间分割（BSP或者四叉树）~~

## 示例
在我破旧的电脑上（双核CPU，4G内存）检测100个图形大概耗时**1ms**。如果去掉渲染的话，这个数值可能更低一些。运行结果如下图

![collision](https://github.com/simplex86/Collision2D.Net/blob/main/doc/collision.gif)

## 说明
1. 这个项目仅仅是因为突然对凸多边形的碰撞检测算法感兴趣而写的，所以目前只打算实现离散碰撞。有想过写连续碰撞，但暂时没有知识储备，所以短时间内这部分内容应该不会有推进。
2. 如上所说，这个项目仅实现碰撞检测算法，并不是完备的碰撞系统，**更不是物理系统**（不是物理系统！不是物理系统！不是物理系统！）。所以演示程序中，碰撞后只是用相撞的图形的重心坐标简单算出新的方向而已，不符合任何物理定律（没有质量、法线、角速度等）。只是在边缘的碰撞实现了反射。
3. 胶囊的AABB计算并不是精确的，刚开始是想错了，后来发现这样算简单（可能也更快），就没有进一步修改了。当然这样也导致了胶囊在边缘碰撞时有缺陷，但对图形之间的碰撞检测是没有影响。
4. 为了能方便进行测试，演示程序中实现了一个生成随机凸多边形的算法（当然是借鉴别人的，详见参考文献4） 
>最初计划实现一套空间分割来加速碰撞检测，后面想了下，这个项目主要是实现凸多边形间的碰撞检测算法而不是完备的碰撞检测系统，所以单独开了个仓库实现了个简易（但能用）的四叉树，点击[这里](https://github.com/simplex86/Quadtree.Net)跳转。**也许**，随着时间推移和知识储备增加，会真的去实现个完备的碰撞系统。也许吧，但至少目前不是！

## 参考文献
1. [实时碰撞检测算法技术](https://book.douban.com/subject/4861957/)
2. [3D数学基础: 图形与游戏开发](https://book.douban.com/subject/1400419/)
3. [碰撞检测算法之GJK算法](https://zhuanlan.zhihu.com/p/511164248)
4. [随机凸多边形生成算法](https://kingins.cn/2022/02/18/%E9%9A%8F%E6%9C%BA%E5%87%B8%E5%A4%9A%E8%BE%B9%E5%BD%A2%E7%94%9F%E6%88%90%E7%AE%97%E6%B3%95/)
