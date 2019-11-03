using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Layout;
using System.Windows.Forms;

namespace Starbound.RealmFactory.UserInterface.ObjectPaletteInterface
{
    public class ObjectPaletteLayoutEngine : LayoutEngine
    {
        public DisplayMode DisplayMode { get; set; }
        public int Columns { get; set; }
        public int RowHeight { get; set; }
        public int Spacing { get; set; }

        public ObjectPaletteLayoutEngine()
        {
            Columns = 4;
            RowHeight = 50;
            Spacing = 2;
        }

        public override void InitLayout(object child, System.Windows.Forms.BoundsSpecified specified)
        {
            base.InitLayout(child, specified);
        }

        public override bool Layout(object container, System.Windows.Forms.LayoutEventArgs layoutEventArgs)
        {
            if (DisplayMode == UserInterface.DisplayMode.ImageOnly) { return LayoutGrid(container, layoutEventArgs); }
            else return LayoutList(container, layoutEventArgs);
        }

        public bool LayoutGrid(object container, System.Windows.Forms.LayoutEventArgs layoutEventArgs)
        {
            Panel panel = (Panel)container;
            System.Windows.Forms.Control.ControlCollection controls = panel.Controls;

            for(int index = 0; index < controls.Count; index++)
            {
                Control control = controls[index];
                control.Width = RowHeight;
                control.Height = RowHeight;

                int row = index / Columns;
                int column = index % Columns;
                control.Location = new System.Drawing.Point(column * (RowHeight + Spacing) + Spacing, row * (RowHeight + Spacing) + Spacing);
                ((ObjectPaletteItem)control).DisplayMode = DisplayMode;
            }

            Control last = panel.Controls.Count > 0 ? panel.Controls[panel.Controls.Count - 1] : null;

            panel.Width = Columns * (RowHeight + Spacing) + Spacing;
            int totalRows = controls.Count == 0 ? 0 : ((controls.Count - 1) / Columns) + 1;
            panel.Height = totalRows * (RowHeight + Spacing) + Spacing;

            return true;
        }

        public bool LayoutList(object container, System.Windows.Forms.LayoutEventArgs layoutEventArgs)
        {
            Panel panel = (Panel)container;

            System.Windows.Forms.Control.ControlCollection controls = panel.Controls;

            for (int index = 0; index < controls.Count; index++)
            {
                Control control = controls[index];
                control.Width = panel.ClientSize.Width - Spacing * 2;
                control.Height = RowHeight;
                control.Location = new System.Drawing.Point(Spacing, index * (RowHeight + Spacing) + Spacing);
                ((ObjectPaletteItem)control).DisplayMode = DisplayMode;
            }

            int totalRows = controls.Count;
            panel.Height = totalRows * (RowHeight + Spacing) + Spacing;

            return true;
        }
    }
}
