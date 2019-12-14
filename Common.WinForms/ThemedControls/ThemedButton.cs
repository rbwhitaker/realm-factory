using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Starbound.Common.WinForms.Themes;

namespace Starbound.Common.WinForms.ThemedControls
{
    public class ThemedButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            ThemeManager.Theme.PaintButton(pevent, this);
        }
    }
}
