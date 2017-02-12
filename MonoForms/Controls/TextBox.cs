// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextBox.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms.Controls
{
	using System;
	using System.Diagnostics;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;

	using MonoForms.Core;
	using MonoForms.Input;

	using MonoForms.Core.Extensions;
	using MonoForms.Shapes;

    /// <summary>
	/// Represents a control which displays a field where text can be entered.
	/// </summary>
	public class TextBox : VisualControl, IInputReceivable
	{
		private bool focused;
		private string input;
		private MouseState previousMouseState;
		private Text text;

		/// <summary>
		/// Initializes a new instance of the <see cref="TextBox"/> class.
		/// </summary>
		public TextBox()
		{
			this.focused = false;
			this.input = string.Empty;
			this.InitializeMembers();
		}

		/// <summary>
		/// Occurs when the text content of this instance of <see cref="TextBox"/> changed.
		/// </summary>
		public event EventHandler<EventArgs> TextChanged;

		/// <inheritdoc/>
		public override object Content
		{
			get
			{
				return this.input;
			}

			set
			{
				if (value is string)
				{
					this.input = value as string;
				    this.TextChanged?.Invoke(this, EventArgs.Empty);
				}

				this.Refresh();
			}
		}

		/// <inheritdoc/>
		public bool Focused
		{
			get
			{
				return this.focused;
			}

			set
			{
				this.focused = value;
			}
		}

		/// <inheritdoc/>
		protected override Microsoft.Xna.Framework.Rectangle WholeRectangle { get; set; }

		/// <summary>
		/// Sets the focus of the application on this instance of <see cref="TextBox"/>.
		/// </summary>
		public void Focus()
		{
			this.focused = true;
            FormsKeyboard.Receiver = this;
		}

		/// <inheritdoc/>
		public override void Initialize()
		{
			this.InitializeMembers();
		}

		/// <inheritdoc/>
		public void ReceiveCommandInput(char command)
		{
		}

		/// <inheritdoc/>
		public void ReceiveSpecialInput(Keys key)
		{
			if (key == Keys.Back)
			{
				if (this.input.Length > 0)
				{
					this.input = this.input.Remove(this.input.Length - 1);
					this.OnTextChanged();
				}
			}

			this.Refresh();
		}

		/// <inheritdoc/>
		public void ReceiveTextInput(string text)
		{
			this.input += text;
			this.Refresh();
			this.OnTextChanged();
		}

		/// <inheritdoc/>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
            if (this.Focused && FormsKeyboard.Receiver != this)
			{
                FormsKeyboard.Receiver = this;
			}

            if (Manager.IsInitialized)
			{
				MouseState mouseState = ComponentMouse.GetMouseState(gameTime);
				if (mouseState.LeftButton == ButtonState.Released && this.previousMouseState.LeftButton == ButtonState.Pressed)
				{
					if (!this.WholeRectangle.Contains(mouseState.X, mouseState.Y))
					{
                        FormsKeyboard.Receiver = null;
						this.Focused = false;
					}
				}

				this.previousMouseState = mouseState;
			}
		}

		/// <inheritdoc/>
		protected override void InitializeShapes()
		{
			this.Shapes.Clear();
			this.Shapes.Add(new MonoForms.Shapes.Rectangle(this.WholeRectangle, this.Style));
			this.text = new Text(this.CropText(this.input), this.Style.Font, this.Style.ForeColor, SpriteEffects.None, new Vector2(this.WholeRectangle.Location.X, this.WholeRectangle.Location.Y), this.Style.TextScale, 0F, Vector2.Zero, this.WholeRectangle);
			this.Shapes.Add(this.text);
		}

		private string CropText(string text)
		{
			string toReturn = text;
			Size textSize = this.Style.Font.MeasureStringSize(toReturn, this.Style.TextScale);
			while (textSize.Width > this.WholeRectangle.Width)
			{
				toReturn = toReturn.Remove(0, 1);
				textSize = this.Style.Font.MeasureStringSize(toReturn, this.Style.TextScale);
			}

			return toReturn;
		}

		private void InitializeMembers()
		{
			base.Initialize();
			this.WholeRectangle = new Microsoft.Xna.Framework.Rectangle(this.ScreenLocation.X, this.ScreenLocation.Y, 120, 30);
			if (DefaultStyles.IsInitialized)
			{
				this.DefaultStyle = new Style(Color.White, null, 1, null, null, null, null, StyleType.Default);
				this.Style = this.DefaultStyle;
				this.ActionStyle = new Style(Color.White, Color.Blue, 1, null, null, null, null, StyleType.Action);
				this.ActiveStyle = new Style(Color.White, Color.LightBlue, 1, null, null, null, null, StyleType.Active);
                this.Click += (sender, e) => { FormsKeyboard.Receiver = this; };
                this.Update(new GameTime());
            }
		}

		private void OnTextChanged()
		{
		    this.TextChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
