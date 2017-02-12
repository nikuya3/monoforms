// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Slider.cs" company="">
//   Copyright (c) 2015 .
// </copyright>
// <summary>
//   Represents a bar and a thumb on it, with which a value can be get or set.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Controls
{
    using System;

    using Core;
    using Input;

    using Microsoft.Xna.Framework;

    /// <summary>
    /// Represents a bar and a thumb on it, with which a value can be get or set.
    /// </summary>
    public class Slider : RangeBase
    {
        private const int DefaultThumbHeight = 30;

        private const int DefaultThumbWidth = 10;

        private const int DefaultHeight = 30;

        private const int DefaultWidth = 120;

        private GameTime lastMouseUpdate = new GameTime();

        private Point previousMouseLocation;

        private Microsoft.Xna.Framework.Rectangle thumbRectangle;

        /// <summary>
        /// Initializes a new instance of the <see cref="Slider"/> class.
        /// </summary>
        public Slider()
        {
            this.InitializeMembers();
        }

        /// <summary>
        /// Gets or sets the amount of time in milliseconds until the value should be changed after a change of the thumb position.
        /// </summary>
        public int Interval { get; set; }

        /// <inheritdoc />
        public override void Initialize()
        {
            this.InitializeMembers();
        }

        /// <inheritdoc />
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (ComponentMouse.Captured != this
                || gameTime.TotalGameTime.Milliseconds - this.lastMouseUpdate.TotalGameTime.Milliseconds
                <= this.Interval)
            {
                return;
            }

            if (ComponentMouse.IsMouseDown)
            {
                Point mousePosition = ComponentMouse.GetMouseState(gameTime).Position;
                this.SliderMouseDown(mousePosition);
                this.previousMouseLocation = mousePosition;
            }
        }

        /// <inheritdoc />
        protected override Microsoft.Xna.Framework.Rectangle WholeRectangle { get; set; }

        /// <inheritdoc />
        protected override void InitializeShapes()
        {
            this.Shapes.Clear();
            this.Shapes.Add(new MonoForms.Shapes.Rectangle(this.WholeRectangle, 0, null, Color.Transparent, null));
            double unit = this.Size.Width / (this.Maximum - this.Minimum);
            this.thumbRectangle = new Microsoft.Xna.Framework.Rectangle(
                (int)(this.ScreenLocation.X + (this.Value * unit)) - (Slider.DefaultThumbWidth / 2),
                this.ScreenLocation.Y,
                Slider.DefaultThumbWidth,
                Slider.DefaultThumbHeight);
            this.Shapes.Add(new MonoForms.Shapes.Rectangle(this.thumbRectangle, this.Style));
            var barRectangle = new Microsoft.Xna.Framework.Rectangle(
                this.ScreenLocation.X,
                (int)(this.ScreenLocation.Y + (this.Size.Height / 2)),
                (int)this.Size.Width,
                this.Style.BorderThickness);
            this.Shapes.Add(new MonoForms.Shapes.Rectangle(barRectangle, 0, null, this.Style.ForeColor, null));
        }

        private void InitializeMembers()
        {
            base.Initialize();
            this.Interval = 33;
            this.Size = new Size(Slider.DefaultWidth, Slider.DefaultHeight);
            this.MouseClick += this.SliderMouseClick;
        }

        private void SliderMouseDown(Point location)
        {
            if (this.previousMouseLocation != Point.Zero)
            {
                double unit = this.Size.Width / (this.Maximum - this.Minimum);
                int distance = location.X - this.previousMouseLocation.X;
                if (Math.Abs(distance) > this.SmallChange * unit)
                {
                    if (Math.Abs(distance) > this.LargeChange * unit)
                    {
                        this.Value += distance > 0 ? this.LargeChange : -this.LargeChange;
                    }
                    else
                    {
                        this.Value += distance > 0 ? this.SmallChange : -this.SmallChange;
                    }
                }
            }
            
            this.Refresh();
        }

        private void SliderMouseClick(object sender, MouseEventArgs e)
        {
            double unit = this.Size.Width / (this.Maximum - this.Minimum);
            double distance = ((e.Location.X - this.ScreenLocation.X) - this.Value) / unit;
            double difference = distance < 0
                                    ? Math.Floor(distance / this.LargeChange) * this.LargeChange
                                    : Math.Ceiling(distance / this.LargeChange) * this.LargeChange;
            this.Value += difference;
            this.previousMouseLocation = e.Location;
        }
    }
}
