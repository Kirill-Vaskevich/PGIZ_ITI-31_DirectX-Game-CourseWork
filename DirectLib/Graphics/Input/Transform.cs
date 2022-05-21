using SharpDX;
using System;

namespace DirectLib.Graphics.Input
{
    public class Transform                  // 172 lines
    {
        // Variables
        public float Speed { get; private set; }

        public float SphereXSpeed { get; set; }

        public float SphereZSpeed { get; set; }

        public float CameraRotSpeed { get; set; }

        public float SphereRotSpeed { get; set; }

        // Properties
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float FrameTime { get; set; }
        public float Pitch { get; private set; }
        public float Yaw { get; private set; }
        public float Roll { get; private set; }

        public Transform()
        {
            Speed = 0.005f;
            SphereXSpeed = 0f;
            SphereZSpeed = 0f;
            CameraRotSpeed = 0.1f;
            SphereRotSpeed = 0f;
        }

        #region Public Methods Position & Rotation
        public Vector3 GetPosition()
        {
            return new Vector3(X, Y, Z);
        }

        public void SetPosition(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void SetPosition(Vector3 pos)
        {
            SetPosition(pos.X, pos.Y, pos.Z);
        }

        public Vector3 GetRotation()
        {
            return new Vector3(Pitch, Yaw, Roll);
        }

        public void SetRotation(float pitch, float yaw, float roll)
        {
            Pitch = pitch;
            Yaw = yaw;
            Roll = roll;
        }


        public void SetRotation(Vector3 rot)
        {
            SetRotation(rot.X, rot.Y, rot.Z);
        }

        #endregion

        #region Rotation
        public void TurnLeft(bool keydown)
        {
            if (keydown)
                Roll += CameraRotSpeed;
            if (Roll > 50f)
                Roll = 50f;
        }
        public void TurnRight(bool keydown)
        {
            if (keydown)
                Roll -= CameraRotSpeed;
            if (Roll < -50f)
                Roll = -50f;
        }

        public void LookDown(bool keydown)
        {
            // If the key is pressed increase the CameraRotSpeed at which the camera turns down. If not slow down the turn CameraRotSpeed.
            if (keydown)
                Pitch -= CameraRotSpeed;
            if (Pitch < -40f)
                Pitch = -40f;
        }
        internal void LookUp(bool keydown)
        {
            if (keydown)
            {
                Pitch += CameraRotSpeed;
            }

            if (Pitch > 50f)
                Pitch = 50f;
        }

        #endregion

        #region Moving
        internal void MoveForward(bool keydown)
        {
            if (keydown)
                Z += 1f * SphereZSpeed;
        }

        internal void MoveRight(bool keydown)
        {
            if (keydown)
                X += 1f * SphereXSpeed;
        }

        internal void MoveLeft(bool keydown)
        {
            if (keydown)
                X -= 1f * SphereXSpeed;
        }

        internal void MoveBackward(bool keydown)
        {
            if (keydown)
                Z -= 1f * SphereZSpeed;
        }

        #endregion
    }
}