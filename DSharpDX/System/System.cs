using DSharpDX.Graphics;
using DSharpDX.Graphics.Input;
using SharpDX.Windows;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DSharpDX.System
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
        public Transform GroundPosition { get; private set; }
        public Transform CameraPos { get; private set; }

        // Constructor
        public System() { }

        public static void StartRenderForm(string title, int width, int height, bool vSync, bool fullScreen = true, int testTimeSeconds = 0)
        {
            System system = new System();
            system.Initialize(title, width, height, vSync, fullScreen, testTimeSeconds);
            system.RunRenderForm();
        }

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
            GroundPosition = new Transform();
            CameraPos = new Transform();
            // Set the initial position of the viewer to the same as the initial camera position.
            SpherePosition.SetPosition(Graphics.SphereModel.GetPosition());
            GroundPosition.SetPosition(Graphics.GroundModel.GetPosition());
            CameraPos.SetPosition(Graphics.Camera.GetPosition());

            return result;
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
        public bool Frame()
        {
            // Read the user input.
            if (!Input.Frame() || Input.IsEscapePressed())
                return false;

            // Update the system stats.
            Timer.Frame2();

            SpherePosition.SetPosition(Graphics.SphereModel.GetPosition());
            //GroundPosition.SetPosition(Graphics.GroundModel.GetPosition());
            //CameraPos.SetPosition(Graphics.Camera.GetPosition());
            CameraPos.SetRotation(Graphics.Camera.GetRotation());

            // Do the frame input processing.
            if (!HandleInput(Timer.FrameTime))
                return false;
            // Get the view point position/rotation.
            // Do the frame processing for the graphics object.

            if (!Graphics.Frame(SpherePosition.GetPosition(), SharpDX.Vector3.Zero, CameraPos.GetRotation()))
                return false;

            return true;
        }
        private bool HandleInput(float frameTime)
        {
            // Set the frame time for calculating the updated position.
            SpherePosition.FrameTime = frameTime;
            GroundPosition.FrameTime = frameTime;
            CameraPos.FrameTime = frameTime;

            // Handle the input
            bool keydown = Input.IsLeftArrowPressed();
            SpherePosition.MoveLeft(keydown);
            keydown = Input.IsRightArrowPressed();
            SpherePosition.MoveRight(keydown);
            keydown = Input.IsUpArrowPressed();
            SpherePosition.MoveForward(keydown);
            keydown = Input.IsDownArrowPressed();
            SpherePosition.MoveBackward(keydown);
            keydown = Input.IsSPressed();
            CameraPos.LookUp(keydown);
            SpherePosition.MoveBackward(keydown);
            keydown = Input.IsWPressed();
            CameraPos.LookDown(keydown);
            SpherePosition.MoveForward(keydown);
            keydown = Input.IsAPressed();
            CameraPos.TurnRight(keydown);
            SpherePosition.MoveLeft(keydown);
            keydown = Input.IsDPressed();
            CameraPos.TurnLeft(keydown);
            SpherePosition.MoveRight(keydown);

            return true;
        }
        public void ShutDown()
        {
            ShutdownWindows();

            // Release the position object.
            SpherePosition = null;
            GroundPosition = null;
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
    }
}