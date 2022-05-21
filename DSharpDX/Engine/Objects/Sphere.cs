using DSharpDX.Engine.Colliders;
using SharpDX;

namespace DSharpDX.Engine.Objects
{
    public class Sphere : GameObject
    {
        public Sphere() : base()
        {
            Collider = new SphereCollider(Vector3.Zero, 1f);
        }
    }
}
