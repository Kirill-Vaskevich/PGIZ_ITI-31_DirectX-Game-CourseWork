using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using DirectLib.Engine.Colliders;
using SharpDX.Direct3D11;

namespace DirectLib.Engine.Objects
{
    public class Ground : GameObject
    {
        public Ground() : base()
        {
            Collider = new GroundCollider();
        }

        public bool Initialize(Device device, Vector3 scale)
        {
            return base.Initialize(device, "ground.txt", "texture_dirt.jpg", scale);
        }
    }
}
