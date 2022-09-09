using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D.Engine
{
    struct Simplex
    {
        public List<Vector> vertics;

        public int count
        {
            get => vertics.Count;
        }

        public Vector a { get => vertics[count - 1]; }
        public Vector b { get => vertics[count - 2]; }
        public Vector c { get => vertics[count - 3]; }

        public void Add(ref Vector point)
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
