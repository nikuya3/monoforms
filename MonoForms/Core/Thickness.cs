// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Thickness.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Core
{
    using System;

    /// <summary>
    /// Describes the top-, right-, left- and bottom-thickness of a frame.
    /// </summary>
    public struct Thickness
    {
        private float bottom;
        private float left;
        private float right;
        private float top;

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> struct and assigns <see cref="Left"/>, <see cref="Top"/>, <see cref="Right"/> and <see cref="Bottom"/> the <see cref="float"/>;
        /// </summary>
        /// <param name="uniformLength">
        /// A <see cref="float"/> indicating the uniform length of the member instance of <see cref="Thickness"/>.
        /// </param>
        public Thickness(float uniformLength)
        {
            this.bottom = uniformLength;
            this.left = uniformLength;
            this.right = uniformLength;
            this.top = uniformLength;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> struct with the given values.
        /// </summary>
        /// <param name="left">
        /// A <see cref="float"/> indicating the thickness of the left side.
        /// </param>
        /// <param name="top">
        /// A <see cref="float"/> indicating the thickness of the top side.
        /// </param>
        /// <param name="right">
        /// A <see cref="float"/> indicating the thickness of the right side.
        /// </param>
        /// <param name="bottom">
        /// A <see cref="float"/> indicating the thickness of the bottom side.
        /// </param>
        public Thickness(float left, float top, float right, float bottom)
        {
            this.bottom = bottom;
            this.left = left;
            this.right = right;
            this.top = top;
        }

        /// <summary>
        /// Gets or sets the width of the lower side of the frame.
        /// </summary>
        public float Bottom
        {
            get
            {
                return this.bottom;
            }

            set
            {
                this.bottom = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the left side of the frame.
        /// </summary>
        public float Left
        {
            get
            {
                return this.left;
            }

            set
            {
                this.left = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the right side of the frame.
        /// </summary>
        public float Right
        {
            get
            {
                return this.right;
            }

            set
            {
                this.right = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the upper side of the frame.
        /// </summary>
        public float Top
        {
            get
            {
                return this.top;
            }

            set
            {
                this.top = value;
            }
        }

        /// <summary>
        /// Adds an instance of <see cref="Thickness"/> to another instance of <see cref="Thickness"/> and returns the result.
        /// </summary>
        /// <param name="thickness1">
        /// The first instance of <see cref="Thickness"/> to be added.
        /// </param>
        /// <param name="thickness2">
        /// The second instance of <see cref="Thickness"/> to be added.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Thickness"/> indicating the result of the addition of the given instances of <see cref="Thickness"/>.
        /// </returns>
        public static Thickness operator +(Thickness thickness1, Thickness thickness2)
        {
            if (thickness1 == null)
            {
                thickness1 = new Thickness();
            }

            if (thickness2 == null)
            {
                thickness2 = new Thickness();
            }

            return new Thickness(thickness1.Left + thickness2.Left, thickness1.Top + thickness2.Top, thickness1.Right + thickness2.Right, thickness1.Bottom + thickness2.Bottom);
        }

        /// <summary>
        /// Subtracts an instance of <see cref="Thickness"/> from another instance of <see cref="Thickness"/> and returns the result.
        /// </summary>
        /// <param name="thickness1">
        /// An instance of <see cref="Thickness"/> to be subtracted from.
        /// </param>
        /// <param name="thickness2">
        /// An instance of <see cref="Thickness"/> to be subtract <paramref name="thickness1"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Thickness"/> indicating the result of the subtraction of the given instances of <see cref="Thickness"/>.
        /// </returns>
        public static Thickness operator -(Thickness thickness1, Thickness thickness2)
        {
            if (thickness1 == null)
            {
                thickness1 = new Thickness();
            }

            if (thickness2 == null)
            {
                thickness2 = new Thickness();
            }

            return new Thickness(thickness1.Left - thickness2.Left, thickness1.Top - thickness2.Top, thickness1.Right - thickness2.Right, thickness1.Bottom - thickness2.Bottom);
        }

        /// <summary>
        /// Multiplies an instance of <see cref="Thickness"/> with another instance of <see cref="Thickness"/> and returns the result.
        /// </summary>
        /// <param name="thickness1">
        /// The first instance of <see cref="Thickness"/> to be multiplied.
        /// </param>
        /// <param name="thickness2">
        /// The second instance of <see cref="Thickness"/> to be multiplied.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Thickness"/> indicating the result of the multiplication of the given instances of <see cref="Thickness"/>.
        /// </returns>
        public static Thickness operator *(Thickness thickness1, Thickness thickness2)
        {
            if (thickness1 == null)
            {
                thickness1 = new Thickness();
            }

            if (thickness2 == null)
            {
                thickness2 = new Thickness();
            }

            return new Thickness(thickness1.Left * thickness2.Left, thickness1.Top * thickness2.Top, thickness1.Right * thickness2.Right, thickness1.Bottom * thickness2.Bottom);
        }

        /// <summary>
        /// Divides an instance of <see cref="Thickness"/> by another instance of <see cref="Thickness"/> and returns the result.
        /// </summary>
        /// <param name="thickness1">
        /// An instance of <see cref="Thickness"/> to be divided.
        /// </param>
        /// <param name="thickness2">
        /// An instance of <see cref="Thickness"/> to divide <paramref name="thickness1"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Thickness"/> indicating the result of the division of the given instances of <see cref="Thickness"/>.
        /// </returns>
        public static Thickness operator /(Thickness thickness1, Thickness thickness2)
        {
            if (thickness1 == null)
            {
                thickness1 = new Thickness();
            }

            if (thickness2 == null)
            {
                thickness2 = new Thickness();
            }

            return new Thickness(thickness1.Left / thickness2.Left, thickness1.Top / thickness2.Top, thickness1.Right / thickness2.Right, thickness1.Bottom / thickness2.Bottom);
        }

        /// <summary>
        /// Compares two instances of <see cref="Thickness"/> for inequality.
        /// </summary>
        /// <param name="thickness1">
        /// The first instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <param name="thickness2">
        /// The second instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the two instances of <see cref="Thickness"/> are unequal.
        /// </returns>
        public static bool operator !=(Thickness thickness1, Thickness thickness2)
        {
            return !(thickness1 == thickness2);
        }

        /// <summary>
        /// Compares two instances of <see cref="Thickness"/> for equality.
        /// </summary>
        /// <param name="thickness1">
        /// The first instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <param name="thickness2">
        /// The second instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the two instances of <see cref="Thickness"/> are equal.
        /// </returns>
        public static bool operator ==(Thickness thickness1, Thickness thickness2)
        {
            if (object.ReferenceEquals(thickness1, thickness2))
            {
                return true;
            }

            if ((object)thickness1 == null || (object)thickness2 == null)
            {
                return false;
            }

            return thickness1.Left == thickness2.Left && thickness1.Top == thickness2.Top && thickness1.Right == thickness2.Right && thickness1.Bottom == thickness2.Bottom;
        }

        /// <summary>
        /// Checks whether an instance of <see cref="Thickness"/> is greater than another instance of <see cref="Thickness"/>.
        /// </summary>
        /// <param name="thickness1">
        /// The first instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <param name="thickness2">
        /// The second instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not <paramref name="thickness1"/> is greater than <paramref name="thickness2"/>.
        /// </returns>
        public static bool operator >(Thickness thickness1, Thickness thickness2)
        {
            if (thickness1 == null)
            {
                thickness1 = new Thickness();
            }

            if (thickness2 == null)
            {
                thickness2 = new Thickness();
            }

            return thickness1.Left + thickness1.Top + thickness1.Right + thickness1.Bottom > thickness2.Left + thickness2.Top + thickness2.Right + thickness2.Bottom;
        }

        /// <summary>
        /// Checks whether an instance of <see cref="Thickness"/> is less than another instance of <see cref="Thickness"/>.
        /// </summary>
        /// <param name="thickness1">
        /// The first instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <param name="thickness2">
        /// The second instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not <paramref name="thickness1"/> is less than <paramref name="thickness2"/>.
        /// </returns>
        public static bool operator <(Thickness thickness1, Thickness thickness2)
        {
            if (thickness1 == null)
            {
                thickness1 = new Thickness();
            }

            if (thickness2 == null)
            {
                thickness2 = new Thickness();
            }

            return thickness1.Left + thickness1.Top + thickness1.Right + thickness1.Bottom < thickness2.Left + thickness2.Top + thickness2.Right + thickness2.Bottom;
        }

        /// <summary>
        /// Checks whether an instance of <see cref="Thickness"/> is greater than or equals another instance of <see cref="Thickness"/>.
        /// </summary>
        /// <param name="thickness1">
        /// The first instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <param name="thickness2">
        /// The second instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not <paramref name="thickness1"/> is greater than or equals <paramref name="thickness2"/>.
        /// </returns>
        public static bool operator >=(Thickness thickness1, Thickness thickness2)
        {
            return thickness1 > thickness2 || thickness1 == thickness2;
        }

        /// <summary>
        /// Checks whether an instance of <see cref="Thickness"/> is less than or equals another instance of <see cref="Thickness"/>.
        /// </summary>
        /// <param name="thickness1">
        /// The first instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <param name="thickness2">
        /// The second instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not <paramref name="thickness1"/> is less than or equals <paramref name="thickness2"/>.
        /// </returns>
        public static bool operator <=(Thickness thickness1, Thickness thickness2)
        {
            return thickness1 < thickness2 || thickness1 == thickness2;
        }

        /// <summary>
        /// Compares two instances of <see cref="Thickness"/> for equality.
        /// </summary>
        /// <param name="thickness1">
        /// The first instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <param name="thickness2">
        /// The second instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether <paramref name="thickness1"/> equals <paramref name="thickness2"/>.
        /// </returns>
        public static bool Equals(Thickness thickness1, Thickness thickness2)
        {
            if (thickness1 == null || thickness2 == null)
            {
                return false;
            }

            return thickness1.Equals(thickness2);
        }

        /// <summary>
        /// Returns a new instance of <see cref="Thickness"/> from a given <see cref="string"/>.
        /// </summary>
        /// <param name="source">
        /// A <see cref="string"/> representing the source of the new instance of <see cref="Thickness"/>.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="Thickness"/> generated from the values contained in <paramref name="source"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is a null reference.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="source"/> is not in the correct format.
        /// </exception>
        public static Thickness Parse(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            string[] values = source.Split(',');
            float left, top, right, bottom;
            if (values.Length == 4 && float.TryParse(values[0], out left) && float.TryParse(values[1], out top) && float.TryParse(values[2], out right) && float.TryParse(values[3], out bottom))
            {
                return new Thickness(left, top, right, bottom);
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

            Thickness toCompare = (Thickness)obj;
            if ((object)toCompare == null)
            {
                return false;
            }

            return this.left == toCompare.Left && this.top == toCompare.Top && this.right == toCompare.Right && this.bottom == toCompare.Bottom;
        }

        /// <summary>
        /// Compares a given instance of <see cref="Thickness"/> and this instance of <see cref="Thickness"/> for equality.
        /// </summary>
        /// <param name="value">
        /// An instance of <see cref="Thickness"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether <paramref name="value"/> equals this instance of <see cref="Thickness"/>.
        /// </returns>
        public bool Equals(Thickness value)
        {
            if ((object)value == null)
            {
                return false;
            }

            return this.left == value.Left && this.top == value.Top && this.right == value.Right && this.bottom == value.Bottom;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (int)this.left ^ (int)this.top ^ (int)this.right ^ (int)this.bottom;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", this.left, this.top, this.right, this.bottom);
        }
    }
}
