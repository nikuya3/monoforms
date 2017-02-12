// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Size.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Core
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Describes the size of an object.
    /// </summary>
    public struct Size : IComparable, IComparable<Size>, IEquatable<Size>
    {
        private const float EmptyValue = float.NegativeInfinity;
        private float height;
        private float width;

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> struct with the given <paramref name="width"/> and <paramref name="height"/>.
        /// </summary>
        /// <param name="width">
        /// A <see cref="float"/> indicating the initial width of this <see cref="Size"/>.
        /// </param>
        /// <param name="height">
        /// A <see cref="float"/> indicating the initial height of this <see cref="Size"/>.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is negative.
        /// </exception>
        public Size(float width, float height) : this()
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "width must be non-negative");
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "height must be non-negative");
            }

            this.IsEmpty = false;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Gets an empty <see cref="Size"/>.
        /// </summary>
        public static Size Empty
        {
            get
            {
                return new Size()
                {
                    height = Size.EmptyValue,
                    width = Size.EmptyValue,
                    IsEmpty = true
                };
            }
        }

        /// <summary>
        /// Gets or sets the non-negative height of this instance of <see cref="Size"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Height"/> was modified in <see cref="Empty"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The given value is negative.
        /// </exception>
        public float Height
        {
            get
            {
                return this.height;
            }

            set
            {
                if (this.IsEmpty)
                {
                    throw new InvalidOperationException("Cannot modify Size.Height in Size.Empty.");
                }

                if (value < 0)
                {
                    throw new ArgumentException("value must be non-negative.");
                }

                this.height = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance of <see cref="Size"/> equals <see cref="Empty"/>.
        /// </summary>
        public bool IsEmpty { get; private set; }

        /// <summary>
        /// Gets or sets the non-negative width of this instance of <see cref="Size"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Width"/> was modified in <see cref="Empty"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The given value is negative.
        /// </exception>
        public float Width
        {
            get
            {
                return this.width;
            }

            set
            {
                if (this.IsEmpty)
                {
                    throw new InvalidOperationException("Cannot modify Size.Width in Size.Empty.");
                }

                if (value < 0)
                {
                    throw new ArgumentException("value must be non-negative.");
                }

                this.width = value;
            }
        }

        /// <summary>
        /// Adds an instance of <see cref="Size"/> to another instance of <see cref="Size"/> and returns the result.
        /// </summary>
        /// <param name="size1">
        /// The first instance of <see cref="Size"/> to be added.
        /// </param>
        /// <param name="size2">
        /// The second instance of <see cref="Size"/> to be added.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Size"/> indicating the result of the addition of the given <see cref="Size"/>s.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The result of the addition is negative.
        /// </exception>
        public static Size operator +(Size size1, Size size2)
        {
            if (size1 == null)
            {
                size1 = new Size();
            }

            if (size2 == null)
            {
                size2 = new Size();
            }

            try
            {
                return new Size(size1.Width + size2.Width, size1.Height + size2.Height);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidOperationException("The result of the addition can not be negative.");
            }
        }

        /// <summary>
        /// Subtracts an instance of <see cref="Size"/> from another instance of <see cref="Size"/> and returns the result.
        /// </summary>
        /// <param name="size1">
        /// An instance of <see cref="Size"/> to subtracted from.
        /// </param>
        /// <param name="size2">
        /// An instance of <see cref="Size"/> to subtracted <paramref name="size1"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Size"/> indicating the result of the subtraction of the given <see cref="Size"/>s.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The result of the subtraction is negative.
        /// </exception>
        public static Size operator -(Size size1, Size size2)
        {
            if (size1 == null)
            {
                size1 = new Size();
            }

            if (size2 == null)
            {
                size2 = new Size();
            }

            try
            {
                return new Size(size1.Width - size2.Width, size1.Height - size2.Height);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidOperationException("The result of the subtraction can not be negative.");
            }
        }

        /// <summary>
        /// Multiplies an instance of <see cref="Size"/> with another instance of <see cref="Size"/> and returns the result.
        /// </summary>
        /// <param name="size1">
        /// The first instance of <see cref="Size"/> to be multiplied.
        /// </param>
        /// <param name="size2">
        /// The second instance of <see cref="Size"/> to be multiplied.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Size"/> indicating the result of the multiplication of the given <see cref="Size"/>s.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The result of the multiplication negative.
        /// </exception>
        public static Size operator *(Size size1, Size size2)
        {
            if (size1 == null)
            {
                size1 = new Size();
            }

            if (size2 == null)
            {
                size2 = new Size();
            }

            try
            {
                return new Size(size1.Width * size2.Width, size1.Height * size2.Height);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidOperationException("The result of the multiplication can not be negative.");
            }
        }

        /// <summary>
        /// Divides an instance <see cref="Size"/> by another instance of <see cref="Size"/> and returns the result.
        /// </summary>
        /// <param name="size1">
        /// An instance of <see cref="Size"/> to be divided.
        /// </param>
        /// <param name="size2">
        /// An instance of <see cref="Size"/> to divide <paramref name="size1"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Size"/> indicating the result of the division.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The result of the division is negative.
        /// </exception>
        public static Size operator /(Size size1, Size size2)
        {
            if (size1.IsEmpty)
            {
                size1 = new Size();
            }

            if (size2.IsEmpty)
            {
                size2 = new Size();
            }

            try
            {
                return new Size(size1.Width / size2.Width, size1.Height / size2.Height);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidOperationException("The result of the subtraction can not be negative.");
            }
        }

        /// <summary>
        /// Divides an instance <see cref="Size"/> by a given <see cref="float"/>.
        /// </summary>
        /// <param name="size">
        /// An instance of <see cref="Size"/> to be divided.
        /// </param>
        /// <param name="divisor">
        /// A <see cref="float"/> used as divisor.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Size"/> indicating the result of the division.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="size"/> is <see cref="Empty"/>.
        /// </exception>
        /// <exception cref="DivideByZeroException">
        /// <paramref name="divisor"/> is 0.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The result of the division is negative.
        /// </exception>
        public static Size operator /(Size size, float divisor)
        {
            if (size.IsEmpty)
            {
                throw new ArgumentException("size must not be Size.Empty.", "size");
            }

            if (divisor == 0)
            {
                throw new DivideByZeroException();
            }

            try
            {
                return new Size(size.Width / divisor, size.Height / divisor);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidOperationException("The result of the subtraction can not be negative.");
            }
        }

        /// <summary>
        /// Compares two instances of <see cref="Size"/> for inequality.
        /// </summary>
        /// <param name="size1">
        /// The first instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <param name="size2">
        /// The second instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the two instances of <see cref="Size"/> are unequal.
        /// </returns>
        public static bool operator !=(Size size1, Size size2)
        {
            return !(size1 == size2);
        }

        /// <summary>
        /// Compares two instances of <see cref="Size"/> for equality.
        /// </summary>
        /// <param name="size1">
        /// The first instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <param name="size2">
        /// The second instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the two instances of <see cref="Size"/> are equal.
        /// </returns>
        public static bool operator ==(Size size1, Size size2)
        {
            if (object.ReferenceEquals(size1, size2))
            {
                return true;
            }

            if ((object)size1 == null || (object)size2 == null)
            {
                return false;
            }

            return size1.Width == size2.Width && size1.Height == size2.Height;
        }

        /// <summary>
        /// Checks whether an instance of <see cref="Size"/> is greater than another instance of <see cref="Size"/>.
        /// </summary>
        /// <param name="size1">
        /// The first instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <param name="size2">
        /// The second instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not <paramref name="size1"/> is greater than <paramref name="size2"/>.
        /// </returns>
        public static bool operator >(Size size1, Size size2)
        {
            if (size1 == null)
            {
                size1 = new Size();
            }

            if (size2 == null)
            {
                size2 = new Size();
            }

            return size1.Width + size1.Height > size2.Width + size2.Height;
        }

        /// <summary>
        /// Checks whether an instance of <see cref="Size"/> is less than another instance of <see cref="Size"/>.
        /// </summary>
        /// <param name="size1">
        /// The first instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <param name="size2">
        /// The second instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not <paramref name="size1"/> is less than <paramref name="size2"/>.
        /// </returns>
        public static bool operator <(Size size1, Size size2)
        {
            if (size1 == null)
            {
                size1 = new Size();
            }

            if (size2 == null)
            {
                size2 = new Size();
            }

            return size1.Width + size1.Height < size2.Width + size2.Height;
        }

        /// <summary>
        /// Checks whether an instance of <see cref="Size"/> is greater than or equals another instance of <see cref="Size"/>.
        /// </summary>
        /// <param name="size1">
        /// The first instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <param name="size2">
        /// The second instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not <paramref name="size1"/> is greater than or equals <paramref name="size2"/>.
        /// </returns>
        public static bool operator >=(Size size1, Size size2)
        {
            return size1 > size2 || size1 == size2;
        }

        /// <summary>
        /// Checks whether an instance of <see cref="Size"/> is less than or equals another instance of <see cref="Size"/>.
        /// </summary>
        /// <param name="size1">
        /// The first instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <param name="size2">
        /// The second instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not <paramref name="size1"/> is less than or equals <paramref name="size2"/>.
        /// </returns>
        public static bool operator <=(Size size1, Size size2)
        {
            return size1 < size2 || size1 == size2;
        }

        /// <summary>
        /// Converts an instance of <see cref="Size"/> into an instance of <see cref="Point"/>.
        /// </summary>
        /// <param name="size">
        /// An instance of <see cref="Size"/> to convert.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Point"/> with the <see cref="Point.X"/> and <see cref="Point.Y"/> values conforming the <see cref="Width"/> and <see cref="Height"/> values.
        /// </returns>
        public static explicit operator Point(Size size)
        {
            return new Point((int)size.Width, (int)size.Height);
        }

        /// <summary>
        /// Adds an instance of <see cref="Size"/> to another instance of <see cref="Size"/> and returns the result.
        /// </summary>
        /// <param name="size1">
        /// The first instance of <see cref="Size"/> to be added.
        /// </param>
        /// <param name="size2">
        /// The second instance of <see cref="Size"/> to be added.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Size"/> indicating the result of the addition of the given <see cref="Size"/>s.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The result of the addition is negative.
        /// </exception>
        public static Size Add(Size size1, Size size2)
        {
            return size1 + size2;
        }

        /// <inheritdoc/>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            Size otherSize = (Size)obj;
            if (otherSize != null)
            {
                return (this.Width + this.Height).CompareTo(otherSize.Width + otherSize.Height);
            }
            else
            {
                throw new ArgumentException("obj is not of type {0}.", this.GetType().ToString());
            }
        }

        /// <inheritdoc/>
        public int CompareTo(Size other)
        {
            if (other == null)
            {
                return 1;
            }

            return (this.Width + this.Height).CompareTo(other.Width + other.Height);
        }

        /// <summary>
        /// Divides an instance <see cref="Size"/> by another instance of <see cref="Size"/> and returns the result.
        /// </summary>
        /// <param name="size1">
        /// An instance of <see cref="Size"/> to be divided.
        /// </param>
        /// <param name="size2">
        /// An instance of <see cref="Size"/> to divide <paramref name="size1"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Size"/> indicating the result of the division of the given <see cref="Size"/>s.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The result of the division is negative.
        /// </exception>
        public static Size Divide(Size size1, Size size2)
        {
            return size1 / size2;
        }

        /// <summary>
        /// Divides an instance <see cref="Size"/> by a given <see cref="float"/>.
        /// </summary>
        /// <param name="size">
        /// An instance of <see cref="Size"/> to be divided.
        /// </param>
        /// <param name="divisor">
        /// A <see cref="float"/> used as divisor.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Size"/> indicating the result of the division.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="size"/> is <see cref="Empty"/>.
        /// </exception>
        /// <exception cref="DivideByZeroException">
        /// <paramref name="divisor"/> is 0.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The result of the division is negative.
        /// </exception>
        public static Size Divide(Size size, float divisor)
        {
            if (size.IsEmpty)
            {
                throw new ArgumentException("size must not be Size.Empty.", "size");
            }

            if (divisor == 0)
            {
                throw new DivideByZeroException();
            }

            try
            {
                return new Size(size.Width / divisor, size.Height / divisor);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidOperationException("The result of the subtraction can not be negative.");
            }
        }

        /// <summary>
        /// Compares two instances of <see cref="Size"/> for equality.
        /// </summary>
        /// <param name="size1">
        /// The first instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <param name="size2">
        /// The second instance of <see cref="Size"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether <paramref name="size1"/> equals <paramref name="size2"/>.
        /// </returns>
        public static bool Equals(Size size1, Size size2)
        {
            if (size1 == null || size2 == null)
            {
                return false;
            }

            return size1.Equals(size2);
        }

        /// <summary>
        /// Returns a new instance of <see cref="Size"/> from a given <see cref="string"/>.
        /// </summary>
        /// <param name="source">
        /// A <see cref="string"/> representing the source of the new instance of <see cref="Size"/>.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="Size"/> generated from the values contained in <paramref name="source"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is a null reference.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="source"/> is not in the correct format.
        /// </exception>
        public static Size Parse(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            string[] values = source.Split(',');
            float width, height;
            if (values.Length == 2 && float.TryParse(values[0], out width) && float.TryParse(values[1], out height))
            {
                return new Size(width, height);
            }
            else
            {
                throw new FormatException("source is not in the correct format.");
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Size toCompare = (Size)obj;
            if ((object)toCompare == null)
            {
                return false;
            }

            return this.Width == toCompare.Width && this.Height == toCompare.Height;
        }

        /// <inheritdoc/>
        public bool Equals(Size other)
        {
            if ((object)other == null)
            {
                return false;
            }

            return this.Width == other.Width && this.Height == other.Height;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int width = (int)this.Width;
            int height = (int)this.Height;
            return width ^ height;
        }

        /// <summary>
        /// Multiplies an instance of <see cref="Size"/> with another instance of <see cref="Size"/> and returns the result.
        /// </summary>
        /// <param name="size1">
        /// The first instance of <see cref="Size"/> to be multiplied.
        /// </param>
        /// <param name="size2">
        /// The second instance of <see cref="Size"/> to be multiplied.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Size"/> indicating the result of the multiplication of the given <see cref="Size"/>s.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The result of the multiplication negative.
        /// </exception>
        public static Size Multiply(Size size1, Size size2)
        {
            return size1 * size2;
        }

        /// <summary>
        /// Subtracts an instance of <see cref="Size"/> from another instance of <see cref="Size"/> and returns the result.
        /// </summary>
        /// <param name="size1">
        /// An instance of <see cref="Size"/> to subtracted from.
        /// </param>
        /// <param name="size2">
        /// An instance of <see cref="Size"/> to subtracted <paramref name="size1"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Size"/> indicating the result of the subtraction of the given <see cref="Size"/>s.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The result of the subtraction is negative.
        /// </exception>
        public static Size Subtract(Size size1, Size size2)
        {
            return size1 - size2;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            if (this.IsEmpty)
            {
                return "Empty";
            }
            else
            {
                return string.Format("{0},{1}", this.Width, this.Height);
            }
        }
    }
}
