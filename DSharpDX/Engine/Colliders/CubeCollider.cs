using SharpDX;
using System.Collections.Generic;
using DSharpDX.Engine.Objects;

namespace DSharpDX.Engine.Colliders
{
    public class CubeCollider : Collider
    {
        public float Width { get; protected set; }

        public CubeCollider(Vector3 pos, float width) : base(pos)
        {
            Width = width;
        }

        public Vector3[] GetVertices()
        {
            Vector3 min = Position - Width;
            Vector3 max = Position + Width;

            float dotsCount = 10f; // dots count on fragment is dotsCount^2
            float step = 2f * Width / dotsCount;

            List<Vector3> vertices = new List<Vector3>();

            // front and back fragment
            for (float x = min.X; x <= max.X; x += step)
            {
                for (float y = min.Y; y <= max.Y; y += step)
                {
                    vertices.Add(new Vector3(x, y, min.Z));
                    vertices.Add(new Vector3(x, y, max.Z));
                }
            }
            // left and right fragment
            for (float z = min.Z; z <= max.Z; z += step)
            {
                for (float y = min.Y; y <= max.Y; y += step)
                {
                    vertices.Add(new Vector3(min.X, y, z));
                    vertices.Add(new Vector3(max.X, y, z));
                }
            }

            return vertices.ToArray();
        }
    }
}
