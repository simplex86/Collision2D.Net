using System;
using System.Collections.Generic;

namespace SimpleX.Collision2D.Engine
{
    public class Quadtree<T> where T : IQuadObject
    {
        public float x { get; private set; }
        public float y { get; private set; }
        public float width { get; private set; }
        public float height { get; private set; }
        public int capacity { get; private set; }

        public Quadtree<T>[] nodes { get; private set; } = null;
        private List<T> objects = null;

        private object mutex = new object();

        public Quadtree(float x, float y, float width, float height)
            : this(x, y, width, height, 10)
        {

        }

        public Quadtree(float x, float y, float width, float height, int capacity)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.capacity = capacity;

            objects = new List<T>(capacity);
        }

        private List<int> GetIndex(T o)
        {
            var list = new List<int>(4);

            var N = (o.y - o.h * 0.5f) < y;
            var W = (o.x - o.w * 0.5f) < x;
            var E = (o.x + o.w * 0.5f) > x;
            var S = (o.y + o.h * 0.5f) > y;

            if (E && S) list.Add(0);
            if (W && S) list.Add(1);
            if (W && N) list.Add(2);
            if (E && N) list.Add(3);

            return list;
        }

        public void Clear()
        {
            lock (mutex)
            {
                if (objects != null)
                {
                    objects.Clear();
                }

                if (nodes != null)
                {
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        var node = nodes[i];
                        if (node.nodes != null)
                        {
                            node.Clear();
                        }
                    }
                    nodes = null;
                }
            }
        }

        public void Insert(T o)
        {
            lock (mutex)
            {
                if (nodes != null)
                {
                    var index = GetIndex(o);
                    Insert(index, o);
                }
                else
                {
                    objects.Add(o);
                    if (objects.Count > capacity)
                    {
                        Split();
                        foreach (var v in objects)
                        {
                            var index = GetIndex(v);
                            Insert(index, v);
                        }
                        objects.Clear();
                    }
                }
            }
        }

        private void Insert(List<int> index, T o)
        {
            foreach (var i in index)
            {
                nodes[i].Insert(o);
            }
        }

        private void Split()
        {
            var w = width * 0.5f;
            var h = height * 0.5f;

            nodes = new Quadtree<T>[4]
            {
                new Quadtree<T>(x + w * 0.5f, y + h * 0.5f, w, h, capacity),
                new Quadtree<T>(x - w * 0.5f, y + h * 0.5f, w, h, capacity),
                new Quadtree<T>(x - w * 0.5f, y - h * 0.5f, w, h, capacity),
                new Quadtree<T>(x + w * 0.5f, y - h * 0.5f, w, h, capacity),
            };
        }

        public List<T> Retrieve(T o)
        {
            lock (mutex)
            {
                if (nodes == null)
                {
                    return objects;
                }

                var list = new List<T>(capacity);
                var index = GetIndex(o);

                foreach (var i in index)
                {
                    var node = nodes[i].Retrieve(o);
                    list.AddRange(node);
                }

                var filter = list.FindAll((v) =>
                {
                     return !v.Equals(o);
                });

                return filter;
            }
        }
    }
}
