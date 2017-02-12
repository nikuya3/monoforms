// --------------------------------------------------------------------------------------------------------------------
// <copyright file="picturebox.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Controls
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using MonoForms.Core;

    /// <summary>
    /// Represents a control used to display an image.
    /// </summary>
    public class PictureBox : VisualControl
    {
        private Texture2D textureContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureBox"/> class.
        /// </summary>
        public PictureBox()
        {
            this.WholeRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, 100, 100);
            this.InitializeMembers();
        }

        /// <inheritdoc/>
        public override object Content
        {
            get
            {
                return base.Content;
            }

            set
            {
                base.Content = value;
                if (value is Texture2D)
                {
                    Texture2D texture = value as Texture2D;
                    this.textureContent = texture;
                    this.ActionStyle.Texture = texture;
                    this.ActiveStyle.Texture = texture;
                    this.DefaultStyle.Texture = texture;
                    this.Style.Texture = texture;
                }
            }
        }

        /// <inheritdoc/>
        protected override Microsoft.Xna.Framework.Rectangle WholeRectangle { get; set; }

        /// <inheritdoc/>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            Manager.SpriteBatch.Begin(true);
            Manager.SpriteBatch.SpriteBatch.Draw(this.textureContent, this.WholeRectangle, Color.White);
            Manager.SpriteBatch.End(true);
        }

        /// <inheritdoc/>
        protected override void InitializeShapes()
        {
            this.textureContent = this.Style.Texture;
        }

        private void InitializeMembers()
        {
            base.Initialize();
            this.Update(new GameTime());
        }
    }
}
