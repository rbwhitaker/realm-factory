using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Starbound.Common.WinForms.Themes;

namespace Starbound.Common.WinForms
{
    /// <summary>
    /// A custom-rendered tab control that looks very nice.
    /// </summary>
    public class AwesomeTabs : TabControl
    {
        /// <summary>
        /// Creates a new AwesomeTabs control.
        /// </summary>
        public AwesomeTabs()
        {
            SetStyle(ControlStyles.UserPaint, true);
        }

        /// <summary>
        /// Handles the rendering.  Sends it to the ThemeManager.
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);

            ThemeManager.Theme.PaintTabControl(pevent, this);
        }
    }
}
