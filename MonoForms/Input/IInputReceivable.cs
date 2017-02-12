// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputReceivable.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Input
{
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Implements methods to retrieve input.
    /// </summary>
    public interface IInputReceivable
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IInputReceivable"/> is focused.
        /// </summary>
        bool Focused { get; set; }

        /// <summary>
        /// Receives the last entered <see cref="string"/>.
        /// </summary>
        /// <param name="text">
        /// The last entered <see cref="string"/>.
        /// </param>
        void ReceiveTextInput(string text);

        /// <summary>
        /// Receives the last command.
        /// </summary>
        /// <param name="command">
        /// The last command as <see cref="char"/>.
        /// </param>
        void ReceiveCommandInput(char command);

        /// <summary>
        /// Receives the last entered key.
        /// </summary>
        /// <param name="key">
        /// The last entered key.
        /// </param>
        void ReceiveSpecialInput(Keys key);
    }
}
