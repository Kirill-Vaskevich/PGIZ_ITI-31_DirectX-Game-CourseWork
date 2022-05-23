using SharpDX;
using System;

namespace DirectLib.Graphics.Models
{
    public class Camera                    // 63 lines
    {
        // Properties.
        private float X { get; set; }
        private float Y { get; set; }
        private float Z { get; set; }
        private float Pitch { get; set; }
        private float Yaw { get; set; }
        private float Roll { get; set; }
        public Matrix ViewMatrix { get; private set; }

        public Vector3 LookAt = new Vector3(0, -1, 1);
        // 
        // Constructor
        public Camera() { }

        #region Moving
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

        public void SetRotation(float x, float y, float z)
        {
            Pitch = x;
            Yaw = y;
            Roll = z;
        }

        public Vector3 GetRotation()
        {
            return new Vector3(Pitch, Yaw, Roll);
        }

        public void SetRotation(Vector3 rot)
        {
            SetRotation(rot.X, rot.Y, rot.Z);
        }

        public Vector3 GetPosition()
        {
            return new Vector3(X, Y, Z);
        }
        #endregion

        public void Render()
        {
            // Setup the position of the camera in the world.
            Vector3 position = new Vector3(X, Y, Z);

            // Setup where the camera is looking  forwardby default.
            Vector3 lookAt = LookAt;

            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = Pitch * 0.0174532925f;
            float yaw = Yaw * 0.0174532925f;
            float roll = Roll * 0.0174532925f;

            //// Create the rotation matrix from the yaw, pitch, and roll values.
            Matrix rotationMatrix = Matrix.RotationYawPitchRoll(yaw, pitch, roll);

            // Transform the lookAt and up vector by the rotation matrix so the view is correctly rotated at the origin.
            position = Vector3.TransformCoordinate(position, rotationMatrix);
            Vector3 up = Vector3.TransformCoordinate(Vector3.UnitY, rotationMatrix);
            // Finally create the view matrix from the three updated vectors.
            ViewMatrix = Matrix.LookAtLH(position, lookAt, up);
        }
    }
}
