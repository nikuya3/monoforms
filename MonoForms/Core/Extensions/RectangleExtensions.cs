// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RectangleExtensions.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// <summary>
//   Contains extension methods for the <see cref="Microsoft.Xna.Framework.Rectangle" /> structure.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MonoForms.Core.Extensions
{
    using System;

    using Microsoft.Xna.Framework;

    /// <summary>
    /// Contains extension methods for the <see cref="Rectangle"/> structure.
    /// </summary>
    public static class RectangleExtensions
    {
        /// <summary>
        /// Returns a new instance of <see cref="Rectangle"/> from a given <see cref="Point"/> and a given <see cref="Size"/>.
        /// </summary>
        /// <param name="rectangle">
        /// This instance of <see cref="Rectangle"/>.
        /// </param>
        /// <param name="point">
        /// An instance of <see cref="Point"/> indicating the location of the new instance of <see cref="Rectangle"/>.
        /// </param>
        /// <param name="size">
        /// An instance of <see cref="Size"/> indicating the size of the new instance of <see cref="Rectangle"/>.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="Rectangle"/> from the given <see cref="Point"/> and <see cref="Size"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="size"/> is <see cref="Size.Empty"/>.
        /// </exception>
        public static Rectangle FromPointAndSize(this Rectangle rectangle, Point point, Size size)
        {
            if (size.IsEmpty)
            {
                throw new ArgumentNullException(nameof(size));
            }

            return new Rectangle(point.X, point.Y, (int)size.Width, (int)size.Height);
        }

        /// <summary>
        /// Gets the <see cref="Size"/> of this <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rectangle">
        /// This instance of <see cref="Rectangle"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Size"/> indicating the size of this <see cref="Rectangle"/>.
        /// </returns>
        public static Size GetSize(this Rectangle rectangle)
        {
            return new Size(rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// Returns this instance of <see cref="Rectangle"/> with the specified <see cref="Size"/> set.
        /// </summary>
        /// <param name="rectangle">
        /// This instance of <see cref="Rectangle"/>.
        /// </param>
        /// <param name="size">
        /// An instance of <see cref="Size"/> indicating the new size of this instance of <see cref="Rectangle"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Rectangle"/> with the specified <see cref="Size"/> set.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="size"/> is <see cref="Size.Empty"/>.
        /// </exception>
        public static Rectangle SetSize(this Rectangle rectangle, Size size)
        {
            if (size.IsEmpty)
            {
                throw new ArgumentNullException(nameof(size));
            }

            return rectangle.FromPointAndSize(rectangle.Location, size);
        }
    }
}
