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
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();

            InitializeText();
        }

        public void InitializeText()
        {
            string[] lines = aboutDescription.Lines;
            lines[1] = Constants.VersionString + " - " + Constants.VersionType;
            aboutDescription.Lines = lines;
            string versionType = Constants.VersionType;
            versionLabel.Text = Constants.VersionString + (versionType == "" ? "" : " - " + versionType);
        }

        private void starboundSoftwareUrlLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.starboundsoftware.com/software/realm-factory/");
        }
    }
}
