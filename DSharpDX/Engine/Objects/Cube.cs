using SharpDX;
using DSharpDX.Engine.Colliders;

namespace DSharpDX.Engine.Objects
{
    public class Cube : GameObject
    {
        public Cube() : base()
        {
            Collider = new CubeCollider(Vector3.Zero, 1.8f);
        }
    }
}
