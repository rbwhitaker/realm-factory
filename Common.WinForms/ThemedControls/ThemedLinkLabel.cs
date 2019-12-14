using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Starbound.Common.WinForms.Themes;

namespace Starbound.Common.WinForms.ThemedControls
{
    public class ThemedLinkLabel : LinkLabel
    {
        public ThemedLinkLabel()
            : base()
        {
            SetStyle(ControlStyles.UserPaint, true);
            this.MouseEnter += this_MouseEnter;
        }

        private void this_MouseEnter(object sender, EventArgs e)
        {
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            OnPaintBackground(pevent);

            ThemeManager.Theme.PaintLinkLabel(pevent, this);
        }
    }
}
