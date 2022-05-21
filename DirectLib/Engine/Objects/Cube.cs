using SharpDX;
using DirectLib.Engine.Colliders;

namespace DirectLib.Engine.Objects
{
    public class Cube : GameObject
    {
        public Cube()
        {
            Collider = new CubeCollider(Vector3.Zero, 1.8f);
        }

        public Cube(Vector3 pos, float width)
        {
            Collider = new CubeCollider(pos, width);
        }
    }
}
