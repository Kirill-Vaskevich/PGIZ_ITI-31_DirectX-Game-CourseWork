using SharpDX;

namespace DirectLib.Engine.Objects
{
    public class Bonus : Cube
    {
        public Bonus() : base(Vector3.Zero, 1f)
        {

        }

        public Bonus(Vector3 pos, float width) : base(pos, width)
        {

        }

        public virtual void Effect(Sphere obj)
        {

        }
    }
}
