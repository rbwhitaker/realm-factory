using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Starbound.Common.WinForms.Themes;

namespace Starbound.Common.WinForms.ThemedControls
{
    public class ThemedRichTextBox : RichTextBox
    {
        public ThemedRichTextBox()
            :base()
        {
            //SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ThemeManager.Theme.PaintRichTextBox(e, this);
        }
    }
}
