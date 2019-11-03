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
    public partial class LevelSettings : Form
    {
        private Level level;

        public Level Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;

                nameTextBox.Text = level.Name;
                rowsUpDown.Value = level.Settings.Rows;
                columnsUpDown.Value = level.Settings.Columns;
                cellWidthUpDown.Value = level.Settings.CellWidth;
                cellHeightUpDown.Value = level.Settings.CellHeight;
                backColorButton.BackColor = level.Settings.BackColor;
            }
        }

        public LevelSettings()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            level.Name = nameTextBox.Text;

            level.Settings = new Settings()
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
