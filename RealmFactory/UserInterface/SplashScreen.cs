using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starbound.RealmFactory.UserInterface
{
    public partial class SplashScreen : Form
    {
        private static SplashScreen splashScreen = new SplashScreen();

        public static void ShowSplashScreen()
        {
            splashScreen.Visible = true;
        }

        public static void HideSplashScreen()
        {
            splashScreen.Visible = false;
        }

        private SplashScreen()
        {
            InitializeComponent();
            InitializeText();
        }


        public void InitializeText()
        {
            string versionType = Constants.VersionType;
            versionLabel.Text = Constants.VersionString + (versionType == "" ? "" : " - " + versionType);
        }
    }
}
