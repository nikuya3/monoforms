// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Manager.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// <summary>
//   Manages all components of XnaForms.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MonoForms.Core
{
    using System;
    using System.ComponentModel;

    using Input;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Manages all components of XnaForms.
    /// </summary>
    public static class Manager
    {
        /// <summary>
        /// The <see cref="Microsoft.Xna.Framework.Graphics.GraphicsDevice"/> used by the components of XnaForms.
        /// </summary>
        private static GraphicsDevice graphicsDevice;

        /// <summary>
        /// The <see cref="ComponentSpriteBatch"/> used to draw the components of XnaForms.
        /// </summary>
        private static ComponentSpriteBatch spriteBatch;

        /// <summary>
        /// Gets or sets the <see cref="Microsoft.Xna.Framework.Graphics.GraphicsDevice"/> used by the components of XnaForms.
        /// </summary>
        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                return Manager.graphicsDevice;
            }

            set
            {
                Manager.graphicsDevice = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Manager"/> class has already been initialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static bool IsInitialized { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="ComponentSpriteBatch"/> used to draw the components of XnaForms.
        /// </summary>
        public static ComponentSpriteBatch SpriteBatch
        {
            get
            {
                return Manager.spriteBatch;
            }

            set
            {
                Manager.spriteBatch = value;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// A value indicating whether the dispose or the finalize process should be started.
        /// </param>
        public static void Dispose(bool disposing)
        {
            if (disposing)
            {
                FormsKeyboard.Stop();
            }
        }

        /// <summary>
        /// Initializes the <see cref="Manager"/> class with the given values.
        /// </summary>
        /// <param name="device">
        /// A <see cref="GraphicsDevice"/> used to create default objects.
        /// </param>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> used as default <see cref="SpriteFont"/>.
        /// </param>
        public static void Initialize(GraphicsDevice device, SpriteFont font)
        {
            Manager.graphicsDevice = device;
            Manager.spriteBatch = new ComponentSpriteBatch(device);
            Texture2D texture = new Texture2D(device, 1, 1);
            texture.SetData(new[] { Color.White });
            FormsKeyboard.Initialize();
            DefaultStyles.Initialize(device, font);
            Manager.IsInitialized = true;
        }

        /// <summary>
        /// Updates all components maintained by the <see cref="Manager"/> class.
        /// </summary>
        /// <param name="gameTime">
        /// An instance of <see cref="GameTime"/> indicating the current time.
        /// </param>
        public static void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates all components maintained by the <see cref="Manager"/> class.
        /// </summary>
        /// <param name="gameTime">
        /// An instance of <see cref="GameTime"/> indicating the current time.
        /// </param>
        public static void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
