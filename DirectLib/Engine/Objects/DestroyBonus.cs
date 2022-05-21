using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace DirectLib.Engine.Objects
{
    public class DestroyBonus : Bonus
    {
        public DestroyBonus(Vector3 pos, float width) : base(pos, width) { }

        public override void Effect(Sphere obj)
        {
            obj.DestoyCount++;
        }
    }
}
