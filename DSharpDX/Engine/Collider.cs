﻿using SharpDX;

namespace DSharpDX.Engine
{
    public abstract class Collider
    {
        public Vector3 Position { get; protected internal set; }

        public Collider(Vector3 pos)
        {
            Position = pos;
        }

        public virtual bool IsCollided(GameObject gameObject)
        {
            Vector3 pos = gameObject.Collider.Position;

            if (Position.Y <= pos.Y)
            {
                return true;
            }
            return false;
        }
    }
}
