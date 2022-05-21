using DirectLib.Engine;
using DirectLib.Engine.Colliders;
using DirectLib.Graphics.Models;
using DirectLib.Graphics.Data;
using DirectLib.Graphics.Shaders;
using DirectLib.System;
using SharpDX;
using System;
using System.Collections.Generic;
using DirectLib.Engine.Objects;

namespace DirectLib.Graphics
{
    public class Graphics                  // 309 lines
    {
        // Properties
        private DX11 D3D { get; set; }
        public Camera Camera { get; set; }

        #region Data
        private Light Light { get; set; }
        private RenderTexture RenderTexture { get; set; }
        #endregion

        #region Models
        //private GameObject CubeModel { get; set; }
        public Ground GroundModel { get; set; }
        public Sphere SphereModel { get; set; }
        
        public MapCreator MapCreator { get; set; }

        private List<GameObject> _gameObjects;
        #endregion

        #region Shaders
        public DepthShader DepthShader { get; set; }
        public ShadowShader ShadowShader { get; set; }
        #endregion     

        #region Variables
        private Vector3 _lightPosition = new Vector3(0, 20, -5);
        #endregion

        public Graphics() { }

        // Construtor

        // Methods.
        public bool Initialize(SystemConfiguration configuration, IntPtr windowHandle)
        {
            try
            {
                #region Initialize System
                // Create the Direct3D object.
                D3D = new DX11();

                // Initialize the Direct3D object.
                if (!D3D.Initialize(configuration, windowHandle))
                    return false;
                #endregion

                #region Initialize Camera
                // Create the camera object
                Camera = new Camera();

                // Set the initial position of the camera.
                Camera.SetPosition(0f, 25f, -10f);
                #endregion

                #region Initialize Models

                // Create the sphere model object.
                SphereModel = new Sphere();

                // Initialize the sphere model object.
                if (!SphereModel.Initialize(D3D.Device, "sphere.txt", "bump01.bmp", Vector3.One))
                    return false;

                // Set the position for the sphere model.
                //SphereModel.SetPosition(-6f, 0f ,4f);

                // Create the ground model object.
                GroundModel = new Ground();

                // Initialize the ground model object.
                if (!GroundModel.Initialize(D3D.Device, "ground.txt", "texture_dirt.jpg", new Vector3(40f, 40f, 40f)))
                    return false;

                // Set the position for the ground model.
                GroundModel.SetPosition(0f, -1f, 30f);

                _gameObjects = new List<GameObject>();

                MapCreator = new MapCreator(D3D.Device, _gameObjects);

                _gameObjects.Add(SphereModel);
                _gameObjects.Add(GroundModel);

                Vector3 spherePos = MapCreator.LoadLevel1();
                SphereModel.SetPosition(spherePos);
                
                #endregion

                #region Data variables.
                // Create the light object.
                Light = new Light();

                // Initialize the light object.
                Light.SetAmbientColor(0.15f, 0.15f, 0.15f, 1f);
                Light.SetDiffuseColor(1f, 1f, 1f, 1f);
                Light.SetLookAt(0f, 0f, 0f);
                Light.GenerateProjectionMatrix();

                // Create the render to texture object.
                RenderTexture = new RenderTexture();

                // Initialize the render to texture object.
                if (!RenderTexture.Initialize(D3D.Device, configuration))
                    return false;

                // Create the depth shader object.
                DepthShader = new DepthShader();

                // Initialize the depth shader object.
                if (!DepthShader.Initialize(D3D.Device, windowHandle))
                    return false;
                #endregion

                #region Initialize Shaders
                // Create the shadow shader object.
                ShadowShader = new ShadowShader();

                // Initialize the shadow shader object.
                if (!ShadowShader.Initialize(D3D.Device, windowHandle))
                    return false;
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not initialize Direct3D\nError is '" + ex.Message + "'");
                return false;
            }
        }

        public bool Frame(Vector3 spherePos, Vector3 sphereRot, Vector3 cameraRot)
        {
            // Set the rotation of the camera.
            Camera.SetRotation(cameraRot);
            spherePos = -spherePos;

            bool collide = IsCollided();
            StopObjects(collide);
            MoveObjects(spherePos);

            // Update the position of the light.
            Light.Position = _lightPosition;

            if (!Render())
                return false;
            return true;
        }

        bool IsCollided()
        {
            GameObject obj;

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                obj = _gameObjects[i];

                if (obj is Cube && SphereModel.Collider.IsCollided(obj))
                {
                    if (obj is DestroyBonus)
                    {
                        (obj as DestroyBonus).Effect(SphereModel);
                        _gameObjects.RemoveAt(i);
                    }
                    else if (obj is UnscaleBonus)
                    {
                        UnscaleBonus bonus = obj as UnscaleBonus;
                        SphereModel.Initialize(D3D.Device, "sphere.txt", "bump01.bmp", new Vector3(bonus.Scale, bonus.Scale, bonus.Scale));
                        SphereModel.Collider = new SphereCollider(SphereModel.GetPosition(), bonus.Scale);
                        _gameObjects.RemoveAt(i);
                    }
                    else if (SphereModel.DestoyCount > 0)
                    {
                        _gameObjects.RemoveAt(i);
                        SphereModel.DestoyCount--;
                    }
                    else if (obj is Finish)
                        MapCreator.EndLevel();
                    else
                        return true;
                }
            }
            return false;
        }

