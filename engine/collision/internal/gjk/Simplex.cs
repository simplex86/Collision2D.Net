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
    }
}
