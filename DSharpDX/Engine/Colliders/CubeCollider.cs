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
            float step = 2f * Width / 10f;
            List<Vector3> vertices = new List<Vector3>();
            //front and back
            for (float x = min.X; x <= max.X; x += step)
            {
                for (float y = min.Y; y <= max.Y; y += step)
                {
                    vertices.Add(new Vector3(x, y, min.Z));
                    vertices.Add(new Vector3(x, y, max.Z));
                }
            }
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

        public Vector3[] GetVertices1()
        {
            Vector3 min = Position - Width;
            Vector3 max = Position + Width;
            Vector3[] vertices = new Vector3[22]
            {
                min, // left fragment
                new Vector3(min.X, max.Y, min.Z),
                new Vector3(min.X, max.Y, max.Z),
                new Vector3(min.X, min.Y, max.Z),
                new Vector3(min.X, Position.Y, Position.Z), // left fragment center
                max, // right fragment
                new Vector3(max.X, max.Y, min.Z),
                new Vector3(max.X, min.Y, min.Z),
                new Vector3(max.X, min.Y, max.Z),
                new Vector3(max.X, Position.Y, Position.Z), // right fragment center
                new Vector3(Position.X, min.Y, Position.Z), // front fragment center
                new Vector3(Position.X, max.Y, Position.Z), // back fragment center
                new Vector3(Position.X, Position.Y, min.Z), // up fragment center
                new Vector3(Position.X, Position.Y, max.Z),  // bottom fragment center
                new Vector3(min.X, Position.Y, min.Z),
                new Vector3(min.X, Position.Y, max.Z),
                new Vector3(max.X, Position.Y, min.Z),
                new Vector3(max.X, Position.Y, max.Z),
                new Vector3(Position.X, max.Y, min.Z),
                new Vector3(Position.X, max.Y, max.Z),
                new Vector3(min.X, max.Y, Position.Z),
                new Vector3(max.X, max.Y, Position.Z),

            };

            return vertices;
        }

        public override bool IsCollided(GameObject game)
        {
            return false;
        }
    }
}
