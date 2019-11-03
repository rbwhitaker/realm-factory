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
    public partial class SubmitBugDialog : Form
    {
        public SubmitBugDialog()
        {
            InitializeComponent();

            severityComboBox.SelectedIndex = 0;
            EmptyifyDescription();
            EmptyifyReproductionSteps();
        }

        public BugReport BugReport
        {
            get
            {
                return new BugReport()
                {
                    Severity = (BugSeverity)severityComboBox.SelectedIndex,
                    Description = GetDescriptionText(),
                    ReproductionSteps = GetReproductionText()
                };
            }
        }

        private bool descriptionEmpty = true;
        private bool reproductionStepsEmpty = true;

        private string GetDescriptionText()
        {
            if (descriptionEmpty) { return ""; }
            return descriptionTextBox.Text;
        }

        private string GetReproductionText()
        {
            if (reproductionStepsEmpty) { return ""; }
            return reproductionStepsTextBox.Text;
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

        public void EmptyifyReproductionSteps()
        {
            reproductionStepsTextBox.Text = "If possible, please describe how someone else could repeat the problem.  Doing this makes it much easier for us to find and fix the problem.";
            reproductionStepsTextBox.ForeColor = Color.FromArgb(200, 200, 200);
            reproductionStepsEmpty = true;
        }

        public void EmptyifyDescription()
        {
            descriptionTextBox.Text = "Describe what problem you're having, and how it is preventing you from doing what you want, or how it is different from what you expected it to do.";
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

        private void reproductionStepsTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (reproductionStepsEmpty)
            {
                reproductionStepsTextBox.Text = "";
                reproductionStepsTextBox.ForeColor = Color.Black;
                reproductionStepsEmpty = false;
            }
        }

        private void reproductionStepsTextBox_Leave(object sender, EventArgs e)
        {
            if (reproductionStepsTextBox.Text == "")
            {
                EmptyifyReproductionSteps();
            }
        }

        private void SubmitBugDialog_MouseClick(object sender, MouseEventArgs e)
        {
            this.Focus();
        }
    }
}
