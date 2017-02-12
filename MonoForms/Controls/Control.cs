// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Control.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Controls
{
    using System;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Represents a control of an user interface.
    /// </summary>
    public class Control : IGameComponent, IUpdateable
    {
        private bool enabled;
        private Control parent;
        private int updateOrder;

        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        public Control()
        {
            this.enabled = true;
            this.updateOrder = 0;
            this.parent = null;
        }

        /// <summary>
        /// Occurs when <see cref="Enabled"/> changed.
        /// </summary>
        public event EventHandler<EventArgs> EnabledChanged;

        /// <summary>
        /// Occurs when <see cref="Parent"/> changed.
        /// </summary>
        public event EventHandler<EventArgs> ParentChanged;

        /// <summary>
        /// Occurs when <see cref="UpdateOrder"/> changed.
        /// </summary>
        public event EventHandler<EventArgs> UpdateOrderChanged;

        /// <summary>
        /// Gets a value indicating whether this <see cref="IUpdateable"/> is enabled to update.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }

            set
            {
                this.enabled = value;
                this.EnabledChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets an instance of <see cref="Control"/> considered as parent of this instance of <see cref="Control"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Control Parent
        {
            get
            {
                return this.parent;
            }

            set
            {
                this.parent = value;
                if (this.ParentChanged != null)
                {
                    this.ParentChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance of <see cref="Control"/> is already initialized.
        /// </summary>
        public bool Initialized { get; protected set; }

       /// <summary>
       /// Gets the update priority of this <see cref="IUpdateable"/> relative to others.
       /// </summary>
        public int UpdateOrder
        {
            get
            {
                return this.updateOrder;
            }

            set
            {
                this.updateOrder = value;
                this.UpdateOrderChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Initializes this <see cref="Control"/>.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Updates this <see cref="IUpdateable"/>.
        /// </summary>
        /// <param name="gameTime">
        /// A <see cref="GameTime"/> representing the current timing state.
        /// </param>
        public virtual void Update(GameTime gameTime)
        {
            if (DefaultStyles.IsInitialized)
            {
            }
        }
    }
}
