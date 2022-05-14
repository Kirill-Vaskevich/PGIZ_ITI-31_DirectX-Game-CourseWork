using System;
using SharpDX;

namespace DSharpDX.Graphics.Input
{
    public class DPosition                  // 172 lines
    {
        // Variables
        private float leftTurnSpeed, rightTurnSpeed;
        private float upLookSpeed, downLookSpeed;
        public float Speed { get; private set; }

        // Properties
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float FrameTime { get; set; }
        public float RotationX { get; private set; }
        public float RotationY { get; private set; }
        public float RotationZ { get; private set; }

        public DPosition()
        {
            Speed = 0.03f;
        }

        // Public Methods
        public void SetPosition(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void SetPosition(Vector3 pos)
        {
            X = pos.X;
            Y = pos.Y;
            Z = pos.Z;
        }

        #region Rotation
        public void TurnLeft(bool keydown)
        {
            // If the key is pressed increase the speed at which the camera turns left. If not slow down the turn speed.
            if (keydown)
            {
                leftTurnSpeed += FrameTime * 0.01f;
                if (leftTurnSpeed > FrameTime * 0.15)
                    leftTurnSpeed = FrameTime * 0.15f;
            }
            else
            {
                leftTurnSpeed -= FrameTime * 0.005f;
                if (leftTurnSpeed < 0)
                    leftTurnSpeed = 0;
            }

            // Update the rotation using the turning speed.
            RotationY -= leftTurnSpeed;

            // Keep the rotation in the 0 to 360 range.
            if (RotationY < 0)
                RotationY += 360;
        }
        public void TurnRight(bool keydown)
        {
            // If the key is pressed increase the speed at which the camera turns right. If not slow down the turn speed.
            if (keydown)
            {
                rightTurnSpeed += FrameTime * 0.01f;
                if (rightTurnSpeed > FrameTime * 0.15)
                    rightTurnSpeed = FrameTime * 0.15f;
            }
            else
            {
                rightTurnSpeed -= FrameTime * 0.005f;
                if (rightTurnSpeed < 0)
                    rightTurnSpeed = 0;
            }

            // Update the rotation using the turning speed.
            RotationY += rightTurnSpeed;

            // Keep the rotation in the 0 to 360 range which is looking stright Up.
            if (RotationY > 360)
                RotationY -= 360;
        }
        public void LookDown(bool keydown)
        {
            // If the key is pressed increase the speed at which the camera turns down. If not slow down the turn speed.
            if (keydown)
            {
                downLookSpeed += FrameTime * 0.01f;
                if (downLookSpeed > FrameTime * 0.03)
                    downLookSpeed = FrameTime * 0.03f;
            }
            else
            {
                downLookSpeed -= FrameTime * 0.005f;
                if (downLookSpeed < 0)
                    downLookSpeed = 0;
            }

            // Update the rotation using the turning speed.
            RotationX += downLookSpeed;

            // Keep the rotation maximum 90 degrees which is looking straight down.
            if (RotationX < -90)
                RotationX = -90;
        }
        internal void LookUp(bool keydown)
        {
            if (keydown)
            {
                // Update the upward rotation speed movement based on the frame time and whether the user is holding the key down or not.
                upLookSpeed += FrameTime * 0.001f;

                if (upLookSpeed > FrameTime * 0.03f)
                    upLookSpeed = FrameTime * 0.03f;
            }
            else
            {
                upLookSpeed -= FrameTime * 0.005f;
                if (upLookSpeed < 0.0f)
                    upLookSpeed = 0.0f;
            }

            // Update the rotation.
            RotationX -= upLookSpeed;

            // Keep the rotation maximum 90 degrees.
            if (RotationX > 90.0f)
                RotationX = 90.0f;
        }

        #endregion

        internal void MoveForward(bool keydown)
        {
            if (keydown)
                Z += 1f * Speed;
        }

        internal void MoveRight(bool keydown)
        {
            if (keydown)
                X += 1f * Speed;
        }

        internal void MoveLeft(bool keydown)
        {
            if (keydown)
                X -= 1f * Speed;
        }

        internal void MoveUp(bool keydown)
        {
            if (keydown)
                Y += 1f * Speed;
        }

        internal void MoveBackward(bool keydown)
        {
            if (keydown)
                Z -= 1f * Speed;
        }
    }
}