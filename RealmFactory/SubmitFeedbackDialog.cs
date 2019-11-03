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
    public partial class SubmitFeedbackDialog : Form
    {
        public SubmitFeedbackDialog()
        {
            InitializeComponent();

            EmptyifyDescription();
        }

        public FeedbackReport FeedbackReport
        {
            get
            {
                return new FeedbackReport()
                {
                    FeedbackText = GetFeedbackText()
                };
            }
        }

        private bool feedbackEmpty = true;

        private string GetFeedbackText()
        {
            if (feedbackEmpty) { return ""; }
            return feedbackTextBox.Text;
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

        public void EmptyifyDescription()
        {
            feedbackTextBox.Text = "Enter your thoughts on this program here, and let us know what we're doing right and wrong.  Your feedback gives us the opportunity to make improvements to this software.";
            feedbackTextBox.ForeColor = Color.FromArgb(200, 200, 200);
            feedbackEmpty = true;
        }

        private void descriptionTextBox_Leave(object sender, EventArgs e)
        {
            if (feedbackTextBox.Text == "")
            {
                EmptyifyDescription();
            }
        }

        private void descriptionTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (feedbackEmpty)
            {
                feedbackTextBox.Text = "";
                feedbackTextBox.ForeColor = Color.Black;
                feedbackEmpty = false;
            }
        }


        private void SubmitBugDialog_MouseClick(object sender, MouseEventArgs e)
        {
            this.Focus();
        }
    }
}
