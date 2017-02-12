// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainGame.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WindowsTest
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoForms;
    using MonoForms.Core;
    using MonoForms.Core.Extensions;
    using WindowsTest.Forms;

    /// <summary>
    /// Runs the application.
    /// </summary>
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainGame"/> class.
        /// </summary>
        public MainGame()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;

            Globals.ContentManager = this.Content;
            Globals.GraphicsDeviceManager = this.graphics;
        }

        /// <summary>
        /// Initializes this instance of <see cref="MainGame"/>.
        /// </summary>
        protected override void Initialize()
        {
            Manager.Initialize(this.GraphicsDevice, this.Content.Load<SpriteFont>("DefaultFont"));
            MainForm mainForm = new MainForm();
            mainForm.Closed += (sender, e) => { this.Exit(); };
            this.Components.Add(mainForm);
            base.Initialize();
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Manager.Dispose(disposing);
        }

        /// <summary>
        /// Loads the content of this instance of <see cref="MainGame"/>.
        /// </summary>
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Unloads the content of this instance of <see cref="MainGame"/>.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Updates the contents of this instance of <see cref="MainGame"/>.
        /// </summary>
        /// <param name="gameTime">
        /// An instance of <see cref="GameTime"/> representing the current time.
        /// </param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the contents of this instance of <see cref="MainGame"/>.
        /// </summary>
        /// <param name="gameTime">
        /// An instance of <see cref="GameTime"/> representing the current time.
        /// </param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
