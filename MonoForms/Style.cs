// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Style.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using MonoForms.Controls;
    using MonoForms.Shapes;

    /// <summary>
    /// Provides a set of values which is to be consistently used.
    /// </summary>
    /// <remarks>
    /// A <see cref="Style"/> contains a variety of values which can be passed to an instance of <see cref="VisualControl"/>.
    /// The values can not be forced upon this instance of <see cref="VisualControl"/>, it is more like a recommendation for the graphical representation of the <see cref="IShape"/>s of the instance of <see cref="VisualControl"/>.
    /// A great part of the default <see cref="VisualControl"/>s uses the style guideline where it is possible, but at some point also differs from it.
    /// Therefore, also custom <see cref="VisualControl"/>s can not be forced to follow the style guidelines, the instance of<see cref="Style"/> can only provide a recommendation.
    /// </remarks>
    /// <example>
    /// Generating an instance of <see cref="Style"/> is very easy. The initialization is made by the constructor, the thought is to pass the constructed
    /// reference to every instance of <see cref="VisualControl"/> which should follow this instance of <see cref="Style"/>.
    /// <code language="C#" title="Style example">
    /// // Calling the default constructor will generate a Style using the default values of the given StyleType.
    /// Style defaultStyle = new Style(StyleType.Default);
    /// // Calling the constructor overload will give the possibility to provide custom style values. If not all values differ from the default style value, passing a null reference will be handled as to use the default value from the given StyleType of the parameter which was passed as null reference.
    /// Style customStyle = new Style(Color.Red, Color.Blue, null, null, null, Color.Yellow, null, StyleType.Default);
    /// // In this case, only the properties Style.ForeColor, Style.BackColor and Style.BorderColor are affected. The other properties contain the values of DefaultStyle.
    /// </code>
    /// </example>
    public class Style : INotifyPropertyChanged
    {
        private Color backColor;
        private Color borderColor;
        private int borderThickness;
        private SpriteFont font;
        private Color foreColor;
        private float textScale;
        private Texture2D texture;

        /// <summary>
        /// Initializes a new instance of the <see cref="Style"/> class, which uses the default values of the given <see cref="StyleType"/>.
        /// </summary>
        /// <param name="type">
        /// The <see cref="StyleType"/> of the new instance of <see cref="Style"/>.
        /// </param>
        public Style(StyleType type)
        {
            switch (type)
            {
                case StyleType.Action:
                    this.SetActionStyle();
                    break;
                case StyleType.Active:
                    this.SetActiveStyle();
                    break;
                case StyleType.Default:
                    this.SetDefaultStyle();
                    break;
            }

            this.StyleType = type;
            DefaultStyles.Changed += this.DefaultStyle_Changed;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Style"/> class with the specified values.
        /// Pass <see langword="null"/> for the parameters which should use the default value of the given <see cref="StyleType"/>.
        /// </summary>
        /// <param name="backColor">
        /// A <see cref="Color"/> indicating the color used to draw background layers.
        /// </param>
        /// <param name="borderColor">
        /// A <see cref="Color"/> indicating the color used to border.
        /// </param>
        /// <param name="borderThickness">
        /// An <see cref="int"/> indicating the thickness of the border to be drawn.
        /// </param>
        /// <param name="font">
        /// The <see cref="SpriteFont"/> used to draw text.
        /// </param>
        /// <param name="fontSize">
        /// An <see cref="int"/> indicating the size of the passed <paramref name="font"/>.
        /// </param>
        /// <param name="foreColor">
        /// A <see cref="Color"/> indicating the color used to draw front layers.
        /// </param>
        /// <param name="texture">
        /// The <see cref="Texture2D"/> used to draw.
        /// </param>
        /// <param name="type">
        /// A <see cref="StyleType"/> indicating the <see cref="StyleType"/> of the <see cref="Style"/>.
        /// </param>
        public Style(
            Color? backColor,
            Color? borderColor,
            int? borderThickness,
            SpriteFont font,
            int? fontSize,
            Color? foreColor,
            Texture2D texture,
            StyleType type)
        {
            if (borderThickness < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(borderThickness),  "borderThickness must be greater than or equals 0.");
            }

            this.SetBackColor(backColor, type);
            this.SetBorderColor(borderColor, type);
            this.SetBorderThickness(borderThickness, type);
            this.SetFont(font, type);
            this.SetFontSize(fontSize, type);
            this.SetForeColor(foreColor, type);
            this.SetTexture(texture, type);
            this.StyleType = type;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a <see cref="Color"/> indicating the color used to draw background layers.
        /// </summary>
        public Color BackColor
        {
            get
            {
                return this.backColor;
            }

            set
            {
                this.backColor = value;
                this.OnPropertyChanged("BackColor");
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Color"/> indicating the color used to draw border.
        /// </summary>
        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }

            set
            {
                this.borderColor = value;
                this.OnPropertyChanged("BorderColor");
            }
        }

        /// <summary>
        /// Gets or sets an <see cref="int"/> indicating the thickness of the border to be drawn.
        /// </summary>
        public int BorderThickness
        {
            get
            {
                return this.borderThickness;
            }

            set
            {
                this.borderThickness = value;
                this.OnPropertyChanged("BorderThickness");
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="SpriteFont"/> used to draw <see cref="string"/>s.
        /// </summary>
        public SpriteFont Font
        {
            get
            {
                return this.font;
            }

            set
            {
                this.font = value;
                this.OnPropertyChanged("Font");
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Color"/> indicating the color used to draw foreground layers.
        /// </summary>
        public Color ForeColor
        {
            get
            {
                return this.foreColor;
            }

            set
            {
                this.foreColor = value;
                this.OnPropertyChanged("ForeColor");
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="float"/> indicating the scale used to draw <see cref="string"/>s.
        /// </summary>
        public float TextScale
        {
            get
            {
                return this.textScale;
            }

            set
            {
                this.textScale = value;
                this.OnPropertyChanged("TextScale");
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Texture2D"/> used to draw.
        /// </summary>
        public Texture2D Texture
        {
            get
            {
                return this.texture;
            }

            set
            {
                this.texture = value;
                this.OnPropertyChanged("Texture");
            }
        }

        /// <summary>
        /// Gets the <see cref="StyleType"/> of this instance of <see cref="Style"/>.
        /// </summary>
        public StyleType StyleType { get; private set; }

        private void DefaultStyle_Changed(object sender, EventArgs e)
        {
            this.SetDefaultStyle();
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void SetActionStyle()
        {
            this.ForeColor = DefaultStyles.Action.ForeColor;
            this.BackColor = DefaultStyles.Action.BackColor;
            this.BorderColor = DefaultStyles.Action.BorderColor;
            this.Font = DefaultStyles.Action.Font;
            this.TextScale = DefaultStyles.CalculateScale(this.Font, DefaultStyles.DefaultFontSize);
            this.BorderThickness = DefaultStyles.Action.BorderThickness;
            this.Texture = DefaultStyles.Action.Texture;
        }

        private void SetActiveStyle()
        {
            this.ForeColor = DefaultStyles.Active.ForeColor;
            this.BackColor = DefaultStyles.Active.BackColor;
            this.BorderColor = DefaultStyles.Active.BorderColor;
            this.Font = DefaultStyles.Active.Font;
            this.TextScale = DefaultStyles.CalculateScale(this.Font, DefaultStyles.DefaultFontSize);
            this.BorderThickness = DefaultStyles.Active.BorderThickness;
            this.Texture = DefaultStyles.Active.Texture;
        }

        private void SetBackColor(Color? value, StyleType type)
        {
            if (value.HasValue)
            {
                this.backColor = value.Value;
            }
            else
            {
                switch (type)
                {
                    case StyleType.Action:
                        this.backColor = DefaultStyles.Action.BackColor;
                        break;
                    case StyleType.Active:
                        this.backColor = DefaultStyles.Active.BackColor;
                        break;
                    case StyleType.Default:
                        this.backColor = DefaultStyles.Default.BackColor;
                        break;
                }
            }
        }

        private void SetBorderThickness(int? value, StyleType type)
        {
            if (value.HasValue)
            {
                this.borderThickness = value.Value;
            }
            else
            {
                switch (type)
                {
                    case StyleType.Action:
                        this.borderThickness = DefaultStyles.Action.BorderThickness;
                        break;
                    case StyleType.Active:
                        this.borderThickness = DefaultStyles.Active.BorderThickness;
                        break;
                    case StyleType.Default:
                        this.borderThickness = DefaultStyles.Default.BorderThickness;
                        break;
                }
            }
        }

        private void SetBorderColor(Color? value, StyleType type)
        {
            if (value.HasValue)
            {
                this.BorderColor = value.Value;
            }
            else
            {
                switch (type)
                {
                    case StyleType.Action:
                        this.borderColor = DefaultStyles.Action.BorderColor;
                        break;
                    case StyleType.Active:
                        this.borderColor = DefaultStyles.Active.BorderColor;
                        break;
                    case StyleType.Default:
                        this.borderColor = DefaultStyles.Default.BorderColor;
                        break;
                }
            }
        }

        private void SetDefaultStyle()
        {
            this.ForeColor = DefaultStyles.Default.ForeColor;
            this.BackColor = DefaultStyles.Default.BackColor;
            this.BorderColor = DefaultStyles.Default.BorderColor;
            this.Font = DefaultStyles.Default.Font;
            this.TextScale = DefaultStyles.CalculateScale(this.Font, DefaultStyles.DefaultFontSize);
            this.BorderThickness = DefaultStyles.Default.BorderThickness;
            this.Texture = DefaultStyles.Default.Texture;
        }

        private void SetFont(SpriteFont font, StyleType type)
        {
            if (font == null)
            {
                switch (type)
                {
                    case StyleType.Action:
                        this.font = DefaultStyles.Action.Font;
                        break;
                    case StyleType.Active:
                        this.font = DefaultStyles.Active.Font;
                        break;
                    case StyleType.Default:
                        this.font = DefaultStyles.Default.Font;
                        break;
                }
            }
            else
            {
                this.Font = font;
            }
        }

        private void SetFontSize(int? fontSize, StyleType type)
        {
            if (fontSize == null)
            {
                switch (type)
                {
                    case StyleType.Action:
                        this.TextScale = DefaultStyles.CalculateScale(this.Font, DefaultStyles.ActionFontSize);
                        break;
                    case StyleType.Active:
                        this.TextScale = DefaultStyles.CalculateScale(this.Font, DefaultStyles.ActiveFontSize);
                        break;
                    case StyleType.Default:
                        this.TextScale = DefaultStyles.CalculateScale(this.Font, DefaultStyles.DefaultFontSize);
                        break;
                }
            }
            else
            {
                this.TextScale = DefaultStyles.CalculateScale(this.Font, fontSize.Value);
            }
        }

        private void SetForeColor(Color? foreColor, StyleType type)
        {
            if (foreColor.HasValue)
            {
                this.foreColor = foreColor.Value;
            }
            else
            {
                switch (type)
                {
                    case StyleType.Action:
                        this.foreColor = DefaultStyles.Action.ForeColor;
                        break;
                    case StyleType.Active:
                        this.foreColor = DefaultStyles.Active.ForeColor;
                        break;
                    case StyleType.Default:
                        this.foreColor = DefaultStyles.Default.ForeColor;
                        break;
                }
            }
        }

        private void SetTexture(Texture2D texture, StyleType type)
        {
            if (texture == null)
            {
                switch (type)
                {
                    case StyleType.Action:
                        this.texture = DefaultStyles.Action.Texture;
                        break;
                    case StyleType.Active:
                        this.texture = DefaultStyles.Active.Texture;
                        break;
                    case StyleType.Default:
                        this.texture = DefaultStyles.Default.Texture;
                        break;
                }
            }
            else
            {
                this.texture = texture;
            }
        }
    }
}
