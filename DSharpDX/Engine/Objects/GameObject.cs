using DSharpDX.Engine.Colliders;
using DSharpDX.Graphics.Models;
using SharpDX;

namespace DSharpDX.Engine.Objects
{
    public class GameObject : Model
    {
        public Collider Collider { get; protected set; }

        public GameObject() { }

        private Vector3 _oldPos;

        private Vector3 _rotation;

        protected Vector3 position;

        #region Moving
        public void SetPosition(float x, float y, float z)
        {
            if (Collider.Collide)
            {
                position = new Vector3(x, y, z);
                Collider.Position = position;
            }
            else
            {
                _oldPos = position;
                position = new Vector3(x, y, z);
                Collider.Position = position;
            }
        }

        public void Stop()
        {
            position = _oldPos;
        }

        public void SetPosition(Vector3 pos)
        {
            SetPosition(pos.X, pos.Y, pos.Z);
        }

        public Vector3 GetPosition()
        {
            return position;
        }

        public void SetRotation(float x, float y, float z)
        {
            _rotation = new Vector3(x, y, z);
        }

        public void SetRotation(Vector3 rot)
        {
            SetRotation(rot.X, rot.Y, rot.Z);
        }

        public Vector3 GetRotation()
        {
            return _rotation;
        }
        #endregion
    }
}
