// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyChangedEventArgs.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Core
{
    using System;

    /// <summary>
    /// Provides data for an event associated with a property that was changed.
    /// </summary>
    /// <typeparam name="T">
    /// The <see cref="Type"/> of the changed property.
    /// </typeparam>
    public class PropertyChangedEventArgs<T> : System.ComponentModel.PropertyChangedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedEventArgs{T}"/> class.
        /// </summary>
        /// <param name="propertyName">
        /// An instance of <see cref="string"/> representing the name of the property which was changed.
        /// </param>
        /// <param name="oldValue">
        /// The old value of the property which was changed.
        /// </param>
        /// <param name="newValue">
        /// The new value of the property which was changed.
        /// </param>
        public PropertyChangedEventArgs(string propertyName, T oldValue, T newValue) : base(propertyName)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        /// <summary>
        /// Gets the old value of the property which was changed.
        /// </summary>
        public T OldValue { get; private set; }

        /// <summary>
        /// Gets the new value of the property which was changed.
        /// </summary>
        public T NewValue { get; private set; }
    }
}
