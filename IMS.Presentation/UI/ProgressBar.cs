using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Viktor.IMS.Presentation.UI
{
    public partial class ProgressBar : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public ProgressBar()
        {
            InitializeComponent();
        }

        // Thread-safe progress bar value-setting:

        private delegate void SetProgressCallback(int percent);
        public void SetProgress(int percent)
        {
            if (this.InvokeRequired)
            {
                SetProgressCallback d = new SetProgressCallback(SetProgress);
                this.Invoke(d, new object[] { percent });
            }
            else
            {
                this.progress.Value = percent;
                this.percentLabel.Text = percent.ToString() + "%";
            }
        }

        // Thread-safe show dialog:

        private delegate void ShowDialogCallback(Form parent);
        public void ShowDialogThreadSafe(Form parent)
        {
            if (this.InvokeRequired)
            {
                ShowDialogCallback d = new ShowDialogCallback(ShowDialogThreadSafe);
                this.Invoke(d);
            }
            else
            {
                this.ShowDialog(parent);
            }
        }

        // Thread-safe cleanup:

        private delegate void AllDoneCallback();
        public void allDone()
        {
            if (this.InvokeRequired)
            {
                AllDoneCallback d = new AllDoneCallback(allDone);
                this.Invoke(d);
            }
            else
            {
                this.Close();
                this.Dispose();
            }
        }
    }
}