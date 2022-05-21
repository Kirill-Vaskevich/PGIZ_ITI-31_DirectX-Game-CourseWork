using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D11;
using DirectLib.Engine.Objects;

namespace DirectLib.Engine
{
    public class MapCreator
    {
        private Device _device;

        private Vector3 _spherePos;

        private List<GameObject> _gameObjects;

        private int[,] _map1 =
        {
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 1, 0, 2, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 4, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1 },
            { 1, 5, 0, 0, 1, 3, 1, 0, 1, 1, 0, 1, 0, 1 },
            { 1, 1, 1, 1, 1, 2, 1, 0, 0, 1, 0, 0, 0, 1 },
            { 0, 1, 0, 0, 0, 0, 1, 1, 0, 1, 0, 1, 0, 1 },
            { 0, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 1, 0, 1 },
            { 0, 1, 3, 0, 1, 1, 1, 4, 1, 1, 0, 1, 0, 1 },
            { 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1 },
            { 0, 1, 1, 2, 0, 0, 0, 1, 1, 2, 0, 1, 0, 1 },
            { 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1 },
            { 0, 1, 0, 1, 1, 1, 0, 0, 2, 0, 1, 1, 1, 1 },
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
        };

        public MapCreator(Device device, List<GameObject> list)
        {
            _device = device;
            _gameObjects = list;
        }

        public Vector3 LoadLevel1()
        {
            for (int i = 0; i < _map1.GetLength(0); i++)
            {
                for (int j = 0; j < _map1.GetLength(1); j++)
                {
                    Vector3 pos = new Vector3(i * 3.5f - 16.5f, 0f, j * 3.5f + 0);

                    if (_map1[i, j] == 1)
                    {
                        Cube cube = new Cube();
                        cube.Initialize(_device, "cube.txt", "stone.bmp", new Vector3(1.75f, 1.75f, 1.75f));
                        cube.SetPosition(pos);
                        _gameObjects.Add(cube);
                    }
                    else if (_map1[i, j] == 2)
                    {
                        DestroyBonus bonus = new DestroyBonus(pos, 0.5f);
                        bonus.Initialize(_device, "cube.txt", "texture_grass.jpg", new Vector3(.5f, .5f, .5f));
                        _gameObjects.Add(bonus);
                    }
                    else if (_map1[i, j] == 3)
                    {
                        UnscaleBonus bonus = new UnscaleBonus(pos, 0.5f, .75f);
                        bonus.Initialize(_device, "cube.txt", "seafloor.bmp", new Vector3(.5f, .5f, .5f));
                        _gameObjects.Add(bonus);
                    }
                    else if (_map1[i, j] == 4)
                    {
                        Finish finish = new Finish(pos, 0.5f);
                        finish.Initialize(_device, "cube.txt", "bump.bmp", new Vector3(.5f, .5f, .5f));
                        _gameObjects.Add(finish);
                    }
                    else if (_map1[i, j] == 5)
                        _spherePos = pos;
                }
            }
            return _spherePos;
        }

        public void EndLevel()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i] is Sphere || _gameObjects[i] is Ground)
                    continue;
                else
                {
                    _gameObjects.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
