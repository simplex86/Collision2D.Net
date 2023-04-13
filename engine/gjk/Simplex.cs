using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D
{
    struct Simplex
    {
        private List<Vector2> vertics;

        public int count => vertics.Count;

        private Vector2 a
        {
            get { return vertics[count - 1]; }
            set { vertics[count - 1] = value; }
        }
        private Vector2 b
        {
            get { return vertics[count - 2]; }
            set { vertics[count - 2] = value; }
        }
        private Vector2 c
        {
            get { return vertics[count - 3]; }
            set { vertics[count - 3] = value; }
        }

        public Simplex(int count)
        {
            vertics = new List<Vector2>(count);
        }

        public Vector2 this[int index] => vertics[index];


        public void Add(Vector2 point)
        {
            vertics.Add(point);
        }

        //// 检测是否包含原点
        //public bool Check()
        //{
        //    if (count == 3)
        //    {
        //        var ao = -a;
        //        var ab = b - a;
        //        var ac = c - a;

        //        var u = Vector2.Mul3(ab, ac, ac);
        //        if (Vector2.Dot(u, ao) >= 0.0f)
        //        {
        //            return false;
        //        }

        //        var v = Vector2.Mul3(ac, ab, ab);
        //        if (Vector2.Dot(v, ao) < 0.0f)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        //// 调整Simplex（删除无用的点），并返回下一次迭代所需的方向向量
        //public Vector2 Adjust()
        //{
        //    var dir = Vector2.zero;

        //    var ao = -a;
        //    var ab = b - a;

        //    if (count == 2)
        //    {
        //        dir = Vector2.Mul3(ab, ao, ab);
        //        if (dir.magnitude2 < MathX.EPSILON)
        //        {
        //            dir = ab.Perpendicular();
        //        }
        //    }
        //    else if (count == 3)
        //    {
        //        var ac = c - a;

        //        dir = Vector2.Mul3(ab, ac, ac);
        //        if (Vector2.Dot(dir, ao) < 0.0f)
        //        {
        //            dir = Vector2.Mul3(ac, ab, ab);
        //            c = b;
        //        }
        //        b = a;
        //        vertics.RemoveAt(count - 1); // remove 'a'
        //    }

        //    return dir;
        //}

        // 检测是否包含原点
        // 包含则返回true；否则返回false，并返回下一次迭代所需的方向向量
        public bool Check(ref Vector2 dir)
        {
            var ao = -a;
            var ab = b - a;

            if (count == 2)
            {
                dir = Vector2.Mul3(ab, ao, ab);
                if (dir.magnitude2 < MathX.EPSILON)
                {
                    dir = ab.Perpendicular();
                }
            }
            else if (count == 3)
            {
                var ac = c - a;
                var v = Vector2.Mul3(ab, ac, ac);

                if (Vector2.Dot(v, ao) >= 0.0f)
                {
                    dir = v;
                }
                else
                {
                    var u = Vector2.Mul3(ac, ab, ab);
                    if (Vector2.Dot(u, ao) < 0.0f)
                    {
                        return true;
                    }

                    c = b;
                    dir = u;
                }

                b = a;
                vertics.RemoveAt(count - 1); // remove 'a'
            }

            return false;
        }
    }
}
