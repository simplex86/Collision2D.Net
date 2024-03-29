﻿using System;

namespace SimpleX.Collision2D
{
    public struct Transform
    {
        // 位置
        public Vector2 position;
        // 旋转
        public float rotation;
        // 缩放
        public float scale;

        public void Move(Vector2 delta)
        {
            position += delta;
        }

        public void Rotate(float delta)
        {
            rotation += delta;
        }
    }
}
