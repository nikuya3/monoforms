// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComboBox.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// <summary>
//   Represents a standard combo box control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MonoForms.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Microsoft.Xna.Framework;

    using MonoForms.Core;

    /// <summary>
    /// Represents a standard combo box control.
    /// </summary>
    public class ComboBox : ContainerControl
    {
        private const string DefaultText = "ComboBox";
        private Style actionItemStyle;
        private Style activeItemStyle;
        private Style defaultItemStyle;
        private bool expanded;
        private Collection<string> previousStrings;
        private Collection<string> strings;
        private List<Button> buttons;
        private int selectedIndex;
        private string selectedValue;
        private Button selector;
        private Microsoft.Xna.Framework.Rectangle wholeRectangle;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComboBox"/> class.
        /// </summary>
        public ComboBox()
        {
            this.buttons = new List<Button>();
            this.previousStrings = new Collection<string>();
            this.strings = new Collection<string>();
            this.selector = new Button();
            this.wholeRectangle = this.selector.ScreenRectangle;
            this.expanded = false;
            this.selectedIndex = -1;
            this.selectedValue = string.Empty;
            this.InitializeMembers();
        }

        /// <summary>
        /// Occurs when the expanded state of this combo box changed.
        /// </summary>
        public event EventHandler<EventArgs> ExpandedChanged;

        /// <summary>
        /// Occurs when the selected item of this combo box changed.
        /// </summary>
        public event EventHandler<EventArgs> SelectionChanged;

        /// <inheritdoc/>
        public override object Content
        {
            get
            {
                return this.selector.Content;
            }

            set
            {
                this.selector.Content = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance of <see cref="ComboBox"/> is in the expanded mode.
        /// </summary>
        public bool Expanded
        {
            get
            {
                return this.expanded;
            }

            set
            {
                this.expanded = value;
                foreach (Button item in this.buttons)
                {
                    item.Visible = value;
                }

                this.ExpandedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets an instance of <see cref="Collection{T}"/> containing all items of this instance of <see cref="ComboBox"/>.
        /// </summary>
        public Collection<string> Items
        {
            get
            {
                return this.strings;
            }

            set
            {
                this.buttons.Clear();
                Point location = this.selector.ScreenLocation;
                for (int i = 0; i < value.Count; i++)
                {
                    location.Y = this.ScreenLocation.Y + ((int)this.selector.Size.Height * (i + 1));
                    Button toAdd = new Button
                    {
                        ActionStyle = this.actionItemStyle,
                        ActiveStyle = this.activeItemStyle,
                        DefaultStyle = this.defaultItemStyle,
                        Style = this.defaultItemStyle,
                        ScreenLocation = location,
                        Size = this.Size,
                        Content = value[i],
                        Visible = this.expanded
                    };
                    toAdd.Click += this.ItemClicked;
                    this.buttons.Add(toAdd);
                }

                this.Controls.Clear();
                foreach (Button button in this.buttons)
                {
                    this.Controls.Add(button);
                }

                this.strings = new Collection<string>(value);
            }
        }

        /// <summary>
        /// Gets a value indicating the index of the currently selected item.
        /// </summary>
        public int SelectedIndex => this.selectedIndex;

        /// <summary>
        /// Gets the value currently shown in the main selector.
        /// </summary>
        public string SelectedValue => this.selectedValue;

        /// <inheritdoc/>
        protected override Microsoft.Xna.Framework.Rectangle WholeRectangle
        {
            get
            {
                ////Microsoft.Xna.Framework.Rectangle toReturn = new Microsoft.Xna.Framework.Rectangle();
                ////toReturn = Microsoft.Xna.Framework.Rectangle.Union(toReturn, this.selector.ScreenRectangle);
                ////foreach (Button item in this.buttons)
                ////{
                ////    toReturn = Microsoft.Xna.Framework.Rectangle.Union(toReturn, item.ScreenRectangle);
                ////}

                ////return toReturn;
                return this.wholeRectangle;
            }

            set
            {
                this.wholeRectangle = value;
                this.selector.ScreenRectangle = value;
            }
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            this.InitializeMembers();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!this.previousStrings.SequenceEqual(this.strings))
            {
                this.ItemCollectionChanged(this.strings);
            }

            this.previousStrings.Clear();
            foreach (var s in this.strings)
            {
                this.previousStrings.Add(s);
            }
        }

        /// <inheritdoc/>
        protected override void InitializeShapes()
        {
            this.selector.ScreenLocation = this.wholeRectangle.Location;
        }

        private void InitializeMembers()
        {
            base.Initialize();
            if (DefaultStyles.IsInitialized && !this.Initialized)
            {
                this.actionItemStyle = new Style(null, null, 1, null, null, null, null, StyleType.Action);
                this.activeItemStyle = new Style(null, null, 1, null, null, null, null, StyleType.Active);
                this.defaultItemStyle = new Style(Color.White, null, 1, null, null, null, null, StyleType.Default);
                this.selector.Click += (sender, e) => { this.Expanded = !this.Expanded; };
                this.selector.Content = ComboBox.DefaultText;
                this.PrivateControls.Add(this.selector);
                this.Update(new GameTime());
                this.Initialized = true;
            }
        }

        private void ItemClicked(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            this.selectedValue = clicked.Content as string;
            this.selectedIndex = this.buttons.IndexOf(clicked);
            this.Content = this.selectedValue;
            this.Expanded = false;
            this.SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ItemCollectionChanged(IList<string> items)
        {
            this.Items = new Collection<string>(items);
        }
    }
}
