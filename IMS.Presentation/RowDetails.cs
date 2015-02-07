using Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Viktor.IMS.Presentation
{
    public partial class RowDetails : BaseForm
    {
        public DataRow dataRow = null;
        private bool _isDirty = false;
        private BarcodeListener listener;

        public RowDetails(SerialPort serialPort)
        {
            this.InitializeComponent();

            convertor = new ISO9TransliterationProvider();
            myCurrentLanguage = InputLanguage.CurrentInputLanguage;

            this._serialPort = serialPort;
            listener = new BarcodeListener(this);
            listener.BarcodeScanned += this.OnBarcodeScanned;
        }
        public RowDetails(DataRow prodDetails)
        {
            InitializeComponent();
            // Custom events
            //this.textBox1.Enter += new System.EventHandler(base.textBox_Enter);
            //this.Activated += new System.EventHandler(base.Form_Activated);

            dataRow = prodDetails;
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            MainForm m = this.Owner as MainForm;
            listener.Pause();
            m.ResumeSerialEventListener();
            this.Close();
        }
        internal void ShowDataGridViewRow(DataRow row)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm m = this.Owner as MainForm;
            var updated = dataRow;
            updated["Name"] = textBox1.Text.Trim();
            updated["Price"] = textBox2.Text.Trim();
            updated["Stock"] = textBox3.Text.Trim();
            updated["Bar_code"] = textBox4.Text.Trim();
            m.LastDataRow = updated;
            //m.articlesTableAdapter.Update(updated);
            
            _repository.AddArticle(updated);
            m.RefreshView(null);
            listener.Pause();
            m.ResumeSerialEventListener();
            Close();
        }


        private void RowDetails_Load(object sender, EventArgs e)
        {
            //Program.activeFormName = this.Name;
            this.AcceptButton = button1;

            textBox1.Text = dataRow["Name"].ToString();
            textBox2.Text = dataRow["Price"].ToString();
            textBox3.Text = dataRow["Stock"].ToString();
            textBox4.Text = dataRow["Bar_code"].ToString();
        }

        private void OnBarcodeScanned(object sender, EventArgs e)
        {
            BarcodeScannedEventArgs be;

            be = e as BarcodeScannedEventArgs;
            if (be != null)
            {
                this.textBox4.Text = be.Barcode;
                //SetText(be.Barcode);
            }
        }
        private void DataReceivedHandler2(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //Thread.Sleep(500);
                SerialPort sp = (SerialPort)sender;
                string indata = sp.ReadLine();
                indata = RegExReplace(indata, String.Empty).PadLeft(13, '0');
                //indata = indata.Replace(System.Environment.NewLine, "");
                SetText(indata);


                /*
                //Load data correspondin to "MyName"
                //Populate a globale variable List<string> which will be
                //bound to grid at some later stage
                if (InvokeRequired)
                {
                    // after we've done all the processing, 
                    this.Invoke(new MethodInvoker(delegate
                    {
                        // load the control with the appropriate data
                        if (Program.activeFormName == "MainForm")
                        {
                            this.mikavi.Articles.DefaultView.RowFilter = "Bar_code = '" + indata + "'";
                            this.articlesBindingSource.DataSource = this.mikavi.Articles.DefaultView;
                        }
                        else
                        {
                            RowDetails activeForm = (RowDetails)Form.ActiveForm;
                            TextBox tbx = activeForm.Controls["textBox4"] as TextBox;
                            tbx.Text = indata;
                        }
                    }));
                    return;
                }
                */

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox4.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox4.Text = text;
            }
        }
        private static String RegExReplace(String inputString, String replaceValue)
        {
            String pattern = @"[\r|\n|\t]";

            // Specify your replace string value here.

            inputString = Regex.Replace(inputString, pattern, replaceValue);

            return inputString;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            base.textBox_Enter(sender, e);
        }

        private void RowDetails_Activated(object sender, EventArgs e)
        {
            base.Form_Activated(sender, e);
        }

        //private void RowDetails_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        button1.PerformClick();
        //    }
        //}
    }
}
