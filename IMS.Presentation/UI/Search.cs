using System;
using System.Collections.Generic;
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

namespace Viktor.IMS.Presentation.UI
{   
    public partial class Search : BaseForm
    {
        private ISO9TransliterationProvider convertor;
        private InputLanguage myCurrentLanguage;
        private bool m_bLayoutCalled = false;
        private DateTime m_dt;
        //private BarcodeListener listener;
        private int? totalArticles; 
        private int? articlesWithStock;
        private decimal? cumulativeAmount;

        public Search(SerialPort serialPort)
        {
            this.InitializeComponent();
            this.Load += new System.EventHandler(this.SearchForm_Load);

            convertor = new ISO9TransliterationProvider();
            myCurrentLanguage = InputLanguage.CurrentInputLanguage;

            this._serialPort = serialPort;
            listener = new BarcodeListener(this);
            listener.BarcodeScanned += this.OnBarcodeScanned;
            regionDataGridView.AutoGenerateColumns = false;
        }
        public void ResumeSerialEventListener()
        {
            listener.Resume();
        }
        public void RefreshView(string barcode)
        {
            if (barcode != null) textBox1.Text = null;
            
            this.regionDataGridView.DataSource = _repository.GetProducts(null, textBox1.Text, barcode, ref totalArticles, ref articlesWithStock, ref cumulativeAmount);
            this.regionDataGridView.Rows[0].Selected = true; //It is also possible to color the row backgroud

            //this.articlesBindingSource.DataSource = _repository.GetProductsTable(null, textBox1.Text, barcode, ref totalArticles, ref articlesWithStock, ref cumulativeAmount);
            //this.regionDataGridView.FirstDisplayedScrollingRowIndex = i;
            //this.regionDataGridView.CurrentRow.DefaultCellStyle.BackColor = System.Drawing.Color.Red;
        }
        private void SearchForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(KeyEvent);

            RefreshView(null);

            //resize the column once, but allow the ussers to change it.
            this.regionDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            //Program.activeFormName = this.Name;
        }

        private void regionBindingSource_PositionChanged(object sender, EventArgs e)
        {
            // if the user moves to a new row, check if the last row was changed
            BindingSource thisBindingSource = (BindingSource)sender;
            DataRow ThisDataRow = ((DataRowView)thisBindingSource.Current).Row;
            if (ThisDataRow == LastDataRow)
            {
                // we need to avoid to write a datarow to the database when it is still processed. Otherwise we get a problem
                // with the event handling of the dataTable.
                throw new ApplicationException("It seems the PositionChanged event was fired twice for the same row");
            }

            //track the current row for next PositionChanged event
            LastDataRow = ThisDataRow;
        }


        #region Form events
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetKeyboardLayout(myCurrentLanguage);
            CloseSerialConnection();
        }

        public bool CloseSerialConnection()
        {
            try
            {
                //_serialPort.Write("LOCAL\r");
                //System.Threading.Thread.Sleep(100);
                string test = _serialPort.ReadLine();
                _serialPort.DiscardInBuffer();
                _serialPort.Close();
                _serialPort.Dispose();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textBox1.SelectionStart = textBox1.Text.Length;

            //this.mikavi.Articles.DefaultView.RowFilter = "Name LIKE '*" + textBox1.Text + "*'";
            //this.articlesBindingSource.DataSource = this.mikavi.Articles.DefaultView;

            //var dataSource = new BindingList<GetArticlesResult>(_repository.GetArticlesTable(null, textBox1.Text, null));
            RefreshView(null);

        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //var ch = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            KeysConverter kc = new KeysConverter();
            string keyChar = kc.ConvertToString(e.KeyData);
            keyChar = convertor.ToMK(textBox1.Text);
            //textBox1.Text = convertor.ToMK(textBox1.Text);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            SetKeyboardLayout(GetInputLanguageByName("mk"));
        }
        private void MainForm_Activated(object sender, EventArgs e)
        {
            SetKeyboardLayout(GetInputLanguageByName("mk"));
        }
        #endregion

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            SetKeyboardLayout(myCurrentLanguage);
        }
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //Creating DataTable
            DataTable dt = new DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in regionDataGridView.Columns)
            {
                dt.Columns.Add(column.HeaderText, column.ValueType);
            }

            //Adding the Rows
            foreach (DataGridViewRow row in regionDataGridView.Rows)
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


        #region Barcode Events
        private void OnBarcodeScanned(object sender, EventArgs e)
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

        #region Helper Methods
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
        private static String RegExReplace(String inputString, String replaceValue)
        {
            String pattern = @"[\r|\n|\t]";

            // Specify your replace string value here.

            inputString = Regex.Replace(inputString, pattern, replaceValue);

            return inputString;
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

        #region Helper methods
        public static InputLanguage GetInputLanguageByName(string inputName)
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.Name.ToLower().StartsWith(inputName))
                    return lang;
            }
            return null;
        }
        public void SetKeyboardLayout(InputLanguage layout)
        {
            InputLanguage.CurrentInputLanguage = layout;
        }
        #endregion

        /// <summary>
        /// F9-Execute Order, F6-Save, F3-LookUp.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyEvent(object sender, KeyEventArgs e) //Keyup Event 
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    moveUp();
                    e.Handled = true;
                    break;
                case Keys.Down:
                    moveDown();
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    enter();
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    this.Close();
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }
        private void moveUp()
        {
            if (regionDataGridView.RowCount > 0)
            {
                if (regionDataGridView.SelectedRows.Count > 0)
                {
                    int rowCount = regionDataGridView.Rows.Count;
                    int index = regionDataGridView.SelectedCells[0].OwningRow.Index;

                    if (index == 0)
                    {
                        return;
                    }
                    //regionDataGridView.Rows[index - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    regionDataGridView.CurrentCell = regionDataGridView.Rows[index - 1].Cells[1];
                    regionDataGridView.Rows[index - 1].Selected = true;
                }
            }
        }
        private void moveDown()
        {
            if (regionDataGridView.RowCount > 0)
            {
                if (regionDataGridView.SelectedRows.Count > 0)
                {
                    int rowCount = regionDataGridView.Rows.Count;
                    //int index = regionDataGridView.SelectedCells[0].OwningRow.Index;
                    int index = regionDataGridView.CurrentRow.Index;

                    if (index == (rowCount - 1)) // include the header row
                    {
                        return;
                    }
                    //regionDataGridView.Rows[index + 1].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    regionDataGridView.CurrentCell = regionDataGridView.Rows[index + 1].Cells[1];
                    regionDataGridView.Rows[index + 1].Selected = true;
                }
            }
        }
        private void enter()
        {
            this.CurrentProduct = new Product();
            this.CurrentProduct.ProductId = Int32.Parse(regionDataGridView.CurrentRow.Cells[0].Value.ToString());
            Viktor.IMS.Presentation.UI.Sale saleForm = this.Owner as Viktor.IMS.Presentation.UI.Sale;
            listener.Pause();
            saleForm.ResumeSerialEventListener();
            this.Close();
        }

        private void regionDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.CurrentProduct = new Product();
            this.CurrentProduct.ProductId = Int32.Parse(regionDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            Viktor.IMS.Presentation.UI.Sale saleForm = this.Owner as Viktor.IMS.Presentation.UI.Sale;
            listener.Pause();
            saleForm.ResumeSerialEventListener();
            this.Close();
        }
    }
}