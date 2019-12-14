using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Starbound.Common.WinForms.Themes;

namespace Starbound.Common.WinForms.ThemedControls
{
    public class ThemedExpandCollapseButton : ThemedButton
    {
        public string ExpandText { get; set; }
        public string CollapseText { get; set; }

        public Bitmap ExpandIcon { get; set; }
        public Bitmap CollapsedIcon { get; set; }

        private bool expandedState;

        public bool ExpandedState
        {
            get
            {
                return expandedState;
            }
            set
            {
                expandedState = value;
                Text = expandedState ? CollapseText : ExpandText;
                OnExpandStateChanged();
            }
        }


        public ThemedExpandCollapseButton()
        {
            this.Click += ThemedExpandCollapseButton_Click;
        }

        private void ThemedExpandCollapseButton_Click(object sender, EventArgs e)
        {
            ExpandedState = !ExpandedState;
            if (ExpandedState) { OnExpanded(); }
            else { OnCollapsed(); }
        }

        public event EventHandler ExpandStateChanged;

        public event EventHandler Expanded;

        public event EventHandler Collapsed;

        public void OnExpandStateChanged()
        {
            if (ExpandStateChanged != null)
            {
                ExpandStateChanged(this, EventArgs.Empty);
            }
        }

        public void OnExpanded()
        {
            if (Expanded != null)
            {
                Expanded(this, EventArgs.Empty);
            }
        }

        public void OnCollapsed()
        {
            if (Collapsed != null)
            {
                Collapsed(this, EventArgs.Empty);
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            ThemeManager.Theme.PaintExpandCollapseButton(pevent, this);
        }
    }
}
