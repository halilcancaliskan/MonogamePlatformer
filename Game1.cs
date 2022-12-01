using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGamePlatformer;
using Microsoft.Xna.Framework.Media;
using MonoGamePlatformer.Core;

namespace MonoGamePlatformer
{
    public class Game1 : Game
    {
        //FIELDS
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera _camera;


        public static float ScreenWidth { get; set; }
        public static float ScreenHeight { get; set; }

        public Song Song { get; set; }

        public Player Player { get; set; }

        public int Score { get; set; }

        Map Map { get; set; }

        public enum GameState
        {
            MainMenu,
            Playing,
            Quit,
        }

        GameState CurrentGameState = GameState.MainMenu;

        public Button ButtonPlay { get; set; }
        public Button ButtonQuit { get; set; }

        // CONSTRUCTOR
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
        }

        // METHODS
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Player = new Player(new Vector2(100, 100));

            Map = new Map();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Pour récupérer les content dans Ressources.
            Ressources.LoadContent(Content);

            // Musique South Park
            Song = Content.Load<Song>("intro");
            MediaPlayer.Play(Song);
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            // Content de la TileMap => texture des blocs
            TileMap.Content = Content;

            _camera = new Camera();

            // Content du bouton
            ButtonPlay = new Button(Ressources.Start, _graphics.GraphicsDevice);
            ButtonPlay.setPosition(new Vector2(300, 150));
            ButtonQuit = new Button(Ressources.Quit, _graphics.GraphicsDevice);
            ButtonQuit.setPosition(new Vector2(300, 200));

            Map.Generate(new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
                {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0},
                {0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0},
                {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},

            }, 50);

            // TODO: use this.Content to load your game content here
        }
        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MediaPlayer.Volume -= -3f;
            MediaPlayer.Play(Song);
        }

        // UPDATE & DRAW
        protected override void Update(GameTime gameTime)
        {

            MouseState mouse = Mouse.GetState();
            Score = Player.getScore();

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (ButtonPlay.isClicked == true)
                    {
                        CurrentGameState = GameState.Playing;
                    }
                    if (ButtonQuit.isClicked == true)
                    {
                        this.Exit();
                    }
                    ButtonPlay.Update(mouse);
                    ButtonQuit.Update(mouse);
                    break;
                case GameState.Playing:

                    Player.Update(Keyboard.GetState(), gameTime);

                    // Update de la map et des collisions
                    foreach (CollisionTiles tile in Map.CollisionTiles)
                    {
                        Player.Collision(tile.Rectangle, Map.Width, Map.Height);
                    }

                    break;
                case GameState.Quit:
                    //Code ici
                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    _spriteBatch.Draw(Ressources.BackgroundMenu, new Vector2(0, 0), Color.White);

                    ButtonPlay.Draw(_spriteBatch);
                    ButtonQuit.Draw(_spriteBatch);
                    break;
                case GameState.Playing:
                    _spriteBatch.Draw(Ressources.BackgroundJeu, new Vector2(0, 0), Color.White);
                    _spriteBatch.DrawString(Ressources.Score, "Score : " + Score.ToString(), new Vector2(0, 0), Color.White);
                        
                    Player.Draw(_spriteBatch);
                    Map.Draw(_spriteBatch);

                    break;
            }



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

