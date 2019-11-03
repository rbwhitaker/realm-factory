using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starbound.RealmFactory.UserInterface.ObjectPaletteInterface
{
    public class ObjectPaletteLayoutPanel : Panel
    {
        private ObjectPaletteLayoutEngine layoutEngine;

        public override System.Windows.Forms.Layout.LayoutEngine LayoutEngine
        {
            get
            {
                if (layoutEngine == null)
                {
                    layoutEngine = new ObjectPaletteLayoutEngine() { DisplayMode = DisplayMode.ImageOnly };
                }

                return layoutEngine;
            }
        }
    }
}
