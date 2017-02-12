// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WindowsTest.Forms
{
    using System.Diagnostics;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    using MonoForms;
    using MonoForms.Controls;
    using MonoForms.Core;

    /// <summary>
    /// Represents the main <see cref="Form"/> of this application.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (gameTime.TotalGameTime.Seconds == 2)
            {
                if (!this.comboBox.Items.Contains("Item4"))
                {
                    this.comboBox.Items.Add("Item4");
                }
            }

            if (gameTime.TotalGameTime.Seconds == 3)
            {
                if (!this.comboBox.Items.Contains("Item5"))
                {
                    this.comboBox.Items.Add("Item5");
                }

                this.Controls.Remove(this.button);
            }

            if (gameTime.TotalGameTime.Seconds == 4)
            {
                if (this.comboBox.Items.Contains("Item5"))
                {
                    this.comboBox.Items.Remove("Item5");
                }
            }
        }
    }
}
