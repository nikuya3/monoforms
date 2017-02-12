// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Text.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Shapes
{
    using System;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using MonoForms.Core;

    using MonoForms.Core.Extensions;

    /// <summary>
    /// Holds and draws a <see cref="string"/>.
    /// </summary>
    public class Text : IShape
    {
        private Microsoft.Xna.Framework.Rectangle bounds;

        /// <summary>
        /// The <see cref="SpriteEffects"/> used for the text to be drawn.
        /// </summary>
        private SpriteEffects effects;

        /// <summary>
        /// The <see cref="SpriteFont"/> used to draw the <see cref="TextContent"/>.
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// A <see cref="Vector2"/> indicating the origin of the text to be drawn.
        /// </summary>
        private Vector2 origin;

        /// <summary>
        /// A <see cref="float"/> indicating the rotation of the text to be drawn.
        /// </summary>
        private float rotation;

        /// <summary>
        /// A value indicating the drawing scale of the <see cref="TextContent"/>.
        /// </summary>
        private float textScale;

        private RasterizerState rasterizerState;

        private Microsoft.Xna.Framework.Rectangle scissorRectangle;

        private Style style;

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class. Locates this <see cref="Text"/> within the given <see cref="Microsoft.Xna.Framework.Rectangle"/>.
        /// </summary>
        /// <param name="text">
        /// A <see cref="string"/> representing the <see cref="TextContent"/> to be drawn.
        /// </param>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> indicating the font used to draw the <see cref="TextContent"/>.
        /// </param>
        /// <param name="color">
        /// A <see cref="Color"/> indicating the color in which the <see cref="TextContent"/> is drawn.
        /// </param>
        /// <param name="effects">
        /// A <see cref="SpriteEffects"/> indicating the effects used by the <see cref="TextContent"/>.
        /// </param>
        /// <param name="bounds">
        /// A <see cref="Rectangle"/> indicating the boundaries of this <see cref="Text"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="color"/> or <paramref name="font"/> or <paramref name="text"/> is a null reference.
        /// </exception>
        public Text(string text, SpriteFont font, Color color, SpriteEffects effects, Microsoft.Xna.Framework.Rectangle bounds)
        {
            if (color == null)
            {
                throw new ArgumentNullException("color");
            }

            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            ////if (text.IsNullOrEmpty) {
            ////    throw new ArgumentException("text must consist of at least one character.", "text");
            ////}

            this.TextContent = text;
            this.Color = color;
            this.effects = effects;
            this.font = font;
            this.bounds = bounds;
            this.AdjustText(bounds);
            this.origin = Vector2.Zero;
            this.rotation = 0F;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class. Locates this <see cref="Text"/> with the given values.
        /// </summary>
        /// <param name="text">
        /// A <see cref="string"/> representing the <see cref="TextContent"/> to be drawn.
        /// </param>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> indicating the font used to draw the <see cref="TextContent"/>.
        /// </param>
        /// <param name="color">
        /// A <see cref="Color"/> indicating the color in which the <see cref="TextContent"/> is drawn.
        /// </param>
        /// <param name="effects">
        /// A <see cref="SpriteEffects"/> indicating the effects used by the <see cref="TextContent"/>.
        /// </param>
        /// <param name="position">
        /// A <see cref="Vector2"/> indicating the position of this <see cref="Text"/>.
        /// </param>
        /// <param name="scale">
        /// A <see cref="float"/> indicating a multiplier for the drawing scale of the <see cref="TextContent"/>.
        /// </param>
        /// <param name="rotation">
        /// A <see cref="float"/> indicating the angles to this <see cref="Text"/> about its angle.
        /// </param>
        /// <param name="origin">
        /// A <see cref="Vector2"/> indicating the origin of this <see cref="Text"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="color"/> and/or <see cref="font"/> are NULL.
        /// </exception>
        public Text(string text, SpriteFont font, Color color, SpriteEffects effects, Vector2 position, float scale, float rotation, Vector2 origin)
        {
            if (color == null)
            {
                throw new ArgumentNullException("color");
            }

            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            this.TextContent = text;
            this.font = font;
            this.Color = color;
            this.effects = effects;
            Size measuredText = font.MeasureStringSize(this.TextContent);
            this.bounds = new Microsoft.Xna.Framework.Rectangle((int)position.X, (int)position.Y, (int)(measuredText.Width * scale), (int)(measuredText.Height * scale));
            this.textScale = scale;
            this.rotation = rotation;
            this.origin = origin;
            ////measuredText = new Vector2(measuredText.X * scale, measuredText.Y * scale);
            ////this.Position = new Microsoft.Xna.Framework.Rectangle((int)position.X, (int)position.Y, (int)measuredText.X, (int)measuredText.Y);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class. Locates this <see cref="Text"/> with the given values. Cuts this instance of <see cref="Text"/> off if it expands the boundaries of the given scissor rectangle.
        /// </summary>
        /// <param name="text">
        /// A <see cref="string"/> representing the <see cref="TextContent"/> to be drawn.
        /// </param>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> indicating the font used to draw the <see cref="TextContent"/>.
        /// </param>
        /// <param name="color">
        /// A <see cref="Color"/> indicating the color in which the <see cref="TextContent"/> is drawn.
        /// </param>
        /// <param name="effects">
        /// A <see cref="SpriteEffects"/> indicating the effects used by the <see cref="TextContent"/>.
        /// </param>
        /// <param name="position">
        /// A <see cref="Vector2"/> indicating the position of this <see cref="Text"/>.
        /// </param>
        /// <param name="scale">
        /// A <see cref="float"/> indicating a multiplier for the drawing scale of the <see cref="TextContent"/>.
        /// </param>
        /// <param name="rotation">
        /// A <see cref="float"/> indicating the angles to this <see cref="Text"/> about its angle.
        /// </param>
        /// <param name="origin">
        /// A <see cref="Vector2"/> indicating the origin of this <see cref="Text"/>.
        /// </param>
        /// <param name="scissorRectangle">
        /// An instance of <see cref="Microsoft.Xna.Framework.Rectangle"/> indicating the boundaries where this instance of <see cref="Text"/> should be drawn.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="color"/> and/or <see cref="font"/> are NULL.
        /// </exception>
        public Text(string text, SpriteFont font, Color color, SpriteEffects effects, Vector2 position, float scale, float rotation, Vector2 origin, Microsoft.Xna.Framework.Rectangle scissorRectangle)
        {
            if (color == null)
            {
                throw new ArgumentNullException("color");
            }

            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            this.TextContent = text;
            this.font = font;
            this.Color = color;
            this.effects = effects;
            Size measuredText = font.MeasureStringSize(this.TextContent);
            this.bounds = new Microsoft.Xna.Framework.Rectangle((int)position.X, (int)position.Y, (int)(measuredText.Width * scale), (int)(measuredText.Height * scale));
            this.textScale = scale;
            this.rotation = rotation;
            this.origin = origin;
            this.rasterizerState = new RasterizerState() { ScissorTestEnable = true };
            this.scissorRectangle = scissorRectangle;
            ////measuredText = new Vector2(measuredText.X * scale, measuredText.Y * scale);
            ////this.Position = new Microsoft.Xna.Framework.Rectangle((int)position.X, (int)position.Y, (int)measuredText.X, (int)measuredText.Y);
        }

        /// <inheritdoc/>
        public Microsoft.Xna.Framework.Rectangle Bounds
        {
            get
            {
                return this.bounds;
            }

            set
            {
                this.bounds = value;
                this.AdjustText(value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color"/> in which the text is drawn.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="Point"/> indicating the position of this <see cref="Text"/>.
        /// </summary>
        public Point Location
        {
            get
            {
                return this.bounds.Location;
            }

            set
            {
                this.bounds.Location = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> to be drawn.
        /// </summary>
        public string TextContent { get; set; }

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

            if (this.rasterizerState != null)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, this.rasterizerState);
                Microsoft.Xna.Framework.Rectangle preRectangle = spriteBatch.GraphicsDevice.ScissorRectangle;
                if (preRectangle.Contains(this.scissorRectangle))
                {
                    spriteBatch.GraphicsDevice.ScissorRectangle = this.scissorRectangle;
                }

                spriteBatch.DrawString(
                this.font,
                this.TextContent,
                new Vector2(this.bounds.Location.X, this.bounds.Location.Y),
                this.Color,
                this.rotation,
                this.origin,
                this.textScale,
                this.effects,
                0F);
                spriteBatch.GraphicsDevice.ScissorRectangle = preRectangle;
            }
            else
            {
                spriteBatch.DrawString(
                    this.font,
                    this.TextContent,
                    new Vector2(this.bounds.Location.X, this.bounds.Location.Y),
                    this.Color,
                    this.rotation,
                    this.origin,
                    this.textScale,
                    this.effects,
                    0F);
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.TextContent;
        }

        /// <summary>
        /// Calculates a <see cref="Vector2"/> indicating the position where a given <see cref="string"/> should be drawn in a given <see cref="Rectangle"/> with the given <see cref="SpriteFont"/>.
        /// </summary>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> to be used to calculate the position of the <paramref name="text"/>.
        /// </param>
        /// <param name="text">
        /// A <see cref="string"/> indicating the text to be used to calculate the position.
        /// </param>
        /// <param name="boundaries">
        /// A <see cref="Rectangle"/> indicating the boundaries used to calculate the position of the <paramref name="text"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Vector2"/> indicating the position where a given <see cref="string"/> should be drawn.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="font"/> or <paramref name="text"/> is a null reference.
        /// </exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static Vector2 CenterString(SpriteFont font, string text, Microsoft.Xna.Framework.Rectangle boundaries)
        {
            return Text.CenterString(font, text, boundaries, 1.0f);
        }

        /// <summary>
        /// Calculates a <see cref="Vector2"/> indicating the position where a given <see cref="string"/> should be drawn in a given <see cref="Rectangle"/> with the given <see cref="SpriteFont"/> considering a given drawing scale.
        /// </summary>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> to be used to calculate the position of the <paramref name="text"/>.
        /// </param>
        /// <param name="text">
        /// A <see cref="string"/> indicating the text to be used to calculate the position.
        /// </param>
        /// <param name="boundaries">
        /// A <see cref="Rectangle"/> indicating the boundaries used to calculate the position of the <paramref name="text"/>.
        /// </param>
        /// <returns>
        /// <param name="scale">
        /// A <see cref="float"/> indicating the drawing scale to be considered.
        /// </param>
        /// A <see cref="Vector2"/> indicating the position where a given <see cref="string"/> should be drawn.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="font"/> or <paramref name="text"/> is a null reference.
        /// </exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static Vector2 CenterString(SpriteFont font, string text, Microsoft.Xna.Framework.Rectangle boundaries, float scale)
        {
            if (font == null)
            {
                throw new ArgumentNullException(nameof(font));
            }

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            Size size = font.MeasureStringSize(text);
            Vector2 position = new Vector2(((boundaries.Width - (size.Width * scale)) / 2) + boundaries.X, ((boundaries.Height - (size.Height * scale)) / 2) + boundaries.Y);
            return position;
        }

        /// <summary>
        /// Adjusts the members of this <see cref="Text"/> to fit the given <see cref="Microsoft.Xna.Framework.Rectangle"/>.
        /// </summary>
        /// <param name="boundaries">
        /// A <see cref="Rectangle"/> which indicates the new boundaries of this <see cref="Text"/>.
        /// </param>
        private void AdjustText(Microsoft.Xna.Framework.Rectangle boundaries)
        {
            Vector2 size = this.font.MeasureString(this.TextContent);
            this.textScale = Math.Min(boundaries.Width / size.X, boundaries.Height / size.Y);
            int textWidth = (int)Math.Round(size.X * this.textScale);
            int textHeight = (int)Math.Round(size.Y * this.textScale);
            this.bounds.Location = new Point(
                ((boundaries.Width - textWidth) / 2) + boundaries.X,
                ((boundaries.Height - textHeight) / 2) + boundaries.Y);
        }
    }
}
