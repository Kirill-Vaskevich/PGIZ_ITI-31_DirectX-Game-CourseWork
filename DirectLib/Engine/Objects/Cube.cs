using SharpDX;
using DirectLib.Engine.Colliders;
using SharpDX.Direct3D11;

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
            position = pos;
            Collider = new CubeCollider(pos, width);
        }

        public bool Initialize(Device device, Vector3 scale)
        {
            return base.Initialize(device, "cube.txt", "stone.bmp", scale);
        }
    }
}
