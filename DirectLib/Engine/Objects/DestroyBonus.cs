using SharpDX;
using SharpDX.Direct3D11;

namespace DirectLib.Engine.Objects
{
    public class DestroyBonus : Bonus
    {
        public DestroyBonus(Vector3 pos, float width) : base(pos, width)
        {
            SetPosition(pos);
        }

        public override void Effect(Sphere obj)
        {
            obj.DestoyCount++;
        }

        public bool Initialize(Device device, Vector3 scale)
        {
            return base.Initialize(device, "cube.txt", "texture_grass.jpg", scale);
        }
    }
}
