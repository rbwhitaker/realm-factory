using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RealmEngine
{
    public partial class SubmitFeatureRequestDialog : Form
    {
        public SubmitFeatureRequestDialog()
        {
            InitializeComponent();

            EmptyifyDescription();
            EmptyifyPurposeSteps();
        }

        public FeatureRequestReport FeatureRequestReport
        {
            get
            {
                return new FeatureRequestReport()
                {
                    Description = GetDescriptionText(),
                    Reason = GetPurposeText()
                };
            }
        }

        private bool descriptionEmpty = true;
        private bool purposeEmpty = true;

        private string GetDescriptionText()
        {
            if (descriptionEmpty) { return ""; }
            return descriptionTextBox.Text;
        }

        private string GetPurposeText()
        {
            if (purposeEmpty) { return ""; }
            return purposeTextBox.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void descriptionTextBox_MouseCaptureChanged(object sender, EventArgs e)
        {
        }

        public void EmptyifyPurposeSteps()
        {
            purposeTextBox.Text = "Describe what you'd be able to accomplish with this new feature, and how that would help you make awesome levels.  We get quite a few feature requests, and supplying this information really helps us understand the value in this new feature.";
            purposeTextBox.ForeColor = Color.FromArgb(200, 200, 200);
            purposeEmpty = true;
        }

        public void EmptyifyDescription()
        {
            descriptionTextBox.Text = "Describe the feature you'd like to see in the next version of this program.";
            descriptionTextBox.ForeColor = Color.FromArgb(200, 200, 200);
            descriptionEmpty = true;
        }

        private void descriptionTextBox_Leave(object sender, EventArgs e)
        {
            if (descriptionTextBox.Text == "")
            {
                EmptyifyDescription();
            }
        }

        private void descriptionTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (descriptionEmpty)
            {
                descriptionTextBox.Text = "";
                descriptionTextBox.ForeColor = Color.Black;
                descriptionEmpty = false;
            }
        }

        private void purposeTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (purposeEmpty)
            {
                purposeTextBox.Text = "";
                purposeTextBox.ForeColor = Color.Black;
                purposeEmpty = false;
            }
        }

        private void reproductionStepsTextBox_Leave(object sender, EventArgs e)
        {
            if (purposeTextBox.Text == "")
            {
                EmptyifyPurposeSteps();
            }
        }

        private void SubmitBugDialog_MouseClick(object sender, MouseEventArgs e)
        {
            this.Focus();
        }
    }
}
