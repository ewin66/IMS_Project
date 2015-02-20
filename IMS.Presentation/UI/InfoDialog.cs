using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Viktor.IMS.Presentation.UI
{
    public partial class InfoDialog : Form
    {
        public InfoDialog(string message, bool showYesNo)
        {
            InitializeComponent();
            lblMessage.Text = message;
            button1.Visible = showYesNo;
            button2.Visible = showYesNo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
