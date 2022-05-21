using SharpDX;
using System;
using DSharpDX.Engine.Objects;

namespace DSharpDX.Engine.Colliders
{
    public class SphereCollider : Collider
    {
        private float _radius;

        public SphereCollider(Vector3 pos, float radius) : base(pos)
        {
            _radius = radius;
        }

        public override bool IsCollided(GameObject gameObject)
        {
            Vector3[] vertices = (gameObject.Collider as CubeCollider).GetVertices();

            foreach (Vector3 vert in vertices)
            {
                float dx = vert.X - Position.X;
                float dy = vert.Y - Position.Y;
                float dz = vert.Z - Position.Z;
                float r2 = dx * dx + dy * dy + dz * dz;

                if (r2 <= Math.Pow(_radius, 2))
                {
                    Collide = true;
                    return true;
                }
            }
            Collide = false;

            return false;
        }
    }
}
