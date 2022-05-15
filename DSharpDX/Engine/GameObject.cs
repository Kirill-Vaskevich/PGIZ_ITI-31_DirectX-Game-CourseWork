using DSharpDX.Graphics.Models;
using SharpDX;
using DSharpDX.Engine.Colliders;

namespace DSharpDX.Engine
{
    public class GameObject : DModel
    {
        public float gravY = 0.0001f;

        public GameObject(ColliderType type)
        {
            if (type == ColliderType.Sphere)
                Collider = new SphereCollider(new Vector3(2f, 4f, 0f), 1f);

            else if (type == ColliderType.Cube)
                Collider = new CubeCollider(new Vector3(-2f, 2f, 0f), 1f);
        }

        public GameObject() { }

        public Collider Collider { get; private set; }

        public override void SetPosition(float x, float y, float z)
        {
            base.SetPosition(x, y, z);
            if (Collider != null)
                Collider.Position = new Vector3(x, y, z);
        }
    }
}
