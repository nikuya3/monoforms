// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckBox.cs" company="">
//   Copyright (c) 2014 .
// </copyright>
// <summary>
//   Represents a control with a checkable box.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MonoForms.Controls
{
    using System.Diagnostics;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using MonoForms.Core;

    using MonoForms.Core.Extensions;
    using MonoForms.Shapes;

    /// <summary>
    ///     Represents a control with a checkable box.
    /// </summary>
    public class CheckBox : VisualControl
    {
        /// <summary>
        /// The default width of the checkable box.
        /// </summary>
        private const int DefaultBoxWidth = 30;

        /// <summary>
        /// The default text content of the combo box.
        /// </summary>
        private const string DefaultText = "CheckBox";

        /// <summary>
        /// A value indicating whether the checkable box should be filled.
        /// </summary>
        private bool fill;

        /// <summary>
        /// A value indicating whether the checkable box is checked.
        /// </summary>
        private bool isChecked;

        /// <summary>
        /// The shape holding the text content.
        /// </summary>
        private Text text;

        /// <summary>
        /// The text content.
        /// </summary>
        private string textContent = CheckBox.DefaultText;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CheckBox" /> class.
        /// </summary>
        public CheckBox()
        {
            this.isChecked = false;
            this.InitializeMembers();
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance of <see cref="CheckBox" /> is currently checked.
        /// </summary>
        public bool Checked
        {
            get
            {
                return this.isChecked;
            }

            set
            {
                this.isChecked = value;
                this.fill = value;
                this.Style = this.DefaultStyle;
            }
        }

        /// <inheritdoc />
        public override object Content
        {
            get
            {
                return base.Content;
            }

            set
            {
                base.Content = value;
                if (value != null)
                {
                    this.textContent = value.ToString();
                    this.Refresh();
                }
            }
        }

        /// <inheritdoc />
        public override Style Style
        {
            get
            {
                return base.Style;
            }

            set
            {
                if (value.StyleType == StyleType.Active)
                {
                    this.fill = true;
                }

                base.Style = value;
            }
        }

        /// <inheritdoc />
        protected override Microsoft.Xna.Framework.Rectangle WholeRectangle { get; set; }

        /// <inheritdoc />
        public override void Initialize()
        {
            this.InitializeMembers();
        }

        /// <inheritdoc />
        protected override void InitializeShapes()
        {
            this.Shapes.Clear();
            var textSize = this.Style.Font.MeasureStringSize(this.textContent, this.Style.TextScale);
            var boxRectangle = new Microsoft.Xna.Framework.Rectangle().FromPointAndSize(
                this.ScreenLocation, 
                new Size(CheckBox.DefaultBoxWidth, textSize.Height));
            var rectangle = new MonoForms.Shapes.Rectangle(boxRectangle, this.Style);
            if (this.fill)
            {
                rectangle = new MonoForms.Shapes.Rectangle(boxRectangle, this.Style);
                if (this.Style.StyleType == StyleType.Active && !this.Checked)
                {
                    this.fill = false;
                }
            }
            else
            {
                rectangle = new MonoForms.Shapes.Rectangle(
                    boxRectangle, 
                    this.Style.BorderThickness, 
                    this.Style.BorderColor, 
                    Color.Transparent, 
                    this.Style.Texture);
            }

            var textRectangle = boxRectangle.FromPointAndSize(new Point(boxRectangle.Right, boxRectangle.Top), textSize);
            this.text = new Text(
                this.textContent, 
                this.Style.Font, 
                this.Style.ForeColor, 
                SpriteEffects.None, 
                textRectangle);
            this.Shapes.Add(rectangle);
            this.Shapes.Add(this.text);
        }

        /// <summary>
        /// Initializes the instance members of the <see cref="CheckBox"/> class.
        /// </summary>
        private void InitializeMembers()
        {
            base.Initialize();
            if (DefaultStyles.IsInitialized)
            {
                this.DefaultStyle.BackColor = Color.Black;
                this.ActiveStyle.BackColor = this.DefaultStyle.BackColor * 0.5f;
                this.MouseClick += (sender, e) =>
                    {
                        this.fill = true;
                        this.Style = this.ActionStyle;
                    };
                this.MouseUp += (sender, e) => { this.Checked = !this.Checked; };
                this.Update(new GameTime());
            }
        }
    }
}