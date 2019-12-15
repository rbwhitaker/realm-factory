using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Starbound.Common.WinForms.Themes;
using System.Drawing;

namespace Starbound.Common.WinForms.ThemedControls
{
    public class ThemedPanel : Panel
    {
        public override System.Drawing.Color BackColor
        {
            get
            {
                return Color.White;
            }
            set
            {
                base.BackColor = value;
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            ThemeManager.Theme.PaintPanel(pevent, this);
        }
    }
}
