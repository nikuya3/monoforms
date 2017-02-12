// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisualControl.cs" company="">
//   Copyright (c) 2015 .
// </copyright>
// <summary>
//   Represents a <see cref="Control" /> with a graphical representation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    using Core;
    using Core.Extensions;
    using Input;

    using MonoForms.Shapes;

    /// <summary>
    ///     Represents a <see cref="Control" /> with a graphical representation.
    /// </summary>
    public abstract class VisualControl : Control, IDrawable
    {
        /// <summary>
        /// The <see cref="Style"/> used when the 
        /// </summary>
        private Style actionStyle;

        /// <summary>
        /// The active style.
        /// </summary>
        private Style activeStyle;

        /// <summary>
        /// The default style.
        /// </summary>
        private Style defaultStyle;

        /// <summary>
        /// The draw order.
        /// </summary>
        private int drawOrder;

        /// <summary>
        /// The previous mouse state.
        /// </summary>
        private MouseState previousMouseState;

        /// <summary>
        /// The screen size.
        /// </summary>
        private Size screenSize;

        /// <summary>
        /// The style.
        /// </summary>
        private Style style;

        /// <summary>
        /// A value indicating if the shapes should be reinitialized on the next update process.
        /// </summary>
        private bool refreshOnUpdate = true;

        /// <summary>
        /// The subscribed parent rectangle changed.
        /// </summary>
        private bool subscribedParentRectangleChanged;

        /// <summary>
        /// The Visible.
        /// </summary>
        private bool visible;

        /// <summary>
        ///     Initializes a new instance of the <see cref="VisualControl" /> class.
        /// </summary>
        protected VisualControl()
        {
            this.Shapes = new Collection<IShape>();
            this.previousMouseState = new MouseState();
            this.drawOrder = 0;
            this.subscribedParentRectangleChanged = false;
            this.visible = true;
            this.InitializeMembers();
        }

        /// <summary>
        ///     Gets or sets the <see cref="Style" /> used by this <see cref="VisualControl" /> when an action is initiated on it.
        /// </summary>
        public virtual Style ActionStyle
        {
            get
            {
                return this.actionStyle;
            }

            set
            {
                this.actionStyle = value;
                if (this.style.StyleType == StyleType.Action)
                {
                    this.Refresh();
                }
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="Style" /> used by this <see cref="VisualControl" /> when it is responding to the user
        ///     input.
        /// </summary>
        public virtual Style ActiveStyle
        {
            get
            {
                return this.activeStyle;
            }

            set
            {
                this.activeStyle = value;
                if (this.style.StyleType == StyleType.Active)
                {
                    this.Refresh();
                }
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="object" /> indicating the main content of this <see cref="VisualControl" />.
        /// </summary>
        public virtual object Content { get; set; }

        /// <summary>
        ///     Gets or sets an instance of <see cref="Style" /> used by this instance of <see cref="VisualControl" /> in its
        ///     default mode.
        /// </summary>
        public Style DefaultStyle
        {
            get
            {
                return this.defaultStyle;
            }

            set
            {
                this.defaultStyle = value;
                if (this.style.StyleType == StyleType.Default)
                {
                    this.Refresh();
                }
            }
        }

        /// <summary>
        ///     Gets or sets a <see cref="Point" /> indicating the location of this <see cref="VisualControl" /> relative to its
        ///     parent <see cref="VisualControl" />.
        /// </summary>
        public virtual Point Location
        {
            get
            {
                if (this.Parent is VisualControl)
                {
                    var parent = this.Parent as VisualControl;
                    return new Point(
                        this.ScreenLocation.X - parent.ScreenLocation.X, 
                        this.ScreenLocation.Y - parent.ScreenLocation.Y);
                }

                return this.ScreenLocation;
            }

            set
            {
                if (this.Parent is VisualControl)
                {
                    var parent = this.Parent as VisualControl;
                    this.ScreenLocation = new Point(
                        value.X + parent.ScreenLocation.X, 
                        value.Y + parent.ScreenLocation.Y);
                }
                else
                {
                    this.ScreenLocation = value;
                }
            }
        }

        /// <summary>
        ///     Gets or sets a <see cref="Point" /> indicating the location of this <see cref="VisualControl" /> relative to the
        ///     <see cref="GameWindow" /> containing this <see cref="VisualControl" />.
        /// </summary>
        public virtual Point ScreenLocation
        {
            get
            {
                return this.ScreenRectangle.Location;
            }

            set
            {
                this.ScreenRectangle = this.ScreenRectangle.FromPointAndSize(value, this.ScreenRectangle.GetSize());
            }
        }

        /// <summary>
        ///     Gets or sets a <see cref="Microsoft.Xna.Framework.Rectangle" /> indicating the whole Visible boundaries of this
        ///     <see cref="VisualControl" />.
        /// </summary>
        public virtual Microsoft.Xna.Framework.Rectangle ScreenRectangle
        {
            get
            {
                return this.WholeRectangle;
            }

            set
            {
                if (value != this.WholeRectangle)
                {
                    this.RectangleChanged?.Invoke(
                        this, 
                        new PropertyChangedEventArgs<Microsoft.Xna.Framework.Rectangle>(
                            "ScreenRectangle", 
                            this.ScreenRectangle, 
                            value));
                }

                this.WholeRectangle = value;
                if (DefaultStyles.IsInitialized)
                {
                    this.Refresh();
                }
            }
        }

        /// <summary>
        ///     Gets or sets an instance of <see cref="Size" /> indicating the size of this instance of
        ///     <see cref="VisualControl" />.
        /// </summary>
        public virtual Size Size
        {
            get
            {
                if (this.screenSize.IsEmpty)
                {
                    return this.screenSize;
                }

                return this.ScreenRectangle.GetSize();
            }

            set
            {
                this.screenSize = value;
                if (!value.IsEmpty)
                {
                    this.ScreenRectangle = this.ScreenRectangle.SetSize(value);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="Style" /> used by this <see cref="VisualControl" />.
        /// </summary>
        public virtual Style Style
        {
            get
            {
                return this.style;
            }

            set
            {
                ////this.actionStyle = value;
                ////this.activeStyle = value;
                ////this.defaultStyle = value;
                this.style = value;
                this.Refresh();
            }
        }

        /// <summary>
        ///     Gets or sets a <see cref="List{T}" /> which holds every <see cref="IShape" /> registered in this
        ///     <see cref="VisualControl" />.
        /// </summary>
        protected Collection<IShape> Shapes { get; set; }

        /// <summary>
        ///     Gets or sets an instance of <see cref="Microsoft.Xna.Framework.Rectangle" /> indicating the whole boundaries of
        ///     this instance of <see cref="VisualControl" />.
        /// </summary>
        protected abstract Microsoft.Xna.Framework.Rectangle WholeRectangle { get; set; }

        /// <summary>
        ///     Occurs when <see cref="DrawOrder" /> changed.
        /// </summary>
        public event EventHandler<EventArgs> DrawOrderChanged;

        /// <summary>
        ///     Occurs when <see cref="Visible" /> changed.
        /// </summary>
        public event EventHandler<EventArgs> VisibleChanged;

        /// <summary>
        ///     Gets or sets the draw priority of this <see cref="IDrawable" /> relative to others.
        /// </summary>
        public int DrawOrder
        {
            get
            {
                return this.drawOrder;
            }

            set
            {
                this.drawOrder = value;
                this.DrawOrderChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="IDrawable" /> should be drawn.
        /// </summary>
        public virtual bool Visible
        {
            get
            {
                return this.visible;
            }

            set
            {
                this.visible = value;
                this.VisibleChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Draws this <see cref="IDrawable"/>.
        /// </summary>
        /// <param name="gameTime">
        /// A <see cref="GameTime"/> representing the current timing state.
        /// </param>
        public virtual void Draw(GameTime gameTime)
        {
            if (DefaultStyles.IsInitialized)
            {
                if (this.visible)
                {
                    Manager.SpriteBatch.Begin(true);
                    foreach (var geometry in this.Shapes)
                    {
                        geometry.Draw(Manager.SpriteBatch.SpriteBatch);
                    }

                    Manager.SpriteBatch.End(true);
                }
            }
        }

        /// <summary>
        ///     Occurs when this <see cref="VisualControl" /> is clicked.
        /// </summary>
        public event EventHandler<EventArgs> Click;

        /// <summary>
        ///     Occurs when a <see cref="MouseButton" /> was pressed for the first time while the <see cref="Mouse" /> entered this
        ///     <see cref="VisualControl" />.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseClick;

        /// <summary>
        ///     Occurs when the mouse pointer entered this <see cref="VisualControl" />.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseEnter;

        /// <summary>
        ///     Occurs when the mouse pointer left this <see cref="VisualControl" />.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseLeave;

        /// <summary>
        /// Occurs when a <see cref="MouseButton"/> was pressed while the <see cref="Mouse"/> entered this <see cref="VisualControl"/>.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseDown;

        /// <summary>
        ///     Occurs when a <see cref="MouseButton" /> was just released while the <see cref="Mouse" /> entered this
        ///     <see cref="VisualControl" />.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseUp;

        /// <summary>
        ///     Occurs when <see cref="ScreenRectangle" /> was changed.
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs<Microsoft.Xna.Framework.Rectangle>> RectangleChanged;

        /// <inheritdoc />
        public override void Initialize()
        {
            this.InitializeMembers();
        }

        /// <summary>
        ///     Forces this instance of <see cref="VisualControl" /> to recalculate and redraw.
        /// </summary>
        public void Refresh()
        {
            this.refreshOnUpdate = true;
        }

        /// <inheritdoc />
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!DefaultStyles.IsInitialized)
            {
                return;
            }

            if (this.refreshOnUpdate)
            {
                this.InitializeShapes();
                this.refreshOnUpdate = false;
            }

            var mouseState = ComponentMouse.GetMouseState(gameTime);
            this.CheckEvents(mouseState);
            this.previousMouseState = mouseState;
        }

        /// <summary>
        ///     Initializes the shapes of this instance of <see cref="VisualControl" />.
        /// </summary>
        protected abstract void InitializeShapes();

        /// <summary>
        /// The check events.
        /// </summary>
        /// <param name="mouseState">
        /// The mouse state.
        /// </param>
        private void CheckEvents(MouseState mouseState)
        {
            if (!this.Visible)
            {
                return;
            }

            foreach (var geometry in this.Shapes)
            {
                if (geometry.Bounds.Contains(new Point(mouseState.X, mouseState.Y)))
                {
                    this.OnMouseEnter(mouseState);
                    if (mouseState.LeftButton == ButtonState.Released
                        && this.previousMouseState.LeftButton == ButtonState.Pressed)
                    {
                        this.OnMouseUp(mouseState, MouseButton.Left);
                    }

                    if (mouseState.RightButton == ButtonState.Released
                        && this.previousMouseState.RightButton == ButtonState.Pressed)
                    {
                        this.OnMouseUp(mouseState, MouseButton.Right);
                    }

                    if (mouseState.MiddleButton == ButtonState.Released
                        && this.previousMouseState.MiddleButton == ButtonState.Pressed)
                    {
                        this.OnMouseUp(mouseState, MouseButton.Middle);
                    }

                    if (mouseState.XButton1 == ButtonState.Released
                        && this.previousMouseState.XButton1 == ButtonState.Pressed)
                    {
                        this.OnMouseUp(mouseState, MouseButton.XButton1);
                    }

                    if (mouseState.XButton2 == ButtonState.Released
                        && this.previousMouseState.XButton2 == ButtonState.Pressed)
                    {
                        this.OnMouseUp(mouseState, MouseButton.XButton2);
                    }

                    if (ComponentMouse.IsMouseDown)
                    {
                        switch (ComponentMouse.PressedButton)
                        {
                            case MouseButton.Left:
                                {
                                    if (this.previousMouseState.LeftButton == ButtonState.Pressed)
                                    {
                                        this.OnMouseDown(mouseState, MouseButton.Left);
                                    }
                                    else
                                    {
                                        this.OnMouseClick(mouseState, MouseButton.Left);
                                    }

                                    break;
                                }

                            case MouseButton.Right:
                                {
                                    if (this.previousMouseState.RightButton == ButtonState.Pressed)
                                    {
                                        this.OnMouseDown(mouseState, MouseButton.Right);
                                    }
                                    else
                                    {
                                        this.OnMouseClick(mouseState, MouseButton.Right);
                                    }

                                    break;
                                }

                            case MouseButton.Middle:
                                {
                                    if (this.previousMouseState.MiddleButton == ButtonState.Pressed)
                                    {
                                        this.OnMouseDown(mouseState, MouseButton.Middle);
                                    }
                                    else
                                    {
                                        this.OnMouseClick(mouseState, MouseButton.Middle);
                                    }

                                    break;
                                }

                            case MouseButton.XButton1:
                                {
                                    if (this.previousMouseState.XButton1 == ButtonState.Pressed)
                                    {
                                        this.OnMouseDown(mouseState, MouseButton.XButton1);
                                    }
                                    else
                                    {
                                        this.OnMouseClick(mouseState, MouseButton.XButton1);
                                    }

                                    break;
                                }

                            case MouseButton.XButton2:
                                {
                                    if (mouseState.XButton2 == ButtonState.Pressed)
                                    {
                                        if (this.previousMouseState.XButton2 == ButtonState.Pressed)
                                        {
                                            this.OnMouseDown(mouseState, MouseButton.XButton2);
                                        }
                                        else
                                        {
                                            this.OnMouseClick(mouseState, MouseButton.XButton2);
                                        }
                                    }

                                    break;
                                }
                        }
                    }

                    break;
                }

                if (this.Shapes.IndexOf(geometry) == this.Shapes.Count - 1)
                {
                    this.OnMouseLeave(mouseState);
                }
            }
        }

        /// <summary>
        /// The initialize members.
        /// </summary>
        private void InitializeMembers()
        {
            base.Initialize();
            if (DefaultStyles.IsInitialized)
            {
                this.defaultStyle = new Style(StyleType.Default);
                this.actionStyle = new Style(StyleType.Action);
                this.activeStyle = new Style(StyleType.Active);
                this.style = this.defaultStyle;
                this.MouseEnter += (sender, e) => { this.Style = this.ActiveStyle; };
                this.MouseLeave += (sender, e) => { this.Style = this.DefaultStyle; };
                this.MouseClick += (sender, e) => { this.Style = this.ActionStyle; };
                this.MouseUp += (sender, e) => { this.Style = this.ActiveStyle; };
                this.ParentChanged += (sender, e) =>
                    {
                        if (this.Parent != null && !this.subscribedParentRectangleChanged)
                        {
                            var parent = this.Parent as VisualControl;
                            parent.RectangleChanged += this.Parent_RectangleChanged;
                            this.subscribedParentRectangleChanged = true;
                        }
                    };
            }
        }

        /// <summary>
        /// The parent_ rectangle changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Parent_RectangleChanged(
            object sender, 
            PropertyChangedEventArgs<Microsoft.Xna.Framework.Rectangle> e)
        {
            var newRectangle = new Microsoft.Xna.Framework.Rectangle();
            newRectangle.X = this.ScreenRectangle.X + (e.NewValue.X - e.OldValue.X);
            newRectangle.Y = this.ScreenRectangle.Y + (e.NewValue.Y - e.OldValue.Y);
            newRectangle.Width = this.ScreenRectangle.Width;
            newRectangle.Height = this.ScreenRectangle.Height;
            this.ScreenRectangle = newRectangle;
        }

        /// <summary>
        /// The on mouse down.
        /// </summary>
        /// <param name="mouseState">
        /// The mouse state.
        /// </param>
        /// <param name="button">
        /// The button.
        /// </param>
        private void OnMouseDown(MouseState mouseState, MouseButton button)
        {
            this.MouseDown?.Invoke(this, new MouseEventArgs(button, 1, 0, mouseState.ScrollWheelValue, new Point(mouseState.X, mouseState.Y)));
        }

        /// <summary>
        /// The on mouse enter.
        /// </summary>
        /// <param name="mouseState">
        /// The mouse state.
        /// </param>
        private void OnMouseEnter(MouseState mouseState)
        {
            var alreadyEntered =
                this.Shapes.Any(
                    shape => shape.Bounds.Contains(new Point(this.previousMouseState.X, this.previousMouseState.Y)));

            if (!alreadyEntered)
            {
                this.MouseEnter?.Invoke(
                    this, 
                    new MouseEventArgs(MouseButton.None, 0, 0, mouseState.ScrollWheelValue, new Point(mouseState.X, mouseState.Y)));
            }
        }

        private void OnMouseClick(MouseState mouseState, MouseButton button)
        {
            this.MouseClick?.Invoke(this, new MouseEventArgs(button, 1, 0, mouseState.ScrollWheelValue, new Point(mouseState.X, mouseState.Y)));
            ComponentMouse.Captured = this;
        }

        /// <summary>
        /// The on mouse leave.
        /// </summary>
        /// <param name="mouseState">
        /// The mouse state.
        /// </param>
        private void OnMouseLeave(MouseState mouseState)
        {
            var enteredBefore =
                this.Shapes.Any(
                    shape => shape.Bounds.Contains(new Point(this.previousMouseState.X, this.previousMouseState.Y)));

            if (enteredBefore)
            {
                this.MouseLeave?.Invoke(
                    this, 
                    new MouseEventArgs(MouseButton.None, 0, 0, mouseState.ScrollWheelValue, new Point(mouseState.X, mouseState.Y)));
            }
        }

        /// <summary>
        /// The on mouse up.
        /// </summary>
        /// <param name="mouseState">
        /// The mouse state.
        /// </param>
        /// <param name="button">
        /// The button.
        /// </param>
        private void OnMouseUp(MouseState mouseState, MouseButton button)
        {
            this.MouseUp?.Invoke(this, new MouseEventArgs(button, 1, 0, mouseState.ScrollWheelValue, new Point(mouseState.X, mouseState.Y)));
            ComponentMouse.Captured = null;
            if (button == ComponentMouse.DefaultClickButton)
            {
                this.Click?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}