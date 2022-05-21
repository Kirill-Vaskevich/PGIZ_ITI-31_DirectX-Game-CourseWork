using SharpDX;

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
    }
}
