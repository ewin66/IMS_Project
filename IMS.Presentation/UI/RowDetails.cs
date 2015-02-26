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

namespace Viktor.IMS.Presentation.UI
{
    public partial class RowDetails : BaseForm
    {
        public DataRow dataRow = null;
        private bool _isDirty = false;

        public RowDetails(SerialPort serialPort)
        {
            this.InitializeComponent();

            convertor = new ISO9TransliterationProvider();
            myCurrentLanguage = InputLanguage.CurrentInputLanguage;

            this._serialPort = serialPort;
            this._listener = new BarcodeListener(this);
            this.SerialEventListener_Start();
            //_listener.BarcodeScanned += this.OnBarcodeScanned;
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
            this.Close();
        }

        internal void ShowDataGridViewRow(DataRow row)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!rbDomestic.Checked && !rbForeign.Checked)
                {
                    lblError.Text = "Не е одбран тип на производ!";
                    return;
                }

                MainForm mainForm = this.Owner as MainForm;
                var updated = dataRow;
                updated["ProductName"] = textBox1.Text.Trim();
                updated["UnitPrice"] = textBox2.Text.Trim();
                updated["UnitsInStock"] = textBox3.Text.Trim();
                updated["BarCode1"] = textBox4.Text.Trim();
                updated["IsDomestic"] = rbDomestic.Checked;
                
                _repository.AddProduct(updated);
                mainForm.LastDataRow = updated;

                // Ne treba da se refresh-ra gridot se updateuva samo redot za da se vidi deka se slucila promenata.
                // mainForm.RefreshView(null);
                this.Close();
            }
            catch (System.Data.SqlClient.SqlException exc)
            {
                if (exc.Message.Contains("Barcode"))
                    lblError.Text = "Баркодот веке постои!";
                else
                    lblError.Text = exc.Message;
                return;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                return;
            }
        }

        private void RowDetails_Load(object sender, EventArgs e)
        {
            //Program.activeFormName = this.Name;
            this.AcceptButton = button1;

            textBox1.Text = dataRow["ProductName"].ToString();
            textBox2.Text = dataRow["UnitPrice"].ToString();
            textBox3.Text = dataRow["UnitsInStock"].ToString();
            bool? isDomestic = (bool?)dataRow["IsDomestic"];
            rbDomestic.Checked = (isDomestic != null && isDomestic == true);
            rbForeign.Checked = (isDomestic != null && isDomestic == false);
            textBox4.Text = dataRow["BarCode1"].ToString();
        }

        public override void OnBarcodeScanned(object sender, EventArgs e)
        {
            BarcodeScannedEventArgs be;

            be = e as BarcodeScannedEventArgs;
            if (be != null)
            {
                SetText(be.Barcode);
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
    }
}
