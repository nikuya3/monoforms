// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Form.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Controls
{
    using System;
    using System.ComponentModel;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    using MonoForms.Core;
    using MonoForms.Input;
    using MonoForms.Shapes;

    /// <summary>
    /// Represents an interface of <see cref="Control"/>s.
    /// </summary>
    /// <remarks>
    /// A <see cref="Form"/> holds, updates and draws every <see cref="Control"/> added to it. However, it also provides useful functions itself.
    /// It can be commonly thought of as an equivalent to <a href="http://msdn.microsoft.com/en-us/library/system.windows.forms.form%28v=vs.110%29.aspx">System.Windows.Forms.Form</a>.
    /// </remarks>
    /// <example>
    /// It also provides a simple way to set up a user interface of controls in XNA:
    /// <code language="C#" title="Simple example of using it:">
    /// // Manager.Initialize(GraphicsDevice, SpriteFont) must be called once in an application.
    /// Manager.Initialize(Game.GraphicsDeviceManager.GraphicsDevice, Content.Load&lt;SpriteFont&gt;("MyFont"));
    /// Form form = new Form();
    /// form.Content = "Form";
    /// Button button = new Button();
    /// button.Content = "Button";
    /// form.Controls.Add(button);
    /// form.Visible = true;
    /// form.Update(GameTime);
    /// form.Draw(GameTime);
    /// </code>
    /// </example>
    public class Form : ContainerControl
    {
        private const string DefaultText = "Form";
        private const int TitleSymbolsWidth = 100;
        private const int Height = 300;
        private const int TitleHeight = 30;
        private const int Width = 300;
        private Button closeButton;
        private Button minimizeButton;
        private Button maximizeButton;
        private Microsoft.Xna.Framework.Rectangle nextScreenRectangle;
        private Microsoft.Xna.Framework.Rectangle normalBounds;
        private MouseState previousMouseState;
        private Microsoft.Xna.Framework.Rectangle titleRectangle;
        private Text titleText;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form"/> class.
        /// </summary>
        public Form()
        {
            this.WholeRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, Form.Width, Form.Height);
            this.nextScreenRectangle = this.ScreenRectangle;
            this.Visible = false;
            this.IsMoveable = true;
            this.InitializeMembers();
        }

        /// <summary>
        /// Occurs when this instance of <see cref="Form"/> is being closed.
        /// </summary>
        public event EventHandler<EventArgs> Closed;

        /// <summary>
        /// Occurs when this instance of <see cref="Form"/> is attempted to being closed.
        /// </summary>
        public event EventHandler<CancelEventArgs> Closing;

        /// <summary>
        /// Occurs when this instance of <see cref="Form"/> is being minimized.
        /// </summary>
        public event EventHandler<EventArgs> Minimized;

        /// <summary>
        /// Occurs when this instance of <see cref="Form"/> is attempted to being minimized.
        /// </summary>
        public event EventHandler<CancelEventArgs> Minimizing;

        /// <summary>
        /// Occurs when this instance of <see cref="Form"/> is being maximized.
        /// </summary>
        public event EventHandler<EventArgs> Maximized;

        /// <summary>
        /// Occurs when this instance of <see cref="Form"/> is attempted to being maximized.
        /// </summary>
        public event EventHandler<CancelEventArgs> Maximizing;

        /// <summary>
        /// Gets a <see cref="Microsoft.Xna.Framework.Rectangle"/> indicating the boundaries of the caption bar of this <see cref="Form"/>.
        /// </summary>
        public Microsoft.Xna.Framework.Rectangle CaptionBar => this.titleRectangle;

        /// <inheritdoc/>
        public override object Content
        {
            get
            {
                return this.titleText.TextContent;
            }

            set
            {
                this.titleText.TextContent = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Form"/> can be moved be the user.
        /// </summary>
        public bool IsMoveable { get; set; }

        /// <inheritdoc/>
        public override Microsoft.Xna.Framework.Rectangle ScreenRectangle
        {
            get
            {
                return base.ScreenRectangle;
            }

            set
            {
                base.ScreenRectangle = value;
                this.nextScreenRectangle = value;
            }
        }

        /// <inheritdoc/>
        protected override Microsoft.Xna.Framework.Rectangle WholeRectangle { get; set; }

        /// <inheritdoc/>
        public override void Initialize()
        {
            this.InitializeMembers();
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.nextScreenRectangle != this.ScreenRectangle)
            {
                this.ScreenRectangle = this.nextScreenRectangle;
            }

            if (DefaultStyles.IsInitialized)
            {
                MouseState mouseState = ComponentMouse.GetMouseState(gameTime);
                if (this.IsMoveable && ComponentMouse.IsMouseDown)
                {
                    this.CheckMouse(mouseState);
                }
                
                this.previousMouseState = mouseState;
            }
        }

        /// <inheritdoc/>
        protected override void InitializeShapes()
        {
            this.Shapes.Clear();
            this.PrivateControls.Clear();

            this.titleRectangle = new Microsoft.Xna.Framework.Rectangle(
                this.WholeRectangle.X,
                this.WholeRectangle.Y,
                this.WholeRectangle.Width,
                Form.TitleHeight);
            Microsoft.Xna.Framework.Rectangle contentRectangle = new Microsoft.Xna.Framework.Rectangle(
                this.WholeRectangle.X,
                this.WholeRectangle.Y + this.titleRectangle.Height,
                this.WholeRectangle.Width,
                this.WholeRectangle.Height - this.titleRectangle.Height);
            Microsoft.Xna.Framework.Rectangle symbols = new Microsoft.Xna.Framework.Rectangle(
                ((this.titleRectangle.Width - Form.TitleSymbolsWidth) - this.Style.BorderThickness) + this.titleRectangle.X,
                this.titleRectangle.Y + this.Style.BorderThickness,
                Form.TitleSymbolsWidth,
                this.titleRectangle.Height - this.Style.BorderThickness); // times two
            Microsoft.Xna.Framework.Rectangle logicalMinimizeRectangle = new Microsoft.Xna.Framework.Rectangle(
                symbols.X,
                symbols.Y,
                symbols.Width / 3,
                symbols.Height);
            Microsoft.Xna.Framework.Rectangle visualMinimizeRectangle = new Microsoft.Xna.Framework.Rectangle(
                logicalMinimizeRectangle.X + (logicalMinimizeRectangle.Width / 4), // no symbols
                (logicalMinimizeRectangle.Height / 2) + logicalMinimizeRectangle.Y,
                logicalMinimizeRectangle.Width / 2,
                this.Style.BorderThickness * 2);
            Microsoft.Xna.Framework.Rectangle maximizeRectangle = new Microsoft.Xna.Framework.Rectangle(
                logicalMinimizeRectangle.X + logicalMinimizeRectangle.Width,
                symbols.Y,
                symbols.Width / 3,
                symbols.Height);
            Microsoft.Xna.Framework.Rectangle closeRectangle = new Microsoft.Xna.Framework.Rectangle(
                maximizeRectangle.X + maximizeRectangle.Width + (maximizeRectangle.Width / 4),
                symbols.Y + (symbols.Height / 4),
                (symbols.Width / 3) / 2,
                symbols.Height / 2);

            string textContent = this.titleText == null ? Form.DefaultText : this.titleText.TextContent;

            this.titleText = new Text(
                textContent,
                this.Style.Font,
                Color.White,
                SpriteEffects.None,
                new Vector2(this.titleRectangle.Location.X, this.titleRectangle.Location.Y + (this.titleRectangle.Height / 4)),
                this.Style.TextScale,
                0F,
                Vector2.Zero);

            this.closeButton.ScreenRectangle = closeRectangle;
            this.closeButton.Content = string.Empty;
            this.closeButton.DefaultStyle.BackColor = Color.White;
            this.closeButton.DefaultStyle.BorderThickness = 0;
            this.closeButton.Style = this.closeButton.DefaultStyle;
            this.closeButton.ActiveStyle.BackColor = Color.Red;
            this.closeButton.ActiveStyle.BorderColor = Color.White;
            this.closeButton.ActionStyle.BackColor = Color.DarkRed;
            this.closeButton.ActionStyle.BorderColor = Color.DarkRed;

            this.minimizeButton.ScreenRectangle = visualMinimizeRectangle;
            this.minimizeButton.Content = string.Empty;
            this.minimizeButton.DefaultStyle = new Style(Color.White, null, 0, null, null, null, null, StyleType.Default);
            this.minimizeButton.Style = this.minimizeButton.DefaultStyle;
            this.minimizeButton.ActionStyle.BorderThickness = 0;
            this.minimizeButton.ActiveStyle.BorderThickness = 0;

            this.maximizeButton.ScreenRectangle = maximizeRectangle;
            this.maximizeButton.Content = string.Empty;
            this.maximizeButton.DefaultStyle = new Style(Color.Transparent, Color.White, this.Style.BorderThickness * 2, null, null, null, null, StyleType.Default);
            this.maximizeButton.Style = this.maximizeButton.DefaultStyle;
            this.maximizeButton.ActiveStyle.BorderColor = this.minimizeButton.ActiveStyle.BackColor;
            this.maximizeButton.ActiveStyle.BackColor = Color.Transparent;
            this.maximizeButton.ActiveStyle.BorderThickness = this.maximizeButton.DefaultStyle.BorderThickness;
            this.maximizeButton.ActionStyle.BorderColor = this.minimizeButton.ActionStyle.BackColor;
            this.maximizeButton.ActionStyle.BackColor = Color.Transparent;
            this.maximizeButton.ActionStyle.BorderThickness = this.maximizeButton.DefaultStyle.BorderThickness;

            this.Shapes.Add(new MonoForms.Shapes.Rectangle(
                this.titleRectangle,
                this.Style.BorderColor,
                this.Style.Texture));
            this.Shapes.Add(new MonoForms.Shapes.Rectangle(
                contentRectangle,
                this.Style.BorderThickness,
                this.Style.BorderColor,
                this.Style.BackColor,
                this.Style.Texture));
            this.Shapes.Add(this.titleText);

            this.PrivateControls.Add(this.closeButton);
            this.PrivateControls.Add(this.minimizeButton);
            this.PrivateControls.Add(this.maximizeButton);
        }

        private void CheckMouse(MouseState state)
        {
            if (!this.titleRectangle.Contains(state.X, state.Y) && ComponentMouse.Captured != this)
            {
                return;
            }

            if (state.LeftButton == ButtonState.Pressed && this.previousMouseState.LeftButton == ButtonState.Pressed)
            {
                var toChange = new Point(state.X - this.previousMouseState.X, state.Y - this.previousMouseState.Y);
                this.ScreenLocation = new Point(this.ScreenLocation.X + toChange.X, this.ScreenLocation.Y + toChange.Y);
                ComponentMouse.Captured = this;
            }
            else
            {
                ComponentMouse.Captured = null;
            }
        }

        private void InitializeMembers()
        {
            base.Initialize();
            if (DefaultStyles.IsInitialized)
            {
                this.DefaultStyle.BackColor = Color.White;
                this.ActiveStyle = this.DefaultStyle;
                this.ActionStyle = this.DefaultStyle;
                this.closeButton = new Button();
                this.closeButton.Click += this.Close;
                this.maximizeButton = new Button();
                this.maximizeButton.Click += this.Maximize;
                this.minimizeButton = new Button();
                this.minimizeButton.Click += this.Minimize;
                this.Update(new GameTime());
            }
        }

        private void Close(object sender, EventArgs e)
        {
            CancelEventArgs eventArgs = new CancelEventArgs(false);
            this.Closing?.Invoke(this, eventArgs);

            if (!eventArgs.Cancel)
            {
                this.Visible = false;
            }

            this.Closed?.Invoke(this, EventArgs.Empty);
        }

        private void Maximize(object sender, EventArgs e)
        {
            CancelEventArgs eventArgs = new CancelEventArgs(false);
            this.Maximizing?.Invoke(this, eventArgs);

            if (!eventArgs.Cancel)
            {
                if (this.ScreenRectangle == Manager.GraphicsDevice.ScissorRectangle)
                {
                    this.nextScreenRectangle = this.normalBounds;
                }
                else
                {
                    this.normalBounds = this.ScreenRectangle;
                    this.nextScreenRectangle = Manager.GraphicsDevice.ScissorRectangle;
                }
            }

            this.Maximized?.Invoke(this, EventArgs.Empty);
        }

        private void Minimize(object sender, EventArgs e)
        {
            CancelEventArgs eventArgs = new CancelEventArgs(false);
            this.Minimizing?.Invoke(this, eventArgs);

            if (!eventArgs.Cancel)
            {
                this.Visible = false;
            }

            this.Minimized?.Invoke(this, EventArgs.Empty);
        }
    }
}
