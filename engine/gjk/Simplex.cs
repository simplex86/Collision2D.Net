using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D
{
    struct Simplex
    {
        public List<Vector2> vertics;

        public int count => vertics.Count;

        public Vector2 a => vertics[count - 1];
        public Vector2 b => vertics[count - 2];
        public Vector2 c => vertics[count - 3];

        public void Add(Vector2 point)
        {
            vertics.Add(point);
        }

        public void Remove(char name)
        {
            switch (name)
            {
                case 'a':
                    vertics.RemoveAt(count - 1);
                    break;
                case 'b':
                    vertics.RemoveAt(count - 2);
                    break;
                case 'c':
                    vertics.RemoveAt(count - 3);
                    break;
                default:
                    break;
            }
        }

        // 是否包含原点
        public bool CheckOrigin(ref Vector2 dir)
        {
            var ao = -a;
            var ab = b - a;

            if (count == 3)
            {
                var ac = c - a;

                var u = Vector2.Mul3(ac, ab, ab);
                var v = Vector2.Mul3(ab, ac, ac);

                if (Vector2.Dot(u, ao) > 0.0f)
                {
                    Remove('c');
                    dir = u;
                }
                else
                {
                    if (Vector2.Dot(v, ao) <= 0.0f)
                    {
                        return true;
                    }

                    Remove('b');
                    dir = v;
                }
            }
            else
            {
                dir = Vector2.Mul3(ab, ao, ab);
            }

            return false;
        }
    }
}
