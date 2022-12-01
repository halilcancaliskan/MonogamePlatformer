using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoGamePlatformer; 

namespace MonoGamePlatformer
{
    class Map
    {
        private List<CollisionTiles> _collisionTiles = new List<CollisionTiles>();

        public List<CollisionTiles> CollisionTiles { get { return _collisionTiles; } }


        private int _width, _height;
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }

        //CONSTRUCTOR 
        public Map()
        {
            
        }

        //METHODS
        public void Generate(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0)
                    {
                        CollisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));

                        _width = (x + 1) * size;

                        _height = (y + 1) * size;
                    }
                }
            }
        }

        //UPDATE & DRAW
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionTiles tile in CollisionTiles)
                tile.Draw(spriteBatch);
        }
    }
}

