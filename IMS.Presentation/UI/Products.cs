using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common.Helpers;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;
using LinqDataModel;
using System.IO;
using ClosedXML.Excel;
using Viktor.IMS.Presentation.Infrastructure;
using System.Configuration;

namespace Viktor.IMS.Presentation.UI
{   
    public partial class Products : BaseForm
    {
        private bool m_bLayoutCalled = false;
        private DateTime m_dt;
        private int? totalArticles;
        private int? articlesWithStock;
        private decimal? cumulativeAmount;
        private System.Windows.Forms.Button tbAddNewProduct;

        public Products(SerialPort serialPort)
        {
            this.InitializeComponent();
            this.updateFont();

            this._serialPort = serialPort;
            this._listener = new BarcodeListener(this);
            this.SerialEventListener_Start();

            convertor = new ISO9TransliterationProvider();
            myCurrentLanguage = InputLanguage.CurrentInputLanguage;

            this.FormClosing += new FormClosingEventHandler(MainForm_Closing);
            this.ActiveControl = textBox1;
        }
        private void updateFont()
        {
            float fontSize = float.Parse(ConfigurationManager.AppSettings["FontSize"].ToString());
            int paddingSize = int.Parse(ConfigurationManager.AppSettings["PaddingSize"].ToString());
            //Change cell font
            foreach (DataGridViewColumn c in this.productsDataGridView.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial Narrow", fontSize, FontStyle.Bold);//Arial Narrow
                c.DefaultCellStyle.Padding = new Padding(paddingSize);
            }
            this.productsDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial Narrow", fontSize-4, FontStyle.Bold);//Tahoma
            this.productsDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            
            //this.productsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Red;
            this.productsDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            ////this.dataGridView1.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.productsDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        protected override void OnLoad(EventArgs e)
        {
            RefreshView(null);
            //resize the column once, but allow the ussers to change it. - Vazno: Resize se pravi otkako ke se povlecat podatocite
            this.productsDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            // do stuff before Load-event is raised
            base.OnLoad(e);
            // do stuff after Load-event was raised
            this.FormId = Program.ActiveForms.Count + 1;
            Program.ActiveForms.Add(new FormData(this, true));
        }

        public void RefreshView(string barcode)
        {
            if (barcode != null) textBox1.Text = null;
            this.articlesBindingSource.DataSource = _repository.GetProductsTable(null, textBox1.Text, barcode, ref totalArticles, ref articlesWithStock, ref cumulativeAmount);

            lblTotalArticles.Text = totalArticles.ToString();
            lblArticlesWithStock.Text = articlesWithStock.ToString();
            lblCumulativeAmount.Text = cumulativeAmount.ToString();
        }

        private void productsDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView thisGrid = (DataGridView)sender;
            BindingSource thisBindingSource = (BindingSource)thisGrid.DataSource;
            DataRow ThisDataRow = ((DataRowView)thisBindingSource.Current).Row;

            if (null != ThisDataRow)
            {
                //Izbrishi gi event handler-ite
                this.SerialEventListener_Pause();
                this._listener.RemoveDataReceivedHandler();
                using (var productDetails = new ProductDetails(this._serialPort))
                {
                    productDetails.dataRow = ThisDataRow;
                    productDetails._repository = this._repository;
                    productDetails.Owner = this;
                    productDetails.StartPosition = FormStartPosition.CenterParent;
                    productDetails.ShowDialog();
                }
                //Vrati gi event hadler-ite
                this._listener = new BarcodeListener(this);
                this.SerialEventListener_Start();
            }
        }

        #region FORM EVENTS
        private void MainForm_Activated(object sender, EventArgs e)
        {
            SetKeyboardLayout(GetInputLanguageByName("mk"));
        }
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            SetKeyboardLayout(myCurrentLanguage);
        }
        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            //Close Parent Tab
            this.Parent.Dispose();
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetKeyboardLayout(myCurrentLanguage);
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            RefreshView(null);
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            SetKeyboardLayout(GetInputLanguageByName("mk"));
        }
        
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //Creating DataTable
            DataTable dt = new DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in productsDataGridView.Columns)
            {
                dt.Columns.Add(column.HeaderText, column.ValueType);
            }

            //Adding the Rows
            foreach (DataGridViewRow row in productsDataGridView.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                }
            }

            //Exporting to Excel
            string folderPath = "C:\\Excel\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Proizvodi");
                wb.SaveAs(folderPath + "Proizvodi.xlsx");
            }
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            BindingSource thisBindingSource = (BindingSource)this.productsDataGridView.DataSource;
            DataRow NewDataRow = ((DataTable)thisBindingSource.DataSource).NewRow();
            //Izbrishi gi event handler-ite
            this.SerialEventListener_Pause();
            this._listener.RemoveDataReceivedHandler();
            using (var productDetails = new ProductDetails(this._serialPort))
            {
                productDetails.dataRow = NewDataRow;
                productDetails._repository = this._repository;
                productDetails.Owner = this;
                productDetails.StartPosition = FormStartPosition.CenterParent;
                productDetails.ShowDialog();
            }
            //Vrati gi event hadler-ite
            this._listener = new BarcodeListener(this);
            this.SerialEventListener_Start();
            RefreshView(null);
            //do whatever you do in double click
        }
        #endregion

        #region BARCODE EVENTS
        public override void OnBarcodeScanned(object sender, EventArgs e)
        {
            BarcodeScannedEventArgs be;

            be = e as BarcodeScannedEventArgs;
            if (be != null)
            {
                if (InvokeRequired)
                {
                    // after we've done all the processing, 
                    this.Invoke(new MethodInvoker(delegate
                    {
                        // load the control with the appropriate data
                        RefreshView(be.Barcode);
                    }));
                    return;
                }
                //SetText(be.Barcode);
            }
        }
        #endregion

        #region HELPER METHODS
        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }
        private void SetText2(string text)
        {
            RowDetails activeForm = (RowDetails)Form.ActiveForm;
            TextBox tbx = activeForm.Controls["textBox4"] as TextBox;
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (tbx.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText2);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                tbx.Text = text;
            }
        }
        delegate void ChangeMyTextDelegate(Control ctrl, string text);
        public static void ChangeMyText(Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                ChangeMyTextDelegate del = new ChangeMyTextDelegate(ChangeMyText);
                ctrl.Invoke(del, ctrl, text);
            }
            else
            {
                ctrl.Text = text;
            }
        }
        #endregion
    }
}