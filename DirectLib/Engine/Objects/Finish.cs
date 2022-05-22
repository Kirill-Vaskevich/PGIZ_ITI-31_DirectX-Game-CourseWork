using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D11;

namespace DirectLib.Engine.Objects
{
    public class Finish : Cube
    {
        public Finish(Vector3 pos, float width) : base(pos, width)
        {
            //position = pos;
        }

        public bool Initialize(Device device, Vector3 scale)
        {
            return base.Initialize(device, "cube.txt", "bump.bmp", scale);
        }
    }
}
