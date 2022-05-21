using SharpDX;

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
    }
}
