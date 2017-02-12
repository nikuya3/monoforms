//-----------------------------------------------------------------------
// <copyright file="MouseEventArgs.cs" company="">
//     Copyright (c) 2014 .
// </copyright>
//-----------------------------------------------------------------------

namespace MonoForms
{
    using System;
    using Microsoft.Xna.Framework;

    using MonoForms.Input;

    /// <summary>
    /// Supports interaction with mouse events.
    /// </summary>
    public class MouseEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventArgs"/> class.
        /// </summary>
        /// <param name="button">
        /// A <see cref="MouseButton"/> indicating the mouse button associated with this <see cref="MouseEventArgs"/>.
        /// </param>
        /// <param name="clicks">
        /// A value indicating the count of times the <paramref name="button"/> was clicked.
        /// </param>
        /// <param name="scrollCount">
        /// A value indicating the count of detents the mouse wheel has rotated.
        /// </param>
        /// <param name="scrollWheelValue">
        /// A value indicating the number of detents the mouse wheel has rotated.
        /// </param>
        /// <param name="location">
        /// A <see cref="Point"/> indicating the location of the mouse.
        /// </param>
        public MouseEventArgs(MouseButton button, int clicks, int scrollCount, int scrollWheelValue, Point location)
        {
            this.Button = button;
            this.Clicks = clicks;
            this.ScrollCount = scrollCount;
            this.ScrollWheelValue = scrollWheelValue;
            this.Location = location;
        }

        /// <summary>
        /// Gets the <see cref="MouseButton"/> associated with the mouse event.
        /// </summary>
        public MouseButton Button { get; private set; }

        /// <summary>
        /// Gets the number of times the <see cref="Button"/> was pressed and released.
        /// </summary>
        public int Clicks { get; private set; }

        /// <summary>
        /// Gets a <see cref="Point"/> indicating the location of the mouse during the generating mouse event.
        /// </summary>
        public Point Location { get; private set; }

        /// <summary>
        /// Gets the count of detents the mouse wheel has rotated.
        /// </summary>
        public int ScrollCount { get; private set; }

        /// <summary>
        /// Gets or sets the number of detents the mouse wheel has rotated. A detent is one notch of the mouse wheel.
        /// </summary>
        public int ScrollWheelValue { get; private set; }
    }
}
