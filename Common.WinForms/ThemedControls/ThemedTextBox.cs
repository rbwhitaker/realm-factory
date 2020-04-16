using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Starbound.Common.WinForms.Themes;

namespace Starbound.Common.WinForms.ThemedControls
{
    public class ThemedTextBox : TextBox
    {
        public ThemedTextBox()
        {
            //SetStyle(ControlStyles.UserPaint, true);
            KeyDown += HandleKeyDown;
            LostFocus += HandleLostFocus;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Enter) { return false; }
            return base.IsInputKey(keyData);
        }

        public event EventHandler FinishedEditing;

        public void OnFinishedEditing()
        {
            if (FinishedEditing != null)
            {
                FinishedEditing(this, EventArgs.Empty);
            }
        }

        public void HandleKeyDown(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Enter)
            {
                args.SuppressKeyPress = true;
                OnFinishedEditing();
            }
        }

        public void HandleLostFocus(object sender, EventArgs args)
        {
            OnFinishedEditing();
        }
        
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            //ThemeManager.Theme.PaintTextBox(pevent, this);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            //ThemeManager.Theme.PaintTextBox(pevent, this);
        }
    }
}
