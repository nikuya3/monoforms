// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.Designer.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WindowsTest.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoForms;
    using MonoForms.Controls;
    using MonoForms.Core;
    using MonoForms.Core.Extensions;

    public partial class MainForm
    {
        private Button button;
        private CheckBox checkBox;
        private ComboBox comboBox;
        private Label label;
        private PictureBox pictureBox;
        private ProgressBar progressBar;
        private Slider slider;
        private Label sliderLabel;
        private TextBox textBox;

        private void InitializeComponent()
        {
            this.ScreenLocation = new Point(100, 100);
            this.Size = new Size(this.Size.Width + 40, this.Size.Height - 40);
            this.IsMoveable = true;

            // Initialize controls
            this.button = new Button();
            this.checkBox = new CheckBox();
            this.comboBox = new ComboBox();
            this.label = new Label();
            this.pictureBox = new PictureBox();
            this.progressBar = new ProgressBar();
            this.slider = new Slider();
            this.sliderLabel = new Label();
            this.textBox = new TextBox();

            // Add controls
            this.Controls.Add(this.button);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.label);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.slider);
            this.Controls.Add(this.sliderLabel);
            this.Controls.Add(this.textBox);

            // Alter controls
            this.button.Content = "Click me!";
            this.button.Location = new Point(190, this.CaptionBar.Height + 50);

            this.checkBox.Content = "Check me!";
            this.checkBox.Location = new Point(20, 140);

            this.comboBox.Content = "Expand me!";
            this.comboBox.Location = new Point(190, 130);
            this.comboBox.Size = new Size(this.comboBox.Size.Width + 20, this.comboBox.Size.Height);
            List<string> items = new List<string>()
            {
                "Item1",
                "Item2",
                "Item3"
            };
            this.comboBox.Items = new ObservableCollection<string>(items);

            this.label.Content = "Read me!";
            this.label.Location = new Point(20, 180);

            Texture2D image = Globals.ContentManager.Load<Texture2D>("-Logo");
            this.pictureBox.Content = image;
            this.pictureBox.Location = new Point(20, this.CaptionBar.Height + 10);
            Size imageSize = image.Bounds.GetSize();
            this.pictureBox.Size = imageSize / new Size(10, 10);

            this.textBox.Content = "Type me!";
            this.textBox.Click += (sender, e) => { this.textBox.Content = string.Empty; };
            this.textBox.Location = new Point(190, this.CaptionBar.Height + 10);

            this.progressBar.Value = 50;
            this.progressBar.Location = new Point(20, (int)(this.label.Location.Y + this.label.Size.Height + 10));

            this.slider.Location = new Point(190, (int)(this.label.Location.Y + this.label.Size.Height));
            this.slider.ValueChanged += (sender, args) => { this.sliderLabel.Content = this.slider.Value; };

            this.sliderLabel.Content = this.slider.Value;
            this.sliderLabel.Location = new Point(this.slider.Location.X, (int)(this.slider.Location.Y + this.slider.Size.Height));

            // Make MainForm visible
            this.Visible = true;
        }
    }
}
