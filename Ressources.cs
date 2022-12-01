using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGamePlatformer;

namespace MonoGamePlatformer
{
    public class Ressources
    {
        // FIELDS
        public static Texture2D KennyStatic { get; set; }

        public static Texture2D KennyGauche { get; set; }

        public static Texture2D KennyDroite { get; set; }

        public static Texture2D KennyBase { get; set; }

        public static Texture2D Start { get; set; }

        public static Texture2D Quit { get; set; }

        public static Texture2D BackgroundMenu { get; set; }

        public static Texture2D BackgroundJeu { get; set; }

        public static SpriteFont Score { get; set; }

        // CONTENT LOAD
        public static void LoadContent(ContentManager content)
        {
            KennyStatic = content.Load<Texture2D>("KennyStatic");
            KennyGauche = content.Load<Texture2D>("KennyGauche");
            KennyDroite = content.Load<Texture2D>("KennyDroite");
            KennyBase = content.Load<Texture2D>("KennyBase");
            Start = content.Load<Texture2D>("Button");
            Quit = content.Load<Texture2D>("ButtonQuit");
            BackgroundMenu = content.Load<Texture2D>("BackgroundMenu");
            BackgroundJeu = content.Load<Texture2D>("BackgroundJeu");
            Score = content.Load<SpriteFont>("Score");
        }

    }
}

