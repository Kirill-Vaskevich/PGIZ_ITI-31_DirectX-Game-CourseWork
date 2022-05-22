using SharpDX;
using SharpDX.Direct3D11;

namespace DirectLib.Engine.Objects
{
    public class UnscaleBonus : Bonus
    {
        public float Scale { get; set; }

        public UnscaleBonus(Vector3 pos, float width, float scale) : base(pos, width)
        {
            SetPosition(pos);
            Scale = scale;
        }

        public bool Initialize(Device device, Vector3 scale)
        {
            return base.Initialize(device, "cube.txt", "seafloor.bmp", scale);
        }
    }
}
