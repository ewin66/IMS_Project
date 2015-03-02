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
        private const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                // add the drop shadow flag for automatically drawing
                // a drop shadow around the form
                CreateParams cp = base.CreateParams;
                if (OSFeature.IsPresent(SystemParameter.DropShadow))
                {
                    cp.ClassStyle |= CS_DROPSHADOW;
                }
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        public InfoDialog(string message, bool showYesNo)
        {
            InitializeComponent();
            lblMessage.Text = message;
            button1.Visible = showYesNo;
            //button2.Visible = showYesNo;
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            //imgProgress.Image = Properties.Resources.Loader_blue_48;
            //System.Threading.Thread.Sleep(1000);
            //imgProgress.Image = Properties.Resources.Ok64_Green;
            //this.Refresh();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
        public void SetImage(string imageType)
        {
            switch (imageType)
            {
                case "progress":
                    imgProgress.Image = Properties.Resources.Loader_blue_48;
                    System.Threading.Thread.Sleep(3000);
                    imgProgress.Image = Properties.Resources.Ok64_Green;
                    break;
                case "ok":
                    imgProgress.Image = Properties.Resources.Ok64_Green;
                    break;
                default:
                    imgProgress.Image = null;
                    break;
            }
            
        }
    }
}
