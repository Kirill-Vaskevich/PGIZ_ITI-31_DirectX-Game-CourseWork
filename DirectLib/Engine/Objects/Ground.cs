using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using DirectLib.Engine.Colliders;

namespace DirectLib.Engine.Objects
{
    public class Ground : GameObject
    {
        public Ground() : base()
        {
            Collider = new GroundCollider();
        }
    }
}
