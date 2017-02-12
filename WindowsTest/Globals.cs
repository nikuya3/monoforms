// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Globals.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WindowsTest
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    /// <summary>
    /// Holds the main instances of values used throughout this application.
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Gets or sets the main instance of <see cref="GraphicsDeviceManager"/>.
        /// </summary>
        public static GraphicsDeviceManager GraphicsDeviceManager { get; set; }

        /// <summary>
        /// Gets or sets the main instance of <see cref="ContentManager"/>.
        /// </summary>
        public static ContentManager ContentManager { get; set; }
    }
}
