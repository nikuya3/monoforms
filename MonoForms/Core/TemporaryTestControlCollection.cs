//-----------------------------------------------------------------------
// <copyright file="TemporaryTestControlCollection.cs" company="">
//     Copyright (c) 2014 .
// </copyright>
//-----------------------------------------------------------------------

namespace MonoForms.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Microsoft.Xna.Framework;

    using MonoForms.Controls;

    /// <summary>
    /// A temporary test collection of <see cref="Control"/>s.
    /// </summary>
    public class TemporaryTestControlCollection : ICollection<Control>
    {
        private List<Control> controls;
        private ContainerControl owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemporaryTestControlCollection"/> class.
        /// </summary>
        /// <param name="owner">
        /// A <see cref="ContainerControl"/> to be associated with this <see cref="TemporaryTestControlCollection"/>.
        /// </param>
        public TemporaryTestControlCollection(ContainerControl owner)
        {
            this.owner = owner;
            this.controls = new List<Control>();
        }

        /// <inheritdoc/>
        public int Count => this.controls.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public void Add(Control item)
        {
            if (item is VisualControl)
            {
                VisualControl visualControl = item as VisualControl;
                if (!this.owner.ScreenRectangle.Contains(visualControl.ScreenRectangle) && !(this.owner is ComboBox))
                {
                    visualControl.Location = new Point(this.owner.Style.BorderThickness, this.owner.Style.BorderThickness);
                    if (this.owner is Form)
                    {
                        Form form = this.owner as Form;
                        visualControl.Location = new Point(visualControl.Location.X, visualControl.Location.Y + form.CaptionBar.Height);
                    }
                }
            }

            item.Parent = this.owner;
            this.controls.Add(item);
        }

        /// <inheritdoc/>
        public bool Contains(Control item)
        {
            return this.controls.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(Control[] array, int arrayIndex)
        {
            this.controls.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public bool Remove(Control item)
        {
            item.Parent = null;
            return this.controls.Remove(item);
        }

        /// <inheritdoc/>
        public IEnumerator<Control> GetEnumerator()
        {
            return new Collection<Control>(this.controls).GetEnumerator();
        }

        /// <inheritdoc/>
        public void Clear()
        {
            foreach (var control in this.controls)
            {
                control.Parent = null;
            }

            this.controls.Clear();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
