using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MolVis
{
    class ToolStripRadioButtonMenuItem : ToolStripMenuItem
    {
        public ToolStripRadioButtonMenuItem()
            : base()
        {
            Initialize();
        }

        public ToolStripRadioButtonMenuItem(string text)
            : base(text, null, (EventHandler)null)
        {
            Initialize();
        }

        public ToolStripRadioButtonMenuItem(Image image)
            : base(null, image, (EventHandler)null)
        {
            Initialize();
        }

        public ToolStripRadioButtonMenuItem(string text, Image image)
            : base(text, image, (EventHandler)null)
        {
            Initialize();
        }

        public ToolStripRadioButtonMenuItem(string text, Image image,
            EventHandler onClick)
            : base(text, image, onClick)
        {
            Initialize();
        }

        public ToolStripRadioButtonMenuItem(string text, Image image,
            EventHandler onClick, string name)
            : base(text, image, onClick, name)
        {
            Initialize();
        }

        public ToolStripRadioButtonMenuItem(string text, Image image,
            params ToolStripItem[] dropDownItems)
            : base(text, image, dropDownItems)
        {
            Initialize();
        }

        public ToolStripRadioButtonMenuItem(string text, Image image,
            EventHandler onClick, Keys shortcutKeys)
            : base(text, image, onClick)
        {
            Initialize();
            this.ShortcutKeys = shortcutKeys;
        }

        // Called by all constructors to initialize CheckOnClick.
        private void Initialize()
        {
            CheckOnClick = true;
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);

            // If this item is no longer in the checked state or if its 
            // parent has not yet been initialized, do nothing.
            if (!Checked || this.Parent == null) return;

            // Clear the checked state for all siblings. 
            foreach (ToolStripItem item in Parent.Items)
            {
                ToolStripRadioButtonMenuItem radioItem =
                    item as ToolStripRadioButtonMenuItem;
                if (radioItem != null && radioItem != this && radioItem.Checked)
                {
                    radioItem.Checked = false;

                    // Only one item can be selected at a time, 
                    // so there is no need to continue.
                    return;
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            // If the item is already in the checked state, do not call 
            // the base method, which would toggle the value. 
            if (Checked) return;

            base.OnClick(e);
        }
    }
}
