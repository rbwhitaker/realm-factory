using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Starbound.Common.WinForms.ThemedControls;

namespace Starbound.Common.WinForms.Themes
{
    public abstract class Theme
    {
        public abstract void PaintLinkLabel(PaintEventArgs args, LinkLabel linkLabel);

        public abstract void PaintTabControl(PaintEventArgs args, TabControl tabControl);

        public abstract void PaintPanel(PaintEventArgs args, Panel panel);

        public abstract void PaintTextBox(PaintEventArgs args, TextBox textBox);

        public abstract void PaintExpandCollapseButton(PaintEventArgs args, ThemedExpandCollapseButton button);

        public abstract void PaintButton(PaintEventArgs args, Button button);

        public abstract void PaintHeaderPanel(PaintEventArgs args, HeaderPanel headerPanel);

        public abstract void PaintCollapsiblePanel(PaintEventArgs args, CollapsiblePanel panel);

        public abstract void PaintRichTextBox(PaintEventArgs pevent, ThemedRichTextBox themedRichTextBox);
    }
}
