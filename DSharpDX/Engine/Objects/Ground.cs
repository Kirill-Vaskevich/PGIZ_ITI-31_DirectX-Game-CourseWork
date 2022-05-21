using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using DSharpDX.Engine.Colliders;

namespace DSharpDX.Engine.Objects
{
    public class Ground : GameObject
    {
        public Ground() : base()
        {
            Collider = new GroundCollider();
        }
    }
}
