using System.Collections.Generic;

namespace SimpleX.Collision2D
{
    internal class EPA
    {
        private const int MAX_ITERATIONS = 100;

        public void CheckPenetration(Simplex simplex, Minkowski minkowski, ref Penetration penetration)
        {
            ExpandingSimplex expandingSimplex = new ExpandingSimplex(simplex);
            ExpandingSimplex.Edge edge = new ExpandingSimplex.Edge();
            Vector2 point = Vector2.zero;

            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                edge = expandingSimplex.GetClosestEdge();
                point = minkowski.Support(edge.normal);

                var projection = Vector2.Dot(point, edge.normal);
                if (projection - edge.distance < MathX.EPSILON)
                {
                    penetration.normal.x = edge.normal.x;
                    penetration.normal.y = edge.normal.y;
                    penetration.depth = projection;

                    return;
                }

                expandingSimplex.Expand(point);
            }

            penetration.normal.x = edge.normal.x;
            penetration.normal.y = edge.normal.y;
            penetration.depth = Vector2.Dot(point, edge.normal);
        }
    }
}