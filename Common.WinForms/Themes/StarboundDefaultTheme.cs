using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Starbound.Common.WinForms.ThemedControls;

namespace Starbound.Common.WinForms.Themes
{
    public class StarboundDefaultTheme : Theme
    {
        public Color ThemeColor { get; set; }
        public Color ThemeLightColor { get; set; }
        public Font HeaderPanelFont { get; set; }
        public int HeaderHeight { get; set; }
        public Color PanelOutlineColor { get; set; }
        public Color StandardTextColor { get; set; }
        public Color LinkColor { get; set; }
        public Color DisabledLinkColor { get; set; }
        public Font CollapsiblePanelFont { get; set; }
        public Color CollapsiblePanelTextColor { get; set; }

        public StarboundDefaultTheme()
        {
            ThemeColor = Color.FromArgb(255, 52, 4);
            ThemeLightColor = Color.FromArgb(255, 156, 133);

            HeaderPanelFont = new Font("Segoe UI", 12, FontStyle.Bold);
            HeaderHeight = 26;
            PanelOutlineColor = Color.FromArgb(221, 221, 221);
            StandardTextColor = Color.FromArgb(102, 102, 102);
            LinkColor = Color.FromArgb(51, 51, 51);
            DisabledLinkColor = Color.FromArgb(190, 190, 190);
            CollapsiblePanelFont = new Font("Segoe UI", 12, FontStyle.Regular);
            CollapsiblePanelTextColor = Color.FromArgb(110, 110, 110);
        }

        public override void PaintLinkLabel(PaintEventArgs args, LinkLabel linkLabel)
        {
            int x = 0;
            if (linkLabel.Image != null)
            {
                x += linkLabel.Image.Width + 3;
            }

            Font font = linkLabel.Font;
            if (linkLabel.Bounds.Contains(linkLabel.PointToClient(Cursor.Position)))
            {
                font = new Font(linkLabel.Font, FontStyle.Underline);
            }

            SizeF textSize = args.Graphics.MeasureString(linkLabel.Text, font);
            args.Graphics.DrawString(linkLabel.Text, font, new SolidBrush(linkLabel.Enabled ? LinkColor : DisabledLinkColor), new PointF(x, linkLabel.Height / 2 - textSize.Height / 2));
        }

        public override void PaintExpandCollapseButton(PaintEventArgs args, ThemedExpandCollapseButton button)
        {
            Color outlineColor = Color.FromArgb(182, 182, 182);
            Brush textBrush = new SolidBrush(Color.FromArgb(75, 75, 75));

            DrawButtonBackground(args, button);

            Control parentForm = button.Parent;
            if (parentForm != null)
            {
                Point mousePoint = parentForm.PointToClient(Cursor.Position);
                if (!button.Enabled)
                {
                    textBrush = new SolidBrush(outlineColor);
                }
                else if (button.Bounds.Contains(mousePoint))
                {
                    textBrush = new SolidBrush(Color.FromArgb(75, 75, 75));
                }
            }

            Bitmap icon = null;
            if (button.ExpandedState) { icon = button.CollapsedIcon; }
            if (!button.ExpandedState) { icon = button.ExpandIcon; }

            SizeF textSize = args.Graphics.MeasureString(button.Text, button.Font);
            if (icon != null) textSize.Width += icon.Width + 3;

            if (button.Font == null || button.Text == null) { return; }
            args.Graphics.DrawString(button.Text, button.Font, textBrush, new PointF(button.Width / 2 - textSize.Width / 2 + icon.Width + 3, button.Height / 2 - textSize.Height / 2));
            if (icon != null)
            {
                args.Graphics.DrawImage(icon, new PointF(button.Width / 2 - textSize.Width / 2, button.Height / 2 - icon.Height / 2));
            }
        }

        public override void PaintButton(PaintEventArgs args, Button button)
        {
            Color outlineColor = Color.FromArgb(182, 182, 182);
            Brush textBrush = new SolidBrush(Color.FromArgb(75, 75, 75));

            DrawButtonBackground(args, button);
            
            Control parentForm = button.Parent;
            if (parentForm != null)
            {
                Point mousePoint = parentForm.PointToClient(Cursor.Position);
                if (!button.Enabled)
                {
                    textBrush = new SolidBrush(outlineColor);
                }
                else if (button.Bounds.Contains(mousePoint))
                {                    
                    textBrush = new SolidBrush(Color.FromArgb(75, 75, 75));
                }
            }

            SizeF textSize = args.Graphics.MeasureString(button.Text, button.Font);
            if (button.Font == null || button.Text == null) { return; }
            args.Graphics.DrawString(button.Text, button.Font, textBrush, new Point((int)(button.Width / 2 - textSize.Width / 2), (int)(button.Height / 2 - textSize.Height / 2)));

        }

