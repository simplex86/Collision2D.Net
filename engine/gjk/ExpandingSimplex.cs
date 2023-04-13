using System.Collections.Generic;

namespace SimpleX.Collision2D
{
    internal enum WindingType
    {
        Unknown          = 0,
        Clockwise        = -1,
        CounterClockwise = 1,
    }

    internal class ExpandingSimplex
    {
        internal struct Edge
        {
            public Vector2 p1;
            public Vector2 p2;
            public Vector2 normal;
            public WindingType winding;
            public float distance;

            public Edge(Vector2 p1,  Vector2 p2, WindingType winding)
            {
                this.p1 = p1;
                this.p2 = p2;
                this.winding = winding;

                normal = new Vector2(p2.x - p1.x, p2.y - p1.y);
                if (winding == WindingType.Clockwise)
                {
                    normal.CW90();
                }
                else 
                { 
                    normal.CCW90();
                }
                normal.Normalized();

                distance = MathX.Abs(p1.x * normal.x + p1.y * normal.y);
            }
        }

        public WindingType winding { get; private set; } = WindingType.Unknown;

        private List<Edge> edges = new List<Edge>();

        public ExpandingSimplex(Simplex simplex)
        {
            Winding(simplex);
            Edges(simplex);
        }

        public Edge GetClosestEdge()
        {
            return edges[0];
        }

        public void Expand(Vector2 p)
        {
            var edge = edges[0];
            edges.RemoveAt(0);

            var edge1 = new Edge(edge.p1, p, winding);
            var edge2 = new Edge(p, edge.p2, winding);
            edges.Add(edge1);
            edges.Add(edge2);

            edges.Sort((a, b) =>
            {
                if (a.distance < b.distance) return -1;
                if (a.distance > b.distance) return 1;
                return 0;
            });
        }

        private void Winding(Simplex simplex)
        {
            var n = simplex.count;
            for (int i = 0; i < n; i++)
            {
                var p1 = simplex[i];
                var p2 = simplex[(i + 1) % n];

                var c = Vector2.Cross(p1, p2);
                if (c > 0)
                {
                    winding = WindingType.Clockwise;
                }
                else
                {
                    winding = WindingType.CounterClockwise;
                }
            }
        }

        private void Edges(Simplex simplex)
        {
            var n = simplex.count;
            for (int i = 0; i < n; i++)
            {
                var p1 = simplex[i];
                var p2 = simplex[(i + 1) % n];

                var edge = new Edge(p1, p2, winding);
                edges.Add(edge);
            }

            edges.Sort((a, b) =>
            {
                if (a.distance < b.distance) return -1;
                if (a.distance > b.distance) return 1;
                return 0;
            });
        }
    }
}
