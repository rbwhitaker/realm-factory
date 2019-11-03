using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Starbound.RealmFactory.DataModel;

namespace Starbound.RealmFactory.UserInterface
{
    public partial class ProjectSettings : Form
    {
        private Project project;

        public Project Project
        {
            get
            {
                return project;
            }
            set
            {
                project = value;

                rowsUpDown.Value = project.Settings.Rows;
                columnsUpDown.Value = project.Settings.Columns;
                cellWidthUpDown.Value = project.Settings.CellWidth;
                cellHeightUpDown.Value = project.Settings.CellHeight;
                backColorButton.BackColor = project.Settings.BackColor;
            }
        }

        public ProjectSettings()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            project.Settings = new Settings()
            {
                BackColor = backColorButton.BackColor,
                Rows = (int)rowsUpDown.Value,
                Columns = (int)columnsUpDown.Value,
                CellWidth = (int)cellWidthUpDown.Value,
                CellHeight = (int)cellHeightUpDown.Value
            };
        }

        private void backColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = backColorButton.BackColor;
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    backColorButton.BackColor = colorDialog.Color;
                }
            }
        }
    }
}
