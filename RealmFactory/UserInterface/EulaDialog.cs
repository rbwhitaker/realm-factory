using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RealmFactory.Properties;

namespace Starbound.RealmFactory.UserInterface
{
    public partial class EulaDialog : Form
    {
        public EulaDialog()
        {
            InitializeComponent();

            string eulaText = "REPLACE THIS"; // Resources.EULA_1_0;
            byte[] byteArray = Encoding.ASCII.GetBytes(eulaText);
            MemoryStream stream = new MemoryStream(byteArray);
            //richTextBox1.LoadFile(stream, RichTextBoxStreamType.RichText);
            richTextBox1.LoadFile(stream, RichTextBoxStreamType.PlainText);
        }
    }
}
