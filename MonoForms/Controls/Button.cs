// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Button.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using MonoForms.Core;
    using MonoForms.Shapes;

    /// <summary>
    /// Represents a standard button control.
    /// </summary>
    public class Button : VisualControl
    {
        private const string DefaultText = "Button";
        private const int DefaultHeight = 30;
        private const int DefaultWidth = 120;
        private List<IShape> geometryContent;
        private Text text;
        private List<Texture2D> textureContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        public Button()
        {
            this.InitializeMembers();
        }

        /// <inheritdoc/>
        public override object Content
        {
            get
            {
                return this.text.TextContent;
            }

            set
            {
                if (value is Texture2D)
                {
                    this.SetTextureContent(value as Texture2D);
                }
                else if (value is IShape)
                {
                    this.SetGeometryContent(value as IShape);
                }
                else if (value is IEnumerable && !(value is IEnumerable<char>))
                {
                    IEnumerable enumarable = value as IEnumerable;
                    foreach (var item in enumarable)
                    {
                        if (item is IShape)
                        {
                            this.SetGeometryContent(value as IShape);
                        }

                        if (item is Texture2D)
                        {
                            this.SetTextureContent(value as Texture2D);
                        }
                    }
                }
                else if (value != null)
                {
                    this.SetStringContent(value.ToString());
                }
            }
        }

        /// <inheritdoc/>
        protected override Microsoft.Xna.Framework.Rectangle WholeRectangle { get; set; }

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Manager.SpriteBatch.Begin(true);
            foreach (Texture2D texture in this.textureContent)
            {
                Manager.SpriteBatch.SpriteBatch.Draw(texture, this.WholeRectangle, Color.White);
            }

            foreach (IShape geometry in this.geometryContent)
            {
                geometry.Draw(Manager.SpriteBatch.SpriteBatch);
            }

            Manager.SpriteBatch.End(true);
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            this.InitializeMembers();
        }
        
        /// <inheritdoc/>
        protected override void InitializeShapes()
        {
            this.Shapes.Clear();
            this.Shapes.Add(new MonoForms.Shapes.Rectangle(this.WholeRectangle, this.Style));
            string textContent = this.text == null ? Button.DefaultText : this.text.TextContent;

            this.text = new Text(
                textContent,
                this.Style.Font,
                this.Style.ForeColor,
                SpriteEffects.None,
                Text.CenterString(this.Style.Font, textContent, this.WholeRectangle, this.Style.TextScale),
                this.Style.TextScale,
                0F,
                Vector2.Zero);
            this.Shapes.Add(this.text);
        }

        /// <summary>
        /// Gets an array of colors for a Top Down Gradient.
        /// </summary>
        /// <param name="width">
        /// The width of the color array.
        /// </param>
        /// <param name="height">
        /// The height of the color array.
        /// </param>
        /// <returns>
        /// Color Array.
        /// </returns>
        [Obsolete]
        private static Color[] GetGradientColors(int width, int height)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "width must be greater than or equals 0.");
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "height must be greater than or equals 0.");
            }

            Color[] result = new Color[width * height];
            float increment = (float)255 / result.Length;

            for (int i = 0; i < result.Length; i++)
            {
                var color = (int)(increment * -(i - 255));
                result[i] = new Color(color, color, color, 0.3F);
            }

            return result;
        }

        private void InitializeMembers()
        {
            base.Initialize();
            this.geometryContent = new List<IShape>();
            this.textureContent = new List<Texture2D>();
            this.WholeRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, Button.DefaultWidth, Button.DefaultHeight);
            if (!DefaultStyles.IsInitialized)
            {
                return;
            }

            this.DefaultStyle = new Style(Color.LightGray, null, null, null, null, null, null, StyleType.Default);
            this.Style = new Style(Color.LightGray, null, null, null, null, null, null, StyleType.Default);
            this.Update(new GameTime());
        }

        private void SetGeometryContent(IShape geometry)
        {
            this.geometryContent.Add(geometry);
            this.text.TextContent = string.Empty;
            this.textureContent.Clear();
        }

        private void SetStringContent(string value)
        {
            this.text.TextContent = value;
            Vector2 newPosition = Text.CenterString(this.Style.Font, value, this.WholeRectangle, this.Style.TextScale);
            this.text.Location = new Point((int)newPosition.X, (int)newPosition.Y);
        }

        private void SetTextureContent(Texture2D texture)
        {
            this.textureContent.Add(texture);
            this.text.TextContent = string.Empty;
            this.geometryContent.Clear();
        }
    }
}
