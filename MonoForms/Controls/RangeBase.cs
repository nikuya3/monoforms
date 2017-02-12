// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RangeBase.cs" company="">
//   Copyright (c) 2015 .
// </copyright>
// <summary>
//   Represents a <see cref="VisualControl"/> that has a value between a specific range.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Controls
{
    using System;

    using Core;

    /// <summary>
    /// Represents a <see cref="VisualControl"/> that has a value between a specific range.
    /// </summary>
    public abstract class RangeBase : VisualControl
    {
        private double largeChange;
        private double maximum;
        private double minimum;
        private double smallChange;
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeBase"/> class.
        /// </summary>
        protected RangeBase()
        {
            this.LargeChange = 5;
            this.Maximum = 100;
            this.SmallChange = 1;
        }

        /// <summary>
        /// Occurs when <see cref="LargeChange"/> changed.
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs<double>> LargeChangeChanged;

        /// <summary>
        /// Occurs when <see cref="Maximum"/> changed.
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs<double>> MaximumChanged;

        /// <summary>
        /// Occurs when <see cref="Minimum"/> changed.
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs<double>> MinimumChanged;

        /// <summary>
        /// Occurs when <see cref="SmallChange"/> changed.
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs<double>> SmallChangeChanged;

        /// <summary>
        /// Occurs when <see cref="Value"/> changed.
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs<double>> ValueChanged;

        /// <summary>
        /// Gets or sets a value to be added to <see cref="Value"/> when a large change occurs.
        /// </summary>
        public double LargeChange
        {
            get
            {
                return this.largeChange;
            }

            set
            {
                double previousValue = this.largeChange;
                this.largeChange = value;
                this.LargeChangeChanged?.Invoke(this, new PropertyChangedEventArgs<double>(nameof(this.LargeChange), previousValue, this.largeChange));
            }
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="Value"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// value is less than or equals <see cref="Maximum"/>.
        /// </exception>
        public double Maximum
        {
            get
            {
                return this.maximum;
            }

            set
            {
                if (value <= this.Minimum)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "value must be greater than RangeBase.Maximum.");
                }

                double previousValue = this.maximum;
                this.maximum = value;
                this.MaximumChanged?.Invoke(this, new PropertyChangedEventArgs<double>(nameof(this.Maximum), previousValue, this.maximum));
            }
        }

        /// <summary>
        /// Gets or sets the smalles possible <see cref="Value"/>.
        /// </summary>
        public double Minimum
        {
            get
            {
                return this.minimum;
            }

            set
            {
                if (value >= this.Maximum)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "value must be less than RangeBase.Maximum.");
                }

                double previousValue = this.minimum;
                this.minimum = value;
                this.MinimumChanged?.Invoke(this, new PropertyChangedEventArgs<double>(nameof(this.Minimum), previousValue, this.minimum));
            }
        }

        /// <summary>
        /// Gets or sets a value to be added to <see cref="Value"/> when a small change occurs.
        /// </summary>
        public double SmallChange
        {
            get
            {
                return this.smallChange;
            }

            set
            {
                double previousValue = this.smallChange;
                this.smallChange = value;
                this.SmallChangeChanged?.Invoke(this, new PropertyChangedEventArgs<double>(nameof(this.SmallChange), previousValue, this.smallChange));
            }
        }

        /// <summary>
        /// Gets the current value.
        /// </summary>
        public double Value
        {
            get
            {
                return this.value;
            }

            set
            {
                double previousValue = this.value;
                this.value = value > this.Maximum ? this.Maximum : value < this.Minimum ? this.Minimum : value;
                this.ValueChanged?.Invoke(this, new PropertyChangedEventArgs<double>(nameof(this.Value), previousValue, this.value));
            }
        }
    }
}
