// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressBar.cs" company="">
//   Copyright (c) 2015 .
// </copyright>
// <summary>
//   Represents a bar where the value is indicated by a second bar.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Controls
{
    using System;

    using Microsoft.Xna.Framework;

    using Core;

    using MonoForms.Shapes;

    /// <summary>
    /// Represents a bar where the value is indicated by a second bar.
    /// </summary>
    public class ProgressBar : RangeBase
    {
        private const int DefaultHeight = 30;

        private const int DefaultWidth = 120;

        private const int DefaultMaximum = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class.
        /// </summary>
        public ProgressBar()
        {
            this.InitializeMembers();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the progress bar is in indetermined mode. The indetermined mode is not yet implemented, so this property will always return false.
        /// </summary>
        public bool IsIndetermined
        {
            get
            {
                return false;
            }

            set
            {
                throw new NotImplementedException("Indetermined mode is not yet implemented.");
            }
        }

        ////public Orientation Orientation { get; set; }

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
            double unit = this.Size.Width / (this.Maximum - this.Minimum);
            this.Shapes.Add(new MonoForms.Shapes.Rectangle(this.WholeRectangle, this.Style.BorderThickness, this.Style.BorderColor, this.Style.BackColor, this.Style.Texture));
            var foreRectangle = new Microsoft.Xna.Framework.Rectangle(this.ScreenLocation.X, this.ScreenLocation.Y, (int)(unit * this.Value), (int)this.Size.Height);
            this.Shapes.Add(new MonoForms.Shapes.Rectangle(foreRectangle, this.Style.BorderThickness, Color.Transparent, this.Style.ForeColor, this.Style.Texture));
        }

        private void InitializeMembers()
        {
            base.Initialize();
            this.Maximum = ProgressBar.DefaultMaximum;
            this.Size = new Size(ProgressBar.DefaultWidth, ProgressBar.DefaultHeight);
            this.DefaultStyle = new Style(Color.LightGray, Color.Gray, 1, null, null, Color.Green, null, StyleType.Default);
            this.Style = new Style(Color.LightGray, Color.Gray, 1, null, null, Color.Green, null, StyleType.Default);
            this.ActiveStyle = new Style(Color.LightGray, Color.Gray, 1, null, null, Color.Green, null, StyleType.Active);
            this.ActionStyle = new Style(Color.LightGray, Color.Gray, 1, null, null, Color.Green, null, StyleType.Action);
            this.ValueChanged += this.ValuesChanged;
            this.MaximumChanged += this.ValuesChanged;
            this.MinimumChanged += this.ValuesChanged;
        }

        private void ValuesChanged(object sender, PropertyChangedEventArgs<double> e)
        {
            this.Refresh();
        }
    }
}
