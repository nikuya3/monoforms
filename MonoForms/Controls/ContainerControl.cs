// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainerControl.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// <summary>
//   Represents a <see cref="VisualControl" /> which holds other <see cref="Controls" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Controls
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    using Microsoft.Xna.Framework;

    using MonoForms.Core;

    /// <summary>
    /// Represents a <see cref="VisualControl"/> which holds other <see cref="Controls"/>.
    /// </summary>
    public abstract class ContainerControl : VisualControl
    {
        /// <summary>
        /// The hide container.
        /// </summary>
        private bool hideContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerControl"/> class.
        /// </summary>
        protected ContainerControl()
        {
            this.Controls = new TemporaryTestControlCollection(this);
            this.PrivateControls = new Collection<Control>();
            this.InitializeMembers();
        }

        /// <summary>
        /// Gets or sets the collection of controls contained within this <see cref="Control"/>.
        /// </summary>
        public TemporaryTestControlCollection Controls { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the container should be hidden. The <see cref="Controls"/> within the container will still be shown,
        /// while setting <see cref="VisualControl.Visible"/> will result in the <see cref="Controls"/> being hidden as well.
        /// </summary>
        public bool HideContainer
        {
            get
            {
                return this.hideContainer;
            }

            set
            {
                this.hideContainer = value;
                if (!value)
                {
                    return;
                }

                foreach (var visualControl in this.PrivateControls.OfType<VisualControl>().Select(control => control))
                {
                    visualControl.Visible = false;
                }
            }
        }

        /// <inheritdoc />
        public override bool Visible
        {
            get
            {
                return base.Visible;
            }

            set
            {
                base.Visible = value;
                foreach (var visualControl in this.Controls.OfType<VisualControl>().Select(control => control))
                {
                    visualControl.Visible = value;
                }

                foreach (var visualControl in this.PrivateControls.OfType<VisualControl>().Select(control => control))
                {
                    visualControl.Visible = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the collection of <see cref="Control"/>s contained within this <see cref="Control"/>, which are not to be changed.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected Collection<Control> PrivateControls { get; set; }

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime)
        {
            if (!this.HideContainer)
            {
                base.Draw(gameTime);
            }
            
            Manager.SpriteBatch.Begin(true);
            foreach (var visualControl in this.PrivateControls.OfType<VisualControl>().Select(control => control))
            {
                visualControl.Draw(gameTime);
            }

            foreach (var visualControl in this.Controls.OfType<VisualControl>().Select(control => control))
            {
                visualControl.Draw(gameTime);
            }

            Manager.SpriteBatch.End(true);
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            this.InitializeMembers();
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.HideContainer)
            {
                foreach (var visualControl in this.PrivateControls.OfType<VisualControl>().Select(control => control).TakeWhile(control => control.Visible))
                {
                    visualControl.Visible = false;
                }
            }

            if (DefaultStyles.IsInitialized)
            {
                foreach (var control in this.PrivateControls)
                {
                    control.Update(gameTime);
                }

                foreach (var control in this.Controls)
                {
                    control.Update(gameTime);
                }
            }
        }

        /// <summary>
        /// The initialize members.
        /// </summary>
        private void InitializeMembers()
        {
            base.Initialize();
        }
    }
}
