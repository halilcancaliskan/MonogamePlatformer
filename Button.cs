using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGamePlatformer
{
    public class Button
    {
        //FIELDS
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _rectangle;

        private bool _down;
        public bool isClicked { get; set; }

        private Color Couleur = new Color(255, 255, 255, 255);

        public Vector2 Size { get; set; }

        // CONSTRUCTOR
        public Button(Texture2D newTexture, GraphicsDevice graphics)
        {
            _texture = newTexture;

            Size = new Vector2(graphics.Viewport.Width / 4, graphics.Viewport.Height / 15);
        } 

        // METHODS
        public void setPosition(Vector2 newPosition)
        {
            _position = newPosition;
        }

        // UPDATE & DRAW
        public void Update(MouseState mouse)
        {
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y, (int)Size.X, (int)Size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(_rectangle))
            {
                if (Couleur.A == 255)
                {
                    _down = false;
                }
                if (Couleur.A == 0)
                {
                    _down = true;
                }
                if (_down)
                {
                    Couleur.A += 3;
                }
                else
                {
                    Couleur.A -= 3;
                }
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                }
            }
            else if (Couleur.A < 255)
            {
                Couleur.A += 3;
                isClicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, Couleur);
        }
    }
}
