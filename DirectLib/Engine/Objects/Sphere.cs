using DirectLib.Engine.Colliders;
using SharpDX;

namespace DirectLib.Engine.Objects
{
    public class Sphere : GameObject
    {
        public int DestoyCount { get; internal set; }

        public Sphere() : base()
        {
            Collider = new SphereCollider(Vector3.Zero, 1f);
        }

        public void DestroyBlock(Cube cube)
        {
            if (DestoyCount > 0)
                cube.Shutdown();
        }
    }
}
