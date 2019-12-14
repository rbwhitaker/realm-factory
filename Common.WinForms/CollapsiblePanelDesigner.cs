using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using Starbound.Common.WinForms;

namespace Starbound.Common.WinForms
{
    public class CollapsiblePanelDesigner : ParentControlDesigner
    {
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);

            if (this.Control is CollapsiblePanel)
            {
                this.EnableDesignMode(((CollapsiblePanel)this.Control).WorkingArea, "WorkingArea");
            }
        }
    }
}
