using SharpDX;

namespace DSharpDX.Graphics.Input
{
    public class DPosition                  // 172 lines
    {
        // Variables
        public float Speed { get; private set; }

        public float SphereSpeed { get; private set; }

        public float CameraRotSpeed { get; private set; }

        // Properties
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float FrameTime { get; set; }
        public float Pitch { get; private set; }
        public float Yaw { get; private set; }
        public float Roll { get; private set; }

        public DPosition()
        {
            Speed = 0.005f;
            SphereSpeed = 0.01f;
            CameraRotSpeed = 0.1f;
        }

        // Public Methods
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

        #region Rotation
        public void TurnLeft(bool keydown)
        {
            if (keydown)
                Roll += CameraRotSpeed;
            
            // Keep the rotation in the 0 to 360 range.
            /*if (Roll < 0)
                Roll += 360;*/
        }
        public void TurnRight(bool keydown)
        {
            if (keydown)
                Roll -= CameraRotSpeed;

            if (Roll > 360)
                Roll -= 360;
        }

        public void LookDown(bool keydown)
        {
            // If the key is pressed increase the CameraRotSpeed at which the camera turns down. If not slow down the turn CameraRotSpeed.
            if (keydown)
            {
                Pitch -= CameraRotSpeed;
            }
            // Keep the rotation maximum 90 degrees which is looking straight down.
            if (Pitch < -90f)
                Pitch = -90f;
        }
        internal void LookUp(bool keydown)
        {
            if (keydown)
            {
                Pitch += CameraRotSpeed;
            }

            // Keep the rotation maximum 90 degrees.
            if (Pitch > 90f)
                Pitch = 90f;
        }

        #endregion

        #region Moving
        internal void MoveForward(bool keydown)
        {
            if (keydown)
                Z += 1f * SphereSpeed;
        }

        internal void MoveRight(bool keydown)
        {
            if (keydown)
                X += 1f * SphereSpeed;
        }

        internal void MoveLeft(bool keydown)
        {
            if (keydown)
                X -= 1f * SphereSpeed;
        }

        internal void MoveUp(bool keydown)
        {
            if (keydown)
                Y += 1f * SphereSpeed;
        }

        internal void MoveBackward(bool keydown)
        {
            if (keydown)
                Z -= 1f * SphereSpeed;
        }

        #endregion
    }
}