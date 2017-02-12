// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rectangle.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Core
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Represents a rectangle.
    /// </summary>
    public struct Rectangle
    {
        private Point location;
        private Size size;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> structure with the given size.
        /// </summary>
        /// <param name="size">
        /// An instance of <see cref="Size"/> indicating the size of the <see cref="Rectangle"/>.
        /// </param>
        public Rectangle(Size size) : this()
        {
            this.size = size;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> structure where <see cref="TopLeft"/> is located at the given location and which has the given size.
        /// </summary>
        /// <param name="location">
        /// An instance of <see cref="Point"/> indicating the location of <see cref="TopLeft"/> of the <see cref="Rectangle"/>.
        /// </param>
        /// <param name="size">
        /// An instance of <see cref="Size"/> indicating the size of the <see cref="Rectangle"/>.
        /// </param>
        public Rectangle(Point location, Size size) : this()
        {
            this.location = location;
            this.size = size;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> structure with the given values.
        /// </summary>
        /// <param name="x">
        /// The X-coordinate of <see cref="TopLeft"/> of the <see cref="Rectangle"/>.
        /// </param>
        /// <param name="y">
        /// The Y-coordinate of <see cref="TopLeft"/> of the <see cref="Rectangle"/>.
        /// </param>
        /// <param name="width">
        /// The width of the <see cref="Rectangle"/>.
        /// </param>
        /// <param name="height">
        /// The height of the <see cref="Rectangle"/>.
        /// </param>
        public Rectangle(float x, float y, float width, float height) : this()
        {
            this.location = new Point((int)x, (int)y);
            this.size = new Size(width, height);
        }

        /// <summary>
        /// Gets an empty instance of <see cref="Rectangle"/>.
        /// </summary>
        public static Rectangle Empty => new Rectangle
                                             {
                                                 IsEmpty = true,
                                                 Location = Point.Zero,
                                                 Size = Size.Empty
                                             };

        /// <summary>
        /// Gets the y-axis of the bottom of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public float Bottom => this.Y + this.Height;

        /// <summary>
        /// Gets the position of the bottom-left corner of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public Point BottomLeft => new Point((int)this.Left, (int)this.Bottom);

        /// <summary>
        /// Gets the position of the bottom-right corner of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public Point BottomRight => new Point((int)this.Right, (int)this.Bottom);

        /// <summary>
        /// Gets or sets the height of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public float Height
        {
            get
            {
                return this.size.Height;
            }

            set
            {
                this.size.Height = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance of <see cref="Rectangle"/> equals <see cref="Rectangle.Empty"/>.
        /// </summary>
        public bool IsEmpty { get; private set; }

        /// <summary>
        /// Gets the x-axis value of the left side of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public float Left => this.X;

        /// <summary>
        /// Gets or sets the position of the top-left corner of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public Point Location
        {
            get
            {
                return this.location;
            }

            set
            {
                this.location = value;
            }
        }

        /// <summary>
        /// Gets the x-axis value of the right side of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public float Right => this.X + this.Width;

        /// <summary>
        /// Gets or sets the size of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public Size Size
        {
            get
            {
                return this.size;
            }

            set
            {
                this.size = value;
            }
        }

        /// <summary>
        /// Gets the y-axis of the top of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public float Top => this.Y;

        /// <summary>
        /// Gets the position of the top-left corner of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public Point TopLeft => new Point((int)this.Left, (int)this.Top);

        /// <summary>
        /// Gets the position of the top-right corner of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public Point TopRight => new Point((int)this.Right, (int)this.Top);

        /// <summary>
        /// Gets or sets the width of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public float Width
        {
            get
            {
                return this.size.Width;
            }

            set
            {
                this.size.Width = value;
            }
        }

        /// <summary>
        /// Gets or sets the x-axis value of the location of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public float X
        {
            get
            {
                return this.location.X;
            }

            set
            {
                this.location.X = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets the y-axis value of the location of this instance of <see cref="Rectangle"/>.
        /// </summary>
        public float Y
        {
            get
            {
                return this.location.Y;
            }

            set
            {
                this.location.Y = (int)value;
            }
        }

        /// <summary>
        /// Compares two instances of <see cref="Rectangle"/> for inequality.
        /// </summary>
        /// <param name="rectangle1">
        /// The first instance of <see cref="Rectangle"/> to be compared.
        /// </param>
        /// <param name="rectangle2">
        /// The second instance of <see cref="Rectangle"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the two instances of <see cref="Rectangle"/> are unequal.
        /// </returns>
        public static bool operator !=(Rectangle rectangle1, Rectangle rectangle2)
        {
            return !(rectangle1 == rectangle2);
        }

        /// <summary>
        /// Compares two instances of <see cref="Rectangle"/> for equality.
        /// </summary>
        /// <param name="rectangle1">
        /// The first instance of <see cref="Rectangle"/> to be compared.
        /// </param>
        /// <param name="rectangle2">
        /// The second instance of <see cref="Rectangle"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the two instances of <see cref="Rectangle"/> are equal.
        /// </returns>
        public static bool operator ==(Rectangle rectangle1, Rectangle rectangle2)
        {
            if (object.ReferenceEquals(rectangle1, rectangle2))
            {
                return true;
            }

            if ((object)rectangle1 == null || (object)rectangle2 == null)
            {
                return false;
            }

            return rectangle1.Location == rectangle2.Location && rectangle1.Size == rectangle2.Size;
        }

        /// <summary>
        /// Creates an instance of <see cref="Microsoft.Xna.Framework.Rectangle"/> equal to this instance of <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rectangle">
        /// An instance of <see cref="Rectangle"/> to be converted.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Microsoft.Xna.Framework.Rectangle"/> equal to this instance of <see cref="Rectangle"/>.
        /// </returns>
        public static explicit operator Microsoft.Xna.Framework.Rectangle(Rectangle rectangle)
        {
            return new Microsoft.Xna.Framework.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
        }

        /// <summary>
        /// Compares two instances of <see cref="Rectangle"/> for equality.
        /// </summary>
        /// <param name="rectangle1">
        /// The first instance of <see cref="Rectangle"/> to be compared.
        /// </param>
        /// <param name="rectangle2">
        /// The second instance of <see cref="Rectangle"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether <paramref name="rectangle1"/> equals <paramref name="rectangle2"/>.
        /// </returns>
        public static bool Equals(Rectangle rectangle1, Rectangle rectangle2)
        {
            if (rectangle1 == null || rectangle2 == null)
            {
                return false;
            }

            return rectangle1.Equals(rectangle2);
        }

        /// <summary>
        /// Checks whether this instance of <see cref="Rectangle"/> contains the given instance of <see cref="Point"/>.
        /// </summary>
        /// <param name="point">
        /// An instance of <see cref="Point"/> to be checked.
        /// </param>
        /// <returns>
        /// A value indicating whether this instance of <see cref="Rectangle"/> contains the given instance of <see cref="Point"/>.
        /// </returns>
        public bool Contains(Point point)
        {
            return this.X <= point.X && point.X < this.Right && this.Y <= point.Y && point.Y < this.Bottom;
        }

        /// <summary>
        /// Checks whether this instance of <see cref="Rectangle"/> contains the given instance of <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rectangle">
        /// An instance of <see cref="Rectangle"/> to be checked.
        /// </param>
        /// <returns>
        /// A value indicating whether this instance of <see cref="Rectangle"/> contains the given instance of <see cref="Rectangle"/>.
        /// </returns>
        public bool Contains(Rectangle rectangle)
        {
            return this.X <= rectangle.X && rectangle.Right <= this.Right && this.Y <= rectangle.Y && rectangle.Bottom <= this.Bottom;
        }

        /// <summary>
        /// Checks whether this instance of <see cref="Rectangle"/> contains the given instance of <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vector">
        /// An instance of <see cref="Vector2"/> to be checked.
        /// </param>
        /// <returns>
        /// A value indicating whether this instance of <see cref="Rectangle"/> contains the given instance of <see cref="Vector2"/>.
        /// </returns>
        public bool Contains(Vector2 vector)
        {
            return this.X <= vector.X && vector.X < this.Right && this.Y <= vector.Y && vector.Y < this.Bottom;
        }

        /// <summary>
        /// Checks whether this instance of <see cref="Rectangle"/> contains the given x and y values.
        /// </summary>
        /// <param name="x">
        /// The x value to be checked.
        /// </param>
        /// <param name="y">
        /// The y value to be checked.
        /// </param>
        /// <returns>
        /// A value indicating whether this instance of <see cref="Rectangle"/> contains the given x and y values.
        /// </returns>
        public bool Contains(float x, float y)
        {
            return this.X <= x && x < this.Right && this.Y <= y && y < this.Bottom;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Rectangle toCompare = (Rectangle)obj;
            if ((object)toCompare == null)
            {
                return false;
            }

            return this.Location == toCompare.Location && this.Size == toCompare.Size;
        }

        /// <summary>
        /// Compares a given instance of <see cref="Rectangle"/> and this instance of <see cref="Rectangle"/> for equality.
        /// </summary>
        /// <param name="value">
        /// An instance of <see cref="Rectangle"/> to be compared.
        /// </param>
        /// <returns>
        /// A value indicating whether <paramref name="value"/> equals this instance of <see cref="Rectangle"/>.
        /// </returns>
        public bool Equals(Rectangle value)
        {
            if ((object)value == null)
            {
                return false;
            }

            return this.Location == value.Location && this.Size == value.Size;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Location.X ^ this.Location.Y ^ (int)this.Size.Width ^ (int)this.Size.Height;
        }
    }
}
