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
    public enum CollapsedState { Collapsed, Expanded }

    [Designer(typeof(Starbound.Common.WinForms.CollapsiblePanelDesigner))]
    public partial class CollapsiblePanel : UserControl
    {
        public string HeaderText { get; set; }
        public int HeaderHeight { get; set; }
        public Bitmap Icon { get; set; }

        private CollapsedState state = CollapsedState.Expanded;

        [Browsable(false)]
        public CollapsedState State 
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        private int storedHeight;

        public void Collapse()
        {
            if (State != CollapsedState.Collapsed)
            {
                state = CollapsedState.Collapsed;
                WorkingArea.Visible = false;
                storedHeight = Height;
                Height = HeaderHeight;
            }
        }

        public void Expand()
        {
            if (State != CollapsedState.Expanded)
            {
                state = CollapsedState.Expanded;
                workingArea.Visible = true;
                Height = storedHeight;
            }
        }

        public CollapsiblePanel()
        {
            InitializeComponent();
            State = CollapsedState.Expanded;
            HeaderHeight = 30;
        }

        public Panel WorkingArea
        {
            get
            {
                return workingArea;
            }
            set
            {
                workingArea = value;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Themes.ThemeManager.Theme.PaintCollapsiblePanel(e, this);
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

        private void CollapsiblePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (new Rectangle(Bounds.Width - 26, 0, 26, HeaderHeight).Contains(e.Location))
            {
                if (State == CollapsedState.Collapsed)
                {
                    Expand();
                }
                else
                {
                    Collapse();
                }

                this.Refresh();
            }
        }
    }
}
