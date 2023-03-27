using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D
{
    struct Simplex
    {
        public List<Vector2> vertics;

        public int count
        {
            get => vertics.Count;
        }

        public Vector2 a { get => vertics[count - 1]; }
        public Vector2 b { get => vertics[count - 2]; }
        public Vector2 c { get => vertics[count - 3]; }

        public void Add(ref Vector2 point)
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
        public bool IsContainsOrigin(ref Vector2 dir)
        {
            var ao = -a;
            var ab = b - a;

            if (count == 3)
            {
                var ac = c - a;

                var u = Vector2.Mul3(ref ac, ref ab, ref ab);
                var v = Vector2.Mul3(ref ab, ref ac, ref ac);

                if (Vector2.Dot(ref u, ref ao) > 0.0f)
                {
                    Remove('c');
                    dir = u;
                }
                else
                {
                    if (Vector2.Dot(ref v, ref ao) <= 0.0f)
                    {
                        return true;
                    }

                    Remove('b');
                    dir = v;
                }
            }
            else
            {
                dir = Vector2.Mul3(ref ab, ref ao, ref ab);
            }

            return false;
        }
    }
}
