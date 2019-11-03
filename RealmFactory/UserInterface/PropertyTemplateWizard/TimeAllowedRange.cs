using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Starbound.Common.WinForms.Wizards;

namespace Starbound.RealmFactory.UserInterface.PropertyTemplateWizard
{
    public partial class TimeAllowedRange : WizardPage
    {
        public TimeAllowedRange()
        {
            InitializeComponent();
        }

        private void descriptionTextBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string text = textBox.Text;

            text = text.ToLower();

            DateTime? time = ParseDateTime(text);

            if (time == null) { e.Cancel = true; }
        }

        private DateTime? ParseDateTime(string text)
        {
            if (Regex.Matches(text, @"\d+:\d+:\d+\.\d+") != null)
            {

            }

            return null;
        }
    }
}
