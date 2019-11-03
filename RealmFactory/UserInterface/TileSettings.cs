using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Starbound.RealmFactory.DataModel;
using Starbound.RealmFactory.UserInterface.PropertyTemplateWizard;
using Starbound.Common.WinForms.Wizards;

namespace Starbound.RealmFactory.UserInterface
{
    public partial class TypeSettings : Form
    {
        private GameObjectType type;

        public GameObjectType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;

                nameTextBox.Text = type.Name;
                tileImagePictureBox.Image = ((ImageObject2DType)type).Image;
            }
        }

        public TypeSettings()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            type.Name = nameTextBox.Text;
            ((ImageObject2DType)type).Image = (Bitmap)tileImagePictureBox.Image;
        }

        private OpenFileDialog imageFileDialog;

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (imageFileDialog == null)
            {
                imageFileDialog = new OpenFileDialog();
                imageFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.tga;*.tif;*.gif|All Files|*.*";
            }

            if (imageFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tileImagePictureBox.Image = Bitmap.FromFile(imageFileDialog.FileName);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            List<WizardPage> pages = new List<WizardPage>();
            pages.Add(new PropertyBasics());
            pages.Add(new BasicType());
            pages.Add(new TimeAllowedRange());
            Starbound.Common.WinForms.Wizards.Wizard.ShowWizard("New Property Wizard", new PropertyTemplateWizardHeader(), pages, typeof(PropertyBasics));
        }
    }
}
