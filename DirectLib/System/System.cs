using DirectLib.Graphics;
using DirectLib.Graphics.Input;
using SharpDX.Windows;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DirectLib.System
{
    public class System                    // 172 lines
    {
        // Properties
        private RenderForm RenderForm { get; set; }
        public SystemConfiguration Configuration { get; private set; }
        public Input Input { get; private set; }
        public Graphics.Graphics Graphics { get; private set; }
        public Timer Timer { get; private set; }
        public Transform SpherePosition { get; private set; }
        public Transform CameraPos { get; private set; }

        // Constructor
        public System() { }

        // Methods
        public virtual bool Initialize(string title, int width, int height, bool vSync, bool fullScreen, int testTimeSeconds)
        {
            bool result = false;

            if (Configuration == null)
                Configuration = new SystemConfiguration(title, width, height, fullScreen, vSync);

            // Initialize Window.
            InitializeWindows(title);

            if (Input == null)
            {
                Input = new Input();
                if (!Input.Initialize(Configuration, RenderForm.Handle))
                    return false;
            }
            if (Graphics == null)
            {
                Graphics = new Graphics.Graphics();
                result = Graphics.Initialize(Configuration, RenderForm.Handle);
            }
            
            // Create and initialize Timer.
            Timer = new Timer();
            if (!Timer.Initialize())
            {
                Console.WriteLine("Could not initialize Timer object");
                return false;
            }

            // Create the position object.
            SpherePosition = new Transform();
            CameraPos = new Transform();

            return result;
        }

        public bool Frame()
        {
            // Read the user input.
            if (!Input.Frame() || Input.IsEscapePressed())
                return false;

            // Update the system stats.
            Timer.Frame2();            

            // Do the frame input processing.
            if (!HandleInput(Timer.FrameTime))
                return false;
            // Get the view point position/rotation.
            // Do the frame processing for the graphics object.

            SphereMoving();

            if (!Graphics.Frame(SpherePosition.GetPosition(), SpherePosition.GetRotation(), CameraPos.GetRotation()))
                return false;

            // Sync & Sphere speed boost
            SpherePosition.SetPosition(SharpDX.Vector3.Zero);
            CameraPos.SetRotation(Graphics.Camera.GetRotation());
            SpherePosition.SetRotation(Graphics.SphereModel.GetRotation());
            SpherePosition.SphereXSpeed = 0.001f * Math.Abs(CameraPos.GetRotation().Z);
            SpherePosition.SphereZSpeed = 0.001f * Math.Abs(CameraPos.GetRotation().X);

            return true;
        }

        void SphereMoving()
        {
            if (CameraPos.GetRotation().X > 1)
                SpherePosition.MoveBackward(true);
            else if (CameraPos.GetRotation().X < -1)
                SpherePosition.MoveForward(true);
            if (CameraPos.GetRotation().Z > 1)
                SpherePosition.MoveRight(true);
            else if (CameraPos.GetRotation().Z < -1)
                SpherePosition.MoveLeft(true);
        }

        private bool HandleInput(float frameTime)
        {
            // Set the frame time for calculating the updated position.
            SpherePosition.FrameTime = frameTime;
            CameraPos.FrameTime = frameTime;

            // Handle the input
            #region Ground rotation
            bool keydown = Input.IsSPressed();
            CameraPos.LookUp(keydown);
            
            keydown = Input.IsWPressed();
            CameraPos.LookDown(keydown);

            keydown = Input.IsAPressed();
            CameraPos.TurnRight(keydown);

            keydown = Input.IsDPressed();
            CameraPos.TurnLeft(keydown);
            #endregion
            
            return true;
        }

        #region Other
        public static void StartRenderForm(string title, int width, int height, bool vSync, bool fullScreen = true, int testTimeSeconds = 0)
        {
            System system = new System();
            system.Initialize(title, width, height, vSync, fullScreen, testTimeSeconds);
            system.RunRenderForm();
        }

        private void InitializeWindows(string title)
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;

            // Initialize Window.
            RenderForm = new RenderForm(title)
            {
                ClientSize = new Size(Configuration.Width, Configuration.Height),
                FormBorderStyle = SystemConfiguration.BorderStyle
            };

            // The form must be showing in order for the handle to be used in Input and Graphics objects.
            RenderForm.Show();
            RenderForm.Location = new Point((width / 2) - (Configuration.Width / 2), (height / 2) - (Configuration.Height / 2));
        }

        private void RunRenderForm()
        {
            RenderLoop.Run(RenderForm, () =>
            {
                if (!Frame())
                    ShutDown();
            });
        }

        public void ShutDown()
        {
            ShutdownWindows();

            // Release the position object.
            SpherePosition = null;
            CameraPos = null;
            // Release the Timer object
            Timer = null;

            // Release graphics and related objects.
            Graphics?.Shutdown();
            Graphics = null;
            // Release DriectInput related object.
            Input?.Shutdown();
            Input = null;
            Configuration = null;
        }

        private void ShutdownWindows()
        {
            RenderForm?.Dispose();
            RenderForm = null;
        }
        #endregion
    }
}