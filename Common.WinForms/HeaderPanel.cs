using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Starbound.Common.WinForms
{
    public partial class HeaderPanel : UserControl
    {
        public string HeaderText { get; set; }

        public HeaderPanel()
        {
            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Themes.ThemeManager.Theme.PaintHeaderPanel(e, this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Refresh();
        }
    }
}
