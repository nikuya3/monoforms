// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentMouse.cs" company="">
//   Copyright (c) 2015 .
// </copyright>
// <summary>
//   Improves the interoperability between MonoForms and a mouse input device.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Input
{
    using System;
    using System.Diagnostics;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    using MonoForms.Controls;

    /// <summary>
    ///     Improves the interoperability between MonoForms and a mouse input device.
    /// </summary>
    public static class ComponentMouse
    {
        /// <summary>
        ///     A <see cref="MouseButton" /> indicating the default button used to detect clicks.
        /// </summary>
        public const MouseButton DefaultClickButton = MouseButton.Left;

        /// <summary>
        ///     The time of the last <see cref="MouseState" /> retrieving process.
        /// </summary>
        private static GameTime lastRetrievingTime = new GameTime();

        /// <summary>
        ///     The current <see cref="MouseState" />.
        /// </summary>
        private static MouseState mouseState = Mouse.GetState();

        /// <summary>
        /// Initializes static members of the <see cref="ComponentMouse"/> class.
        /// </summary>
        static ComponentMouse()
        {
            ComponentMouse.IsUpdateEnabled = true;
        }

        /// <summary>
        ///     Gets or sets the <see cref="Control" /> which currently captured the <see cref="Mouse" />.
        /// </summary>
        /// <value>
        ///     The last <see cref="Control" /> which was attached to the <see cref="Mouse" /> or NULL, if there was no
        ///     <see cref="Control" /> attached to it.
        /// </value>
        public static Control Captured { get; set; }

        /// <summary>
        ///     Gets a value indicating whether a mouse button is pressed.
        /// </summary>
        public static bool IsMouseDown
            =>
                ComponentMouse.mouseState.LeftButton == ButtonState.Pressed
                || ComponentMouse.mouseState.RightButton == ButtonState.Pressed
                || ComponentMouse.mouseState.MiddleButton == ButtonState.Pressed
                || ComponentMouse.mouseState.XButton1 == ButtonState.Pressed
                || ComponentMouse.mouseState.XButton2 == ButtonState.Pressed;

        /// <summary>
        ///     Gets or sets a value indicating whether <see cref="ComponentMouse" /> is enabled to update its mouse input on every
        ///     update.
        /// </summary>
        public static bool IsUpdateEnabled { get; set; }

        /// <summary>
        ///     Gets the currently pressed <see cref="MouseButton" />.
        /// </summary>
        public static MouseButton PressedButton
        {
            get
            {
                if (ComponentMouse.mouseState.LeftButton == ButtonState.Pressed)
                {
                    return MouseButton.Left;
                }

                if (ComponentMouse.mouseState.RightButton == ButtonState.Pressed)
                {
                    return MouseButton.Right;
                }

                if (ComponentMouse.mouseState.MiddleButton == ButtonState.Pressed)
                {
                    return MouseButton.Middle;
                }

                if (ComponentMouse.mouseState.XButton1 == ButtonState.Pressed)
                {
                    return MouseButton.XButton1;
                }

                if (ComponentMouse.mouseState.XButton2 == ButtonState.Pressed)
                {
                    return MouseButton.XButton2;
                }

                return MouseButton.None;
            }
        }

        /// <summary>
        /// Gets the current <see cref="MouseState"/> of the <see cref="Mouse"/>.
        /// </summary>
        /// <param name="gameTime">
        /// The current <see cref="GameTime"/>.
        /// </param>
        /// <returns>
        /// The current <see cref="MouseState"/> of the <see cref="Mouse"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="gameTime"/> is a null reference.
        /// </exception>
        public static MouseState GetMouseState(GameTime gameTime)
        {
            if (gameTime == null)
            {
                throw new ArgumentNullException(nameof(gameTime));
            }

            if (ComponentMouse.IsUpdateEnabled)
            {
                ComponentMouse.mouseState = Mouse.GetState();
            }
            
            ComponentMouse.lastRetrievingTime = gameTime;
            return ComponentMouse.mouseState;
        }

        /// <summary>
        /// Sets the current <see cref="MouseState"/> to be used within MonoForms.
        ///     <see cref="ComponentMouse"/> automatically updates its mouse input, so use this method only if you have to apply
        ///     custom <see cref="MouseState"/>s.
        ///     As <see cref="ComponentMouse"/> should update its <see cref="MouseState"/> on every game update (for the components to function properly),
        ///     you have to set the <see cref="MouseState"/> on every update. To prevent <see cref="ComponentMouse"/> from updating its mouse input on every update
        /// automatically, set <see cref="IsUpdateEnabled"/> to false.
        /// </summary>
        /// <param name="mouseState">
        /// The new <see cref="MouseState"/>.
        /// </param>
        /// <param name="gameTime">
        /// The <see cref="GameTime"/> indicating the time of the mouse input retrieving.
        /// </param>
        public static void SetMouseState(MouseState mouseState, GameTime gameTime)
        {
            ComponentMouse.mouseState = mouseState;
            ComponentMouse.lastRetrievingTime = gameTime;
        }
    }
}