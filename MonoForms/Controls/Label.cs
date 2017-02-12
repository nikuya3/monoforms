// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Label.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Controls
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    using MonoForms.Core;

    using MonoForms.Core.Extensions;
    using MonoForms.Shapes;

    /// <summary>
    /// Represents the standard label.
    /// </summary>
    public class Label : VisualControl
    {
        private const string DefaultText = "Label";
        private Text text;
        private string textContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        public Label()
        {
            this.Size = Size.Empty;
            this.AutoResize = true;
            this.textContent = Label.DefaultText;
            this.InitializeMembers();
        }

        /// <summary>
        /// Gets or sets a value indicating whether new text content should be resized to fit <see cref="VisualControl.Size"/>.
        /// </summary>
        public bool AutoResize { get; set; }

        /// <inheritdoc/>
        public override object Content
        {
            get
            {
                return base.Content;
            }

            set
            {
                base.Content = value;
                this.textContent = value.ToString();
                this.Refresh();
            }
        }

        /// <inheritdoc/>
        protected override Microsoft.Xna.Framework.Rectangle WholeRectangle { get; set; }

        /// <inheritdoc/>
        public override void Initialize()
        {
            this.InitializeMembers();
        }

        /// <inheritdoc/>
        protected override void InitializeShapes()
        {
            this.Shapes.Clear();
            if (this.AutoResize)
            {
                this.AutoResize = false;
                this.Size = this.Style.Font.MeasureStringSize(this.textContent, this.Style.TextScale);
                this.AutoResize = true;
            }

            this.text = new Text(this.textContent, this.Style.Font, this.Style.ForeColor, SpriteEffects.None, this.WholeRectangle);
            this.Shapes.Add(this.text);
        }

        private void InitializeMembers()
        {
            base.Initialize();
            this.Update(new GameTime());
        }
    }
}