        void StopObjects(bool stop)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (!(_gameObjects[i] is Sphere))
                {
                    _gameObjects[i].Collider.Collide = stop;
                    if (stop)
                        _gameObjects[i].Stop();
                }
            }
        }

        void MoveObjects(Vector3 position)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i] is Sphere)
                    continue;
                Vector3 pos = _gameObjects[i].GetPosition();
                _gameObjects[i].SetPosition(pos.X + position.X, pos.Y + position.Y, pos.Z + position.Z);
            }
        }

        #region Rendering and Shutdown

        public bool Render()
        {
            // First render the scene to a texture.
            if (!RenderSceneToTexture())
                return false;

            // Clear the buffers to begin the scene.
            D3D.BeginScene(0f, 0f, 0f, 1f);

            // Generate the view matrix based on the camera's position.
            Camera.Render();

            // Generate the light view matrix based on the light's position.
            Light.GenerateViewMatrix();

            // Get the world, view, and projection matrices from the camera and d3d objects.
            Matrix viewMatrix = Camera.ViewMatrix;
            Matrix projectionMatrix = D3D.ProjectionMatrix;

            // Get the light's view and projection matrices from the light object.
            Matrix lightViewMatrix = Light.ViewMatrix;
            Matrix lightProjectionMatrix = Light.ProjectionMatrix;
            Matrix worldMatrix;
            //GameObject[] gameObjects = { CubeModel, SphereModel, GroundModel };

            foreach (GameObject obj in _gameObjects)
            {
                // Reset the world matrix.
                worldMatrix = D3D.WorldMatrix;
                // Setup the translation matrix for the cube model.
                Vector3 position = obj.GetPosition();
                Vector3 rotation = obj.GetRotation();
                Matrix.Translation(position.X, position.Y, position.Z, out worldMatrix);
                //Matrix.RotationYawPitchRoll(rotation.Y, rotation.X, rotation.Z, out worldMatrix);

                // Put the cube model vertex and index buffers on the graphics pipeline to prepare them for drawing.
                obj.Render(D3D.DeviceContext);

                // Render the model using the shadow shader.
                if (!ShadowShader.Render(D3D.DeviceContext, obj.IndexCount, worldMatrix, viewMatrix, projectionMatrix, lightViewMatrix, lightProjectionMatrix, obj.Texture.TextureResource, RenderTexture.ShaderResourceView, Light.Position, Light.AmbientColor, Light.DiffuseColour))
                    return false;
            }

            // Present the rendered scene to the screen.
            D3D.EndScene();

            return true;
        }

        private bool RenderSceneToTexture()
        {
            // Set the render target to be the render to texture.
            RenderTexture.SetRenderTarget(D3D.DeviceContext);

            // Clear the render to texture.
            RenderTexture.ClearRenderTarget(D3D.DeviceContext, 0f, 0f, 0f, 1f);

            // Generate the light view matrix based on the light's position.
            Light.GenerateViewMatrix();

            // Get the view and orthographic matrices from the light object.
            Matrix lightViewMatrix = Light.ViewMatrix;
            Matrix lightOrthoMatrix = Light.ProjectionMatrix;
            Matrix worldMatrix;
            //GameObject[] gameObjects = { CubeModel, SphereModel, GroundModel };

            foreach (GameObject obj in _gameObjects)
            {
                // Get the world matrix from the d3d object.
                worldMatrix = D3D.WorldMatrix;

                // Setup the translation matrix for the model.
                Vector3 pos = obj.GetPosition();
                Vector3 rot = obj.GetRotation();
                Matrix.Translation(pos.X, pos.Y, pos.Z, out worldMatrix);
                //Matrix.RotationYawPitchRoll(rot.Y, rot.X, rot.Z, out worldMatrix);

                // Render the cube model with the depth shader.
                obj.Render(D3D.DeviceContext);
                if (!DepthShader.Render(D3D.DeviceContext, obj.IndexCount, worldMatrix, lightViewMatrix, lightOrthoMatrix))
                    return false;
            }

            // Reset the render target back to the original back buffer and not the render to texture anymore.
            D3D.SetBackBufferRenderTarget();

            // Reset the viewport back to the original.
            D3D.ResetViewPort();

            return true;
        }


        public void Shutdown()
        {
            // Release the light object.
            Light = null;
            // Release the camera object.
            Camera = null;

            // Release the shadow shader object.
            ShadowShader?.ShutDown();
            ShadowShader = null;
            // Release the depth shader object.
            DepthShader?.ShutDown();
            DepthShader = null;
            /// Release the render to texture object.
            RenderTexture?.Shutdown();
            RenderTexture = null;
            // Release the ground model object.
            GroundModel?.Shutdown();
            GroundModel = null;
            // Release the sphere model object.
            SphereModel?.Shutdown();
            SphereModel = null;
            // Release the cube model object.
            //CubeModel?.Shutdown();
            //CubeModel = null;
            // Release the Direct3D object.
            D3D?.ShutDown();
            D3D = null;
        }
        #endregion
    }
}
