using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RealmEngine
{
    public partial class HeaderPanel : UserControl
    {
        private string headerText;

        public string HeaderText
        {
            get
            {
                return headerText;
            }
            set
            {
                headerText = value;
                headerLabel.Text = headerText;
            }
        }

        public HeaderPanel()
        {
            InitializeComponent();
        }
    }
}
