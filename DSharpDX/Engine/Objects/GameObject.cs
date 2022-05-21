using DSharpDX.Engine.Colliders;
using DSharpDX.Graphics.Models;
using SharpDX;

namespace DSharpDX.Engine.Objects
{
    public class GameObject : Model
    {
        private Vector3 _oldPos;
        public Collider Collider { get; protected set; }

        public GameObject() { }

        public Vector3 Rotation;


        #region Moving
        public override void SetPosition(float x, float y, float z)
        {
            if (Collider != null && !Collider.Collide)
            {
                _oldPos = new Vector3(x, y, z);
                base.SetPosition(x, y, z);
                Collider.Position = new Vector3(x, y, z);
            }
            else if (Collider != null && Collider.Collide)
            {
                Position = new Vector3(x, y, z);

                Collider.Position = new Vector3(x, y, z);
            }
            else
                base.SetPosition(x, y, z);
        }


        public void Stop()
        {
            Position = _oldPos;
        }

        public void SetPosition(Vector3 pos)
        {
            SetPosition(pos.X, pos.Y, pos.Z);
        }

        public void SetRotation(float x, float y, float z)
        {
            Rotation.X = x;
            Rotation.Y = y;
            Rotation.Z = z;
        }

        public void SetRotation(Vector3 rot)
        {
            SetRotation(rot.X, rot.Y, rot.Z);
        }

        public Vector3 GetRotation()
        {
            return Rotation;
        }
        #endregion
    }
}
