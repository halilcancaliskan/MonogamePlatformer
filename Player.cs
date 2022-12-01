using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGamePlatformer;

namespace MonoGamePlatformer
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public class Player
    {
        // FIELDS

        private Direction Direction;

        private Vector2 _position;

        private Vector2 _vitesse;

        private Rectangle _rectangle;

        public int Score { get; set; }


        private int _frameLine;

        private int _frameColumn;

        private bool _animation;

        private bool _hasJumped = true;

        private int _timer;

        private int _animationSpeed = 5;



        // CONSTRUCTOR
        public Player(Vector2 position)
        {
            _position = position;
            _frameLine = 0;
            _frameColumn = 1;
            _animation = true;
            _timer = 0;
            Score = 0;
        }

        // METHODS
        public int getScore()
        {
            return Score;
        }

        public void Animate(int start, int end)
        {
            _timer++;
            if (_frameColumn > end || _frameColumn < start)
            {
                _frameColumn = start;
            }
            else
            {
                if (_timer == _animationSpeed)
                {
                    _timer = 0;
                    if (_animation)
                    {
                        _frameColumn++;

                        if (_frameColumn > end)
                        {
                            _frameColumn--;
                            _animation = false;
                        }
                    }
                    else
                    {
                        _frameColumn--;

                        if (_frameColumn < start)
                        {
                            _frameColumn++;
                            _animation = true;
                        }
                    }
                }
            }
        }

        public void Collision(Rectangle newRectangle, int xDecal, int yDecal)
        {
            if (_rectangle.TouchTopOf(newRectangle))
            {
                _rectangle.Y = newRectangle.Y - _rectangle.Height - 10;
                _hasJumped = false;    
            }

            if (_rectangle.TouchLeftOf(newRectangle))
            {
                _position.X = newRectangle.X - _rectangle.Width - 2;
            }
            if (_rectangle.TouchRightOf(newRectangle))
            {
                _position.X = newRectangle.X + newRectangle.Width + 2;
            }
            if (_rectangle.TouchBottomOf(newRectangle))
            {
                _vitesse.Y = 1f;
            }
            

            if (_position.X < 0) //Bloque la sortie a gauche de l'écran
            {
                _position.X = 0;
            }
            if (_position.X > xDecal - _rectangle.Width)
            {
                _position.X = xDecal - _rectangle.Width;
            }
            if (_position.Y < 0) // Bloque la sortie en haut de l'écran
            {
                _vitesse.Y = 1f;
            }
            if (_position.Y > yDecal - _rectangle.Height)
            {
                _position.Y = yDecal - _rectangle.Height;
            }
        }

        // UPDATE & DRAW
        public void Update(KeyboardState keyboard, GameTime gameTime)
        {
            _position += _vitesse;

            _rectangle = new Rectangle((int)_position.X, (int)_position.Y, 26, 26);

            if (_hasJumped) // == true / A sauté  
            {
                float i = 1f; 
                _vitesse.Y += 0.25f * i;
            }

            if (!_hasJumped) // == false / N'a pas sauté
            {
                _vitesse.Y = 0f;
            }
            

            //if (_position.Y + Ressources.KennyBase.Height >= 500) // Si la position en Y + la hauteur du personnage est > ou = a 500, alors on ne tombe plus
            //{
            //    hasJump = false;
            //}


            if (keyboard.IsKeyDown(Keys.Z))
            {
                if (!_hasJumped) // S'il appuie sur Z est qu'il n'était pas dans les airs, alors il saute
                {
                    Score += 1;
                    _position.Y -= 8f;
                    _vitesse.Y -= 5f;
                    _hasJumped = true;
                }
                    _frameColumn = 1;
            }


            if (keyboard.IsKeyDown(Keys.Q))
            {
                Score += 1;
                _vitesse.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3; 
                Direction = Direction.Left;
                Animate(0, 1);
            }
            else if (keyboard.IsKeyDown(Keys.D))
            {
                Score += 1;
                _vitesse.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                Direction = Direction.Right;
                Animate(2, 3);
            }
            else
            {
                _vitesse.X = 0f;
                _frameLine = 0;
                _timer = 0;
            }     

            switch (Direction)
            {
                case Direction.Up:
                    _frameLine = 0;
                    break;
                case Direction.Left:
                    _frameLine = 2;
                    break;
                case Direction.Right:
                    _frameLine = 2;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.KennyBase,
                             _position,
                             new Rectangle(_frameColumn * 28,
                                           _frameLine * 28,
                                           28,
                                           28),
                             Color.White);

              
        }
    }
}

