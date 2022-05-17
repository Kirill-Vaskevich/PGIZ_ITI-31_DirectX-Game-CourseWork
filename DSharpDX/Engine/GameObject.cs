using DSharpDX.Engine.Colliders;
using DSharpDX.Graphics.Models;
using SharpDX;

namespace DSharpDX.Engine
{
    public class GameObject : DModel
    {
        public float gravY = 0.0001f;

        public float Speed { get; set; }

        public Vector3 oldPos;
        public Collider Collider { get; private set; }

        public GameObject(ColliderType type)
        {
            if (type == ColliderType.Sphere)
                Collider = new SphereCollider(new Vector3(2f, 4f, 0f), 1f);

            else if (type == ColliderType.Cube)
                Collider = new CubeCollider(new Vector3(-2f, 2f, 0f), 1f);
        }

        public GameObject() { }

        public override void SetPosition(float x, float y, float z)
        {
            if (Collider != null && !Collider.Collide)
            {
                oldPos = new Vector3(x, y, z);
                base.SetPosition(x, y, z);
                Collider.Position = new Vector3(x, y, z);
            }
            else if (Collider != null && Collider.Collide)
            {
                //Position = oldPos;
                Position = new Vector3(x, y, z);

                Collider.Position = new Vector3(x, y, z);
                return;
            }
            else
                base.SetPosition(x, y, z);
        }

        public void Stop()
        {
            Position = oldPos;
        }

        public void SetPosition(Vector3 pos)
        {
            SetPosition(pos.X, pos.Y, pos.Z);
        }
    }
}