        private static void DrawButtonBackground(PaintEventArgs args, Button button)
        {
            Color outlineColor = Color.FromArgb(182, 182, 182);
            Pen outlinePen = new Pen(new SolidBrush(outlineColor), 1);
            Brush fillBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, button.Height), Color.FromArgb(255, 255, 255), Color.FromArgb(180, 180, 180));

            Control parentForm = button.Parent;
            if (parentForm != null)
            {
                Point mousePoint = parentForm.PointToClient(Cursor.Position);
                if (!button.Enabled)
                {
                    outlineColor = Color.FromArgb(175, 175, 175);
                    outlinePen = new Pen(new SolidBrush(outlineColor), 1);
                    fillBrush = new SolidBrush(Color.White);

                }
                else if (button.Bounds.Contains(mousePoint))
                {
                    outlineColor = Color.FromArgb(132, 132, 132);
                    outlinePen = new Pen(new SolidBrush(outlineColor), 1);
                    fillBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, button.Height), Color.FromArgb(255, 255, 255), Color.FromArgb(240, 240, 240));
                }
            }

            DrawPseudoRoundedRectangle(args, new Rectangle(0, 0, button.Width, button.Height), outlinePen);

            args.Graphics.FillRectangle(fillBrush, new Rectangle(1, 1, button.Width - 2, button.Height - 2));
        }

        private static void DrawPseudoRoundedRectangle(PaintEventArgs args, Rectangle rectangle, Pen outlinePen)
        {
            args.Graphics.DrawLine(outlinePen, new Point(rectangle.X + 0, rectangle.Y + 1), new Point(rectangle.X + 0, rectangle.Y + rectangle.Height - 2));
            args.Graphics.DrawLine(outlinePen, new Point(rectangle.X + rectangle.Width - 1, rectangle.Y + 1), new Point(rectangle.X + rectangle.Width - 1, rectangle.Y + rectangle.Height - 2));
            args.Graphics.DrawLine(outlinePen, new Point(rectangle.X + 1, rectangle.Y + 0), new Point(rectangle.X + rectangle.Width - 2, rectangle.Y + 0));
            args.Graphics.DrawLine(outlinePen, new Point(rectangle.X + 1, rectangle.Y + rectangle.Height - 1), new Point(rectangle.X + rectangle.Width - 2, rectangle.Y + rectangle.Height - 1));
        }

        public override void PaintHeaderPanel(PaintEventArgs args, HeaderPanel headerPanel)
        {
            SizeF textSize = args.Graphics.MeasureString(headerPanel.HeaderText, HeaderPanelFont);
            args.Graphics.DrawString(headerPanel.HeaderText, HeaderPanelFont, new SolidBrush(ThemeColor), new PointF(2, HeaderHeight - textSize.Height - 4));
            args.Graphics.DrawLine(new Pen(new SolidBrush(ThemeLightColor), 1), new Point(0, HeaderHeight - 2), new Point(headerPanel.Width, HeaderHeight - 2));
        }

        public override void PaintCollapsiblePanel(PaintEventArgs args, CollapsiblePanel panel)
        {
            Graphics graphics = args.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            graphics.FillRectangle(new SolidBrush(Color.FromArgb(212, 212, 212)), new Rectangle(0, 0, panel.Width, panel.HeaderHeight));

            if (panel.Icon != null)
            {
                graphics.DrawImage(panel.Icon, new Point(4, HeaderHeight / 2 - panel.Icon.Height / 2));
            }
            graphics.DrawString(panel.HeaderText, CollapsiblePanelFont, new SolidBrush(CollapsiblePanelTextColor), new PointF(24, HeaderHeight / 2 - 10));

            PointF circleCenter = new PointF(panel.Width - 14, HeaderHeight / 2 + 1);
            graphics.FillEllipse(new SolidBrush(Color.FromArgb(241, 241, 241)), new RectangleF(circleCenter.X - 8, circleCenter.Y - 8, 16, 16));
            if (panel.State == CollapsedState.Collapsed)
            {
                graphics.FillPolygon(new SolidBrush(Color.FromArgb(110, 110, 110)), new PointF[] { new PointF(circleCenter.X, circleCenter.Y - 4),
                    new PointF(circleCenter.X + 4, circleCenter.Y + 3),
                    new PointF(circleCenter.X - 4, circleCenter.Y + 3) });
            }
            else
            {
                graphics.FillPolygon(new SolidBrush(Color.FromArgb(110, 110, 110)), new PointF[] { new PointF(circleCenter.X, circleCenter.Y + 4),
                    new PointF(circleCenter.X + 4, circleCenter.Y - 3),
                    new PointF(circleCenter.X - 4, circleCenter.Y - 3) });
            }
        }

        public override void PaintPanel(PaintEventArgs args, Panel panel)
        {
            Pen outlinePen = new Pen(new SolidBrush(PanelOutlineColor), 1);
            DrawPseudoRoundedRectangle(args, new Rectangle(0, 0, panel.Width, panel.Height), outlinePen);
            args.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(1, 1, panel.Width - 2, panel.Height - 2));
        }

        public override void PaintRichTextBox(PaintEventArgs args, ThemedRichTextBox richTextBox)
        {
            Pen outlinePen = new Pen(new SolidBrush(PanelOutlineColor), 1);
            DrawPseudoRoundedRectangle(args, new Rectangle(0, 0, richTextBox.Width, richTextBox.Height), outlinePen);
            args.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(1, 1, richTextBox.Width - 2, richTextBox.Height - 2));
        }

        public override void PaintTextBox(PaintEventArgs args, TextBox textBox)
        {
            Pen outlinePen = new Pen(new SolidBrush(PanelOutlineColor), 1);
            DrawPseudoRoundedRectangle(args, new Rectangle(0, 0, textBox.Width, textBox.Height), outlinePen);
            args.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(1, 1, textBox.Width - 2, textBox.Height - 2));

            SizeF size = args.Graphics.MeasureString(textBox.Text, textBox.Font);
            args.Graphics.DrawString(textBox.Text, textBox.Font, new SolidBrush(StandardTextColor), new PointF(5, textBox.Height / 2 - size.Height / 2));
        }

        public override void PaintTabControl(PaintEventArgs pevent, TabControl tabControl)
        {
            Rectangle Bounds = tabControl.Bounds;
            int Width = tabControl.Width;
            int Height = tabControl.Height;
            System.Windows.Forms.TabControl.TabPageCollection TabPages = tabControl.TabPages;
            Font Font = tabControl.Font;

            Pen outlinePen = new Pen(new SolidBrush(Color.FromArgb(255, 221, 221, 221)), 1);
            int tabHeight = tabControl.ItemSize.Height;
            pevent.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(1, tabHeight + 1, Bounds.Width - 2, Bounds.Height - tabHeight - 2));
            pevent.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 241, 241, 241)), new Rectangle(0, 0, Bounds.Width, tabHeight));
            pevent.Graphics.DrawLine(outlinePen, new Point(1, tabHeight), new Point(Bounds.Width - 2, tabHeight));
            pevent.Graphics.DrawLine(outlinePen, new Point(1, Height - 1), new Point(Bounds.Width - 2, Height - 1));
            pevent.Graphics.DrawLine(outlinePen, new Point(0, tabHeight + 1), new Point(0, Height - 2));
            pevent.Graphics.DrawLine(outlinePen, new Point(Width - 1, tabHeight + 1), new Point(Width - 1, Height - 2));

            for (int index = 0; index < TabPages.Count; index++)
            {
                TabPage tabPage = TabPages[index];
                Rectangle bounds = tabControl.GetTabRect(index);

                if (tabControl.SelectedTab == tabPage)
                {
                    pevent.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(bounds.X, bounds.Y + 1, bounds.Width + 1, bounds.Height - 1));

                    pevent.Graphics.DrawLine(outlinePen, new Point(bounds.X, bounds.Top + 1), new Point(bounds.X, tabHeight - 1));
                    pevent.Graphics.DrawLine(outlinePen, new Point(bounds.Right, bounds.Top + 1), new Point(bounds.Right, tabHeight - 1));
                    pevent.Graphics.DrawLine(outlinePen, new Point(bounds.Left + 1, bounds.Top), new Point(bounds.Right - 1, bounds.Top));

                    string title = tabPage.Text;
                    SizeF size = pevent.Graphics.MeasureString(title, Font);
                    pevent.Graphics.DrawString(title, Font, new SolidBrush(Color.Black), bounds.X + bounds.Width / 2 - size.Width / 2, bounds.Y + bounds.Height / 2 - size.Height / 2);
                }
                else
                {
                    string title = tabPage.Text;
                    SizeF size = pevent.Graphics.MeasureString(title, Font);
                    pevent.Graphics.DrawString(title, Font, new SolidBrush(ThemeColor), bounds.X + bounds.Width / 2 - size.Width / 2, bounds.Y + bounds.Height / 2 - size.Height / 2);
                }
            }
        }
    }
}
