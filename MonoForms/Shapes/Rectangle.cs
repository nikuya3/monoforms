// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rectangle.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Shapes
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Holds and draws a <see cref="Microsoft.Xna.Framework.Rectangle"/>.
    /// </summary>
    public class Rectangle : IShape
    {
        /// <summary>
        /// The bottom <see cref="Microsoft.Xna.Framework.Rectangle"/> to be drawn. Only used when a border is drawn.
        /// </summary>
        private Microsoft.Xna.Framework.Rectangle bottomRectangle;

        /// <summary>
        /// A value indicating whether a border should be drawn or not.
        /// </summary>
        private bool drawBorders = false;

        /// <summary>
        /// A <see cref="Rectangle"/> indicating the inner rectangle to be filled when a border is drawn.
        /// </summary>
        private Microsoft.Xna.Framework.Rectangle innerRectangle;

        /// <summary>
        /// The left <see cref="Microsoft.Xna.Framework.Rectangle"/> to be shown. Only used when a border is drawn.
        /// </summary>
        private Microsoft.Xna.Framework.Rectangle leftRectangle;

        /// <summary>
        /// The right <see cref="Microsoft.Xna.Framework.Rectangle"/> to be shown. Only used when a border is drawn.
        /// </summary>
        private Microsoft.Xna.Framework.Rectangle rightRectangle;

        /// <summary>
        /// The top <see cref="Microsoft.Xna.Framework.Rectangle"/> to be drawn.
        /// </summary>
        private Microsoft.Xna.Framework.Rectangle topRectangle;

        /// <summary>
        /// The <see cref="Texture2D"/> used to draw this <see cref="Rectangle"/>.
        /// </summary>
        private Style style;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="rectangle">
        /// A <see cref="Microsoft.Xna.Framework.Rectangle"/> holding the position of the <see cref="Rectangle"/>.
        /// </param>
        /// <param name="color">
        /// A <see cref="Color"/> indicating the color used to fill the <see cref="Rectangle"/>.
        /// </param>
        /// <param name="texture">
        /// A <see cref="Texture2D"/> to be used to draw the <see cref="IShape"/> or NULL, if the default <see cref="Texture2D"/> should be used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rectangle"/> and/or <paramref name="color"/> is/are NULL.
        /// </exception>
        public Rectangle(Microsoft.Xna.Framework.Rectangle rectangle, Color color, Texture2D texture)
        {
            if (rectangle == null || rectangle.IsEmpty)
            {
                throw new ArgumentNullException("rectangle");
            }

            if (color == null)
            {
                throw new ArgumentNullException("color");
            }

            this.topRectangle = rectangle;
            this.style = new Style(color, null, 0, null, null, null, texture, StyleType.Default);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class, which is to be drawn with a border.
        /// </summary>
        /// <param name="rectangle">
        /// A <see cref="Microsoft.Xna.Framework.Rectangle"/> holding the position of the <see cref="Rectangle"/>.
        /// </param>
        /// <param name="borderThickness">
        /// An  <see cref="int"/> indicating the thickness of the border to be drawn or NULL, if the default value should be used.
        /// </param>
        /// <param name="borderColor">
        /// A <see cref="Color"/> indicating the color in which the border should be drawn or NULL, if the default <see cref="Color"/> should be used.
        /// </param>
        /// <param name="fillColor">
        /// A <see cref="Color"/> indicating the color in which the inside of the <see cref="Rectangle"/> should be drawn.
        /// Use <see cref="Color.Transparent"/> to avoid filling. Pass NULL to use the default <see cref="Color"/>.
        /// </param>
        /// <param name="texture">
        /// A <see cref="Texture2D"/> to be used to draw the <see cref="IShape"/> or NULL, if the default <see cref="Texture2D"/> should be used.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="borderThickness"/> must be greater than or equals 0.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rectangle"/> was passed as NULL.
        /// </exception>
        public Rectangle(Microsoft.Xna.Framework.Rectangle rectangle, int? borderThickness, Color? borderColor, Color? fillColor, Texture2D texture)
        {
            if (rectangle == null || rectangle.IsEmpty)
            {
                throw new ArgumentNullException("rectangle");
            }

            if (borderThickness.Value < 0)
            {
                throw new ArgumentException("borderThickness must be greater than or equal 0.", "borderThickness");
            }

            this.SetUpRectangles(rectangle, borderThickness.Value);
            this.drawBorders = true;
            this.style = new Style(fillColor, borderColor, borderThickness, null, null, null, texture, StyleType.Default);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class which uses a given <see cref="Style"/>.
        /// </summary>
        /// <param name="rectangle">
        /// A <see cref="Microsoft.Xna.Framework.Rectangle"/> holding the position of the <see cref="Rectangle"/>.
        /// </param>
        /// <param name="style">
        /// A <see cref="Style"/> indicating the style of the <see cref="Rectangle"/>. Pass a <see cref="Style"/> containing a <see cref="MonoForms.Style.BorderThickness"/> with the value of 0 to draw a borderless <see cref="Rectangle"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rectangle"/> and/or <paramref name="style"/> was/were passed as NULL.
        /// </exception>
        public Rectangle(Microsoft.Xna.Framework.Rectangle rectangle, Style style)
        {
            if (rectangle == null || rectangle.IsEmpty)
            {
                throw new ArgumentNullException("rectangle");
            }

            if (style == null)
            {
                throw new ArgumentNullException("style");
            }

            if (style.BorderThickness > 0)
            {
                this.SetUpRectangles(rectangle, style.BorderThickness);
                this.drawBorders = true;
                this.style = style;
            }
            else
            {
                this.topRectangle = rectangle;
                this.style = style;
            }
        }

        /// <inheritdoc/>
        public Microsoft.Xna.Framework.Rectangle Bounds
        {
            get
            {
                if (this.drawBorders)
                {
                    return Microsoft.Xna.Framework.Rectangle.Union(this.topRectangle, Microsoft.Xna.Framework.Rectangle.Union(Microsoft.Xna.Framework.Rectangle.Union(this.topRectangle, this.leftRectangle), Microsoft.Xna.Framework.Rectangle.Union(this.bottomRectangle, this.rightRectangle)));
                }
                else
                {
                    return this.topRectangle;
                }
            }

            set
            {
                if (this.drawBorders)
                {
                    this.SetUpRectangles(Microsoft.Xna.Framework.Rectangle.Union(this.topRectangle, Microsoft.Xna.Framework.Rectangle.Union(Microsoft.Xna.Framework.Rectangle.Union(this.topRectangle, this.leftRectangle), Microsoft.Xna.Framework.Rectangle.Union(this.bottomRectangle, this.rightRectangle))), this.Style.BorderThickness);
                }
                else
                {
                    this.topRectangle = value;
                }
            }
        }

        /// <inheritdoc/>
        public Point Location
        {
            get
            {
                return this.topRectangle.Location;
            }

            set
            {
                if (this.drawBorders)
                {
                    this.SetUpRectangles(
                        Microsoft.Xna.Framework.Rectangle.Union(this.topRectangle, Microsoft.Xna.Framework.Rectangle.Union(Microsoft.Xna.Framework.Rectangle.Union(this.topRectangle, this.leftRectangle), Microsoft.Xna.Framework.Rectangle.Union(this.bottomRectangle, this.rightRectangle))),
                        this.Style.BorderThickness);
                }
                else
                {
                    this.topRectangle.Location = value;
                }
            }
        }

        /// <inheritdoc/>
        public Style Style
        {
            get
            {
                return this.style;
            }

            set
            {
                this.style = value;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="spriteBatch"/> is a null reference.
        /// </exception>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null)
            {
                throw new ArgumentNullException("spriteBatch");
            }

            if (this.drawBorders)
            {
                spriteBatch.Draw(this.style.Texture, this.topRectangle, this.Style.BorderColor);
                spriteBatch.Draw(this.style.Texture, this.bottomRectangle, this.Style.BorderColor);
                spriteBatch.Draw(this.style.Texture, this.leftRectangle, this.Style.BorderColor);
                spriteBatch.Draw(this.style.Texture, this.rightRectangle, this.Style.BorderColor);
                spriteBatch.Draw(this.style.Texture, this.innerRectangle, this.Style.BackColor);
            }
            else
            {
                spriteBatch.Draw(this.style.Texture, this.topRectangle, this.Style.BackColor);
            }
        }

        private void SetUpRectangles(Microsoft.Xna.Framework.Rectangle parent, int borderThickness)
        {
            this.topRectangle = new Microsoft.Xna.Framework.Rectangle(
                parent.X,
                parent.Y,
                parent.Width,
                borderThickness);
            this.bottomRectangle = new Microsoft.Xna.Framework.Rectangle(
                parent.X,
                parent.Y + borderThickness + (parent.Height - borderThickness - borderThickness),
                parent.Width,
                borderThickness);
            this.leftRectangle = new Microsoft.Xna.Framework.Rectangle(
                parent.X,
                parent.Y + borderThickness,
                borderThickness,
                parent.Height - borderThickness - borderThickness);
            this.rightRectangle = new Microsoft.Xna.Framework.Rectangle(
                parent.X + (parent.Width - borderThickness),
                parent.Y + borderThickness,
                borderThickness,
                parent.Height - borderThickness - borderThickness);
            ////this.innerRectangle = new Microsoft.Xna.Framework.Rectangle(
            ////    this.topRectangle.Y + borderThickness,
            ////    this.bottomRectangle.X + borderThickness,
            ////    this.topRectangle.Width - (borderThickness * 2),
            ////    this.leftRectangle.Height);
            this.innerRectangle = new Microsoft.Xna.Framework.Rectangle(
                this.leftRectangle.Width + parent.X,
                this.topRectangle.Height + parent.Y,
                this.topRectangle.Width - (this.leftRectangle.Width + this.rightRectangle.Width),
                this.leftRectangle.Height);
        }
    }
}
