// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultStyles.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms
{
    using System;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using MonoForms.Controls;

    /// <summary>
    /// Provides a default set of <see cref="Style"/>s.
    /// </summary>
    /// <remarks>
    /// The <see cref="DefaultStyles"/> class must be initialized (call <see cref="DefaultStyles.Initialize(GraphicsDevice, SpriteFont, GameWindow)"/> or any other
    /// overload) in order to make a <see cref="Form"/> or other <see cref="VisualControl"/>s work.
    /// A unique <see cref="Style"/> can be passed to every <see cref="VisualControl"/>, but even then, <see cref="DefaultStyles"/> must be initialized.
    /// Changing the properties of the <see cref="DefaultStyles"/> class will change the look of every existing <see cref="VisualControl"/> which does not have its unique <see cref="Style"/>.
    /// <para>
    /// In general, the initialization of <see cref="DefaultStyles"/> is ugly and kind of disturbs the image of XnaForms, but there is no way to
    /// generate a <see cref="Texture2D"/> or a <see cref="SpriteFont"/> programmatically (the former needs a <see cref="GraphicsDevice"/>, the
    /// latter is a content and by definition not to be generated programmatically).
    /// </para>
    /// </remarks>
    /// <example>
    /// To make a <see cref="VisualControl"/> work, <see cref="DefaultStyles"/> must be initialized (before the <see cref="VisualControl"/> is initialized and
    /// only one general initialization is needed).
    /// <code language="C#" title="Initialization of DefaultStyle">
    /// // DefaultStyle can be initialized like this:
    /// DefaultStyle.Initialize(Game.GraphicsDeviceManager.GraphicsDevice, Content.Load&lt;SpriteFont&gt;("YourFont"), Game.Window);
    /// </code>
    /// </example>
    public static class DefaultStyles
    {
        /// <summary>
        /// An <see cref="int"/> indicating the font size for <see cref="StyleType.Action"/>-<see cref="Style"/>s.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal const int ActionFontSize = 25;

        /// <summary>
        /// An <see cref="int"/> indicating the font size for <see cref="StyleType.Active"/>-<see cref="Style"/>s.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal const int ActiveFontSize = 25;

        /// <summary>
        /// An <see cref="int"/> indicating the font size for <see cref="StyleType.Default"/>-<see cref="Style"/>s.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal const int DefaultFontSize = 25;
        private static Style actionStyle;
        private static Style activeStyle;
        private static Style defaultStyle;

        /// <summary>
        /// Occurs when one of the properties of the <see cref="DefaultStyles"/> class was changed.
        /// </summary>
        public static event EventHandler Changed;

        /// <summary>
        /// Gets or sets the <see cref="Style"/> used when an action is initiated.
        /// </summary>
        public static Style Action
        {
            get
            {
                return DefaultStyles.actionStyle;
            }

            set
            {
                DefaultStyles.actionStyle = value;
                DefaultStyles.OnChanged();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Style"/> used when its implementer is activated.
        /// </summary>
        public static Style Active
        {
            get
            {
                return DefaultStyles.activeStyle;
            }

            set
            {
                DefaultStyles.activeStyle = value;
                DefaultStyles.OnChanged();
            }
        }

        /// <summary>
        /// Gets or sets the default <see cref="Style"/>.
        /// </summary>
        public static Style Default
        {
            get
            {
                return DefaultStyles.defaultStyle;
            }

            set
            {
                DefaultStyles.defaultStyle = value;
                DefaultStyles.OnChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether <see cref="DefaultStyles"/> was already initialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static bool IsInitialized { get; private set; }

        /// <summary>
        /// Initializes the default <see cref="Style"/> with the default <see cref="Texture2D"/> and the given objects.
        /// </summary>
        /// <param name="device">
        /// A <see cref="GraphicsDevice"/> used to create default objects.
        /// </param>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> used as default <see cref="SpriteFont"/>.
        /// </param>
        public static void Initialize(GraphicsDevice device, SpriteFont font)
        {
            Texture2D texture = new Texture2D(device, 1, 1);
            texture.SetData<Color>(new[] { Color.White });
            InitializeMembers(font, texture);
        }

        /// <summary>
        /// Initializes the default <see cref="Style"/> with the given objects.
        /// </summary>
        /// <param name="texture">
        /// A <see cref="Texture2D"/> used as default <see cref="Texture2D"/>.
        /// </param>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> used as default <see cref="SpriteFont"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="texture"/> was passed as NULL.
        /// </exception>
        public static void Initialize(Texture2D texture, SpriteFont font)
        {
            if (texture == null)
            {
                throw new ArgumentNullException("texture");
            }

            DefaultStyles.InitializeMembers(font, texture);
        }

        /// <summary>
        /// Resets the members of the <see cref="DefaultStyles"/> to their default value.
        /// </summary>
        public static void Reset()
        {
            DefaultStyles.actionStyle = new Style(
                Color.Blue,
                Color.DarkBlue,
                2,
                null,
                null,
                Color.Black,
                null,
                StyleType.Action);
            DefaultStyles.activeStyle = new Style(
                Color.LightBlue,
                Color.DarkBlue,
                2,
                null,
                null,
                Color.Black,
                null,
                StyleType.Active);
            DefaultStyles.defaultStyle = new Style(
                new Color(240, 240, 240),
                Color.Black,
                2,
                null,
                null,
                Color.Black,
                null,
                StyleType.Default);
            DefaultStyles.OnChanged();
        }

        /// <summary>
        /// Calculates the proper scale to get the desired height out of the height of the <see cref="SpriteFont"/>.
        /// </summary>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> which height should be scaled.
        /// </param>
        /// <param name="desiredHeight">
        /// An <see cref="int"/> indicating the desired height of the <see cref="SpriteFont"/>.
        /// </param>
        /// <returns>
        /// A <see cref="float"/> indicating the scale for the <paramref name="font"/> to fit the <paramref name="desiredHeight"/>.
        /// </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static float CalculateScale(SpriteFont font, int desiredHeight)
        {
            if (font == null)
            {
                throw new ArgumentNullException(nameof(font));
            }

            float fontHeight = font.MeasureString("Test").Y;
            return desiredHeight / fontHeight;
        }

        private static void InitializeMembers(SpriteFont font, Texture2D texture)
        {
            DefaultStyles.actionStyle = new Style(
                Color.Blue,
                Color.DarkBlue,
                2,
                font,
                DefaultStyles.ActionFontSize,
                Color.Black,
                texture,
                StyleType.Action);
            DefaultStyles.activeStyle = new Style(
                Color.LightBlue,
                Color.DarkBlue,
                2,
                font,
                DefaultStyles.ActiveFontSize,
                Color.Black,
                texture,
                StyleType.Active);
            DefaultStyles.defaultStyle = new Style(
                new Color(240, 240, 240),
                Color.Black,
                2,
                font,
                DefaultStyles.DefaultFontSize,
                Color.Black,
                texture,
                StyleType.Default);
            DefaultStyles.IsInitialized = true;
        }

        private static void OnChanged()
        {
            if (DefaultStyles.Changed != null)
            {
                DefaultStyles.Changed(null, EventArgs.Empty);
            }
        }
    }
}
