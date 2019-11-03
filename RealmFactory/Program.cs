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
            StarboundDefaultTheme theme = (StarboundDefaultTheme)ThemeManager.Theme;
            theme.ThemeColor = Color.Green;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SplashScreen.ShowSplashScreen();
            Application.DoEvents();
            Thread.Sleep(3000);

            try
            {
                WebServicesCommunicator.Initialize();
            }
            catch (Exception)
            {
            }

            Application.Run(new Form1());
        }
    }
}
