using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.WinForms.Themes
{
    public class ThemeManager
    {
        public static Theme Theme { get; set; }

        static ThemeManager()
        {
            Theme = new StarboundDefaultTheme();
        }
    }
}
