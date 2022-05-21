using SharpDX;
using DirectLib.Engine.Colliders;

namespace DirectLib.Engine.Objects
{
    public class Cube : GameObject
    {
        public Cube() : base()
        {
            Collider = new CubeCollider(Vector3.Zero, 1.8f);
        }
    }
}
