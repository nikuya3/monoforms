//-----------------------------------------------------------------------
// <copyright file="BatchState.cs" company="">
//     Copyright (c) 2014 .
// </copyright>
//-----------------------------------------------------------------------

namespace MonoForms
{
    /// <summary>
    /// Indicates the state of a <see cref="ComponentSpriteBatch"/>.
    /// </summary>
    public enum BatchState
    {
        /// <summary>
        /// The <see cref="ComponentSpriteBatch"/> was ended but not yet started again.
        /// </summary>
        Ended,

        /// <summary>
        /// The <see cref="ComponentSpriteBatch"/> was initialized but never started nor ended.
        /// </summary>
        Initialized,

        /// <summary>
        /// The <see cref="ComponentSpriteBatch"/> was started but not yet ended.
        /// </summary>
        Started,
    }
}
