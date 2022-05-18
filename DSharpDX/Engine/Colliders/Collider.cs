﻿using SharpDX;

namespace DSharpDX.Engine.Colliders
{
    public abstract class Collider
    {
        public Vector3 Position { get; protected internal set; }

        public bool Collide { get; protected set; }

        public Collider(Vector3 pos)
        {
            Position = pos;
        }

        public virtual bool IsCollided(GameObject gameObject)
        {
            return true;
        }
    }
}
