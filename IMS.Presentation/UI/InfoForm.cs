using ComponentFactory.Krypton.Toolkit;

namespace Viktor.IMS.Presentation.UI
{
    public partial class InfoForm : KryptonForm
    {
        public InfoForm(string message)
        {
            InitializeComponent();
            lblInfoMessage.Text = message;
            //this.StartPosition = FormStartPosition.CenterParent;
        }
    }
}
