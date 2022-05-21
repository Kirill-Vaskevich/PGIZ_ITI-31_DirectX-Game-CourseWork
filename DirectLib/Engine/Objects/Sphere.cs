using DirectLib.Engine.Colliders;
using SharpDX;

namespace DirectLib.Engine.Objects
{
    public class Sphere : GameObject
    {
        public Sphere() : base()
        {
            Collider = new SphereCollider(Vector3.Zero, 1f);
        }
    }
}
