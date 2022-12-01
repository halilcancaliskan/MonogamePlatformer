using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGamePlatformer;

namespace MonoGamePlatformer
{
    public class TileMap
    {
        //FIELDS
        protected Texture2D _texture;

        private Rectangle _rectangle;
        public Rectangle Rectangle { get { return _rectangle; } protected set { _rectangle = value; } }

        private static ContentManager _content;
        public static ContentManager Content { protected get { return _content; } set { _content = value; } }

        // METHODS

        // UPDATE & DRAW
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, Color.White);
        }    

    }

    class CollisionTiles : TileMap
    {
        public CollisionTiles(int i, Rectangle newRectangle)
        {
            _texture = Content.Load<Texture2D>("Tile" + i);
            this.Rectangle = newRectangle;
        }
    }
}

