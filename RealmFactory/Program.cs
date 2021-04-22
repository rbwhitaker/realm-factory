using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Starbound.RealmFactory.UserInterface;
using Starbound.Common.WinForms.Themes;
using System.Drawing;

namespace RealmEngine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            StarboundDefaultTheme theme = (StarboundDefaultTheme)ThemeManager.Theme;
            theme.ThemeColor = Color.Green;


            SplashScreen.ShowSplashScreen();
            Application.DoEvents();
            Thread.Sleep(3000);

            Application.Run(new Form1());
        }
    }
}
