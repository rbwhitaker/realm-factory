using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Starbound.Common.WinForms.Wizards;

namespace Starbound.RealmFactory.UserInterface.PropertyTemplateWizard
{
    public partial class PropertyBasics : WizardPage
    {
        public PropertyBasics()
        {
            InitializeComponent();

            NextPage = typeof(BasicType);
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            bool canAdvance = (nameTextBox.Text != null && nameTextBox.Text.Length > 0);
            if (this.CanAdvance != canAdvance)
            {
                CanAdvance = canAdvance;
            }
        }
    }
}
