// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IShape.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Shapes
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Defines implementations of a geometric form.
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// Gets or sets a <see cref="Microsoft.Xna.Framework.Rectangle"/> indicating the bounds of this <see cref="IShape"/> relative to the <see cref="GameWindow"/> containing it.
        /// </summary>
        Microsoft.Xna.Framework.Rectangle Bounds { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="Point"/> indicating the location of this <see cref="IShape"/> relative to the <see cref="GameWindow"/> containing it.
        /// </summary>
        Point Location { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Style"/> used to draw this <see cref="IShape"/>.
        /// </summary>
        Style Style { get; set; }

        /// <summary>
        /// Draws this <see cref="IShape"/> on the given <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch">
        /// A <see cref="SpriteBatch"/> used to draw this <see cref="IShape"/>.
        /// </param>
        void Draw(SpriteBatch spriteBatch);
    }
}
