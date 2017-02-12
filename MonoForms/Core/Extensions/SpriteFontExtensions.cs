// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpriteFontExtensions.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Core.Extensions
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Contains extension methods for the <see cref="SpriteFont"/> class.
    /// </summary>
    public static class SpriteFontExtensions
    {
        /// <summary>
        /// Calculates the <see cref="Size"/> of the given <see cref="string"/> when drawn in this <see cref="SpriteFont"/>.
        /// </summary>
        /// <param name="font">
        /// This instance of <see cref="SpriteFont"/>.
        /// </param>
        /// <param name="text">
        /// A <see cref="string"/> to be measured.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Size"/> indicating the size of the given <see cref="string"/> when drawn in this <see cref="SpriteFont"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="font"/> or <paramref name="text"/> is a null reference.
        /// </exception>
        public static Size MeasureStringSize(this SpriteFont font, string text)
        {
            if (font == null)
            {
                throw new ArgumentNullException(nameof(font));
            }

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            Vector2 size = font.MeasureString(text);
            return new Size(size.X, size.Y);
        }

        /// <summary>
        /// Calculates the <see cref="Size"/> of the given <see cref="string"/> when drawn in this <see cref="SpriteFont"/> scaled by the given value.
        /// </summary>
        /// <param name="font">
        /// This instance of <see cref="SpriteFont"/>.
        /// </param>
        /// <param name="text">
        /// A <see cref="string"/> to be measured.
        /// </param>
        /// <param name="scale">
        /// The drawing scale to be considered.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Size"/> indicating the size of the given <see cref="string"/> when drawn in this <see cref="SpriteFont"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="font"/> or <paramref name="text"/> is a null reference.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="scale"/> must be greater than or equals 0.
        /// </exception>
        public static Size MeasureStringSize(this SpriteFont font, string text, float scale)
        {
            if (font == null)
            {
                throw new ArgumentNullException(nameof(font));
            }

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (scale < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(scale), "scale must be greater than or equals 0.");
            }

            Vector2 size = font.MeasureString(text);
            return new Size(size.X * scale, size.Y * scale);
        }
    }
}
