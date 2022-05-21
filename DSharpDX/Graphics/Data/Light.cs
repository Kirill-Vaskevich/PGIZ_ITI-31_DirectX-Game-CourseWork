using DSharpDX.System;
using SharpDX;
using System;

namespace DSharpDX.Graphics.Data
{
    public class Light                 // 48 lines
    {
        // Properties
        public Vector4 AmbientColor { get; private set; }
        public Vector4 DiffuseColour { get; private set; }
        public Vector3 Position { get; set; }
        public Vector3 LookAt { get; set; }
        public Matrix ViewMatrix { get; set; }
        public Matrix ProjectionMatrix { get; set; }

        // Methods
        public void SetAmbientColor(float red, float green, float blue, float alpha)
        {
            AmbientColor = new Vector4(red, green, blue, alpha);
        }
        public void SetDiffuseColor(float red, float green, float blue, float alpha)
        {
            DiffuseColour = new Vector4(red, green, blue, alpha);
        }
        public void GenerateViewMatrix()
        {
            // Setup the vector that points upwards.
            Vector3 upVector = Vector3.Up;

            // Create the view matrix from the three vectors.
            ViewMatrix = Matrix.LookAtLH(Position, LookAt, upVector);
        }
        public void GenerateProjectionMatrix()
        {
            // Setup field of view and screen aspect for a square light source.
            float fieldOfView = (float)Math.PI / 2f;
            float screenAspect = 1f;

            // Create the projection matrix for the light.
            ProjectionMatrix = Matrix.PerspectiveFovLH(fieldOfView, screenAspect, SystemConfiguration.ScreenNear, SystemConfiguration.ScreenDepth);
        }
        public void SetLookAt(float x, float y, float z)
        {
            LookAt = new Vector3(x, y, z);
        }
    }
}