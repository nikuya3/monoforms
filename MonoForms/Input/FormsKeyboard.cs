//-----------------------------------------------------------------------
// <copyright file="FormsKeyboard.cs" company="">
//    Copyright © 2014 .
// </copyright>
//-----------------------------------------------------------------------

namespace MonoForms.Input
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Microsoft.Xna.Framework.Input;

    /// <summary>
    ///     Provides an interface to the underlying XNA <see cref="Keyboard" />.
    /// </summary>
    public static class FormsKeyboard
    {
        /// <summary>
        ///     The lockable <see cref="Keys" /> which were pressed and not yet repressed.
        /// </summary>
        private static readonly List<Keys> LockedKeys = new List<Keys>();

        /// <summary>
        ///     The current <see cref="KeyboardState" />.
        /// </summary>
        private static KeyboardState currentState;

        /// <summary>
        ///     The previous <see cref="KeyboardState" />.
        /// </summary>
        private static KeyboardState previousState;

        /// <summary>
        ///     The <see cref="Timer" /> handling the proper call times for the input retrieving.
        /// </summary>
        private static Timer timer;

        /// <summary>
        ///     Gets or sets the <see cref="IInputReceivable" /> which receives the keyboard input. Set to null to pause the input
        ///     retrieving process.
        /// </summary>
        public static IInputReceivable Receiver { get; set; }

        /// <summary>
        ///     Gets the current <see cref="KeyboardState" />.
        /// </summary>
        public static KeyboardState State
        {
            get
            {
                FormsKeyboard.previousState = FormsKeyboard.currentState;
                FormsKeyboard.currentState = Keyboard.GetState();
                return FormsKeyboard.currentState;
            }
        }

        /// <summary>
        ///     Initializes the members of the <see cref="FormsKeyboard" /> class. Starts the input retrieving process.
        /// </summary>
        public static void Initialize()
        {
            FormsKeyboard.timer = new Timer(state => FormsKeyboard.UpdateInput(), null, 0, 1);
        }

        /// <summary>
        ///     Stops the input retrieving process permanently. If you just want to pause it, set <see cref="Receiver" /> to null.
        /// </summary>
        public static void Stop()
        {
            FormsKeyboard.timer.Dispose();
        }

        /// <summary>
        ///     Retrieves new user input and sends it to <see cref="Receiver" />.
        /// </summary>
        private static void UpdateInput()
        {
            if (FormsKeyboard.Receiver == null)
            {
                return;
            }

            FormsKeyboard.previousState = FormsKeyboard.currentState;
            FormsKeyboard.currentState = Keyboard.GetState();
            var pressedKeys = FormsKeyboard.currentState.GetPressedKeys();
            foreach (var key in pressedKeys.Where(key => FormsKeyboard.previousState[key] == KeyState.Up))
            {
                switch (key)
                {
                    case Keys.Back:
                        FormsKeyboard.Receiver.ReceiveSpecialInput(Keys.Back);
                        break;
                    case Keys.Space:
                        FormsKeyboard.Receiver.ReceiveTextInput(" ");
                        break;
                    case Keys.Enter:
                        FormsKeyboard.Receiver.ReceiveSpecialInput(Keys.Enter);
                        break;
                    case Keys.CapsLock:
                        if (FormsKeyboard.LockedKeys.Contains(Keys.CapsLock))
                        {
                            FormsKeyboard.LockedKeys.Remove(Keys.CapsLock);
                        }
                        else
                        {
                            FormsKeyboard.LockedKeys.Add(Keys.CapsLock);
                        }

                        break;
                    default:
                        var keyString = key.ToString();
                        var capsLocked = FormsKeyboard.LockedKeys.Contains(Keys.CapsLock);
                        var rightShiftDown = FormsKeyboard.currentState[Keys.RightShift] == KeyState.Down;
                        var leftShiftDown = FormsKeyboard.currentState[Keys.LeftShift] == KeyState.Down;
                        var isUpperCase = (capsLocked && (!rightShiftDown && !leftShiftDown))
                                           || (!capsLocked && (rightShiftDown || leftShiftDown));

                        if (keyString.Length == 1)
                        {
                            FormsKeyboard.Receiver.ReceiveTextInput(
                                isUpperCase ? keyString.ToUpper() : keyString.ToLower());
                        }

                        break;
                }
            }
        }
    }
}