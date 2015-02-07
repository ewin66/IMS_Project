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

namespace Viktor.IMS
{   
    //public partial class ArticlesTableAdapter
    //{
    //    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    //    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
    //    public virtual int Update(global::System.Data.DataRow dataRow) {
    //        return this.Adapter.Update(new global::System.Data.DataRow[] {
    //                    dataRow});
    //    }

    //}
    public partial class MainForm : Form
    {
        //private SerialPort _serialPort;
        public DataRow LastDataRow = null; //tracks for the PositionChanged event the last row
        private ISO9TransliterationProvider convertor;
        private InputLanguage myCurrentLanguage;
        private bool m_bLayoutCalled = false;
        private DateTime m_dt;
        //public string activeFormName = "";

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
        private void SplashScreen_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            if (m_bLayoutCalled == false)
            {
                m_bLayoutCalled = true;
                m_dt = DateTime.Now;
                this.Activate();
                SplashScreen.SplashScreen.CloseForm();
            }
        }
        #endregion

        RowDetails form2;
        public MainForm()
        {
            InitializeComponent();
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.SplashScreen_Layout);
            convertor = new ISO9TransliterationProvider();
            myCurrentLanguage = InputLanguage.CurrentInputLanguage;

            Program._serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }
        public void ReturnSerialEvent()
        {
            Program._serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mikavi.Articles' table. You can move, or remove it, as needed.
            this.articlesTableAdapter.Fill(this.mikavi.Articles);
            //resize the column once, but allow the ussers to change it.
            this.regionDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            Program.activeFormName = this.Name;
        }

        /// <summary>
        /// Checks if there is a row with changes and writes it to the database
        /// </summary>
        private void UpdateRowToDatabase()
        {

            if (LastDataRow != null)
            {
                if (LastDataRow.RowState == DataRowState.Modified)
                {
                    articlesTableAdapter.Update(LastDataRow);
                }
            }
        }
        //private void UpdateRowToDatabase_Barcode
        //{
        //    //articlesTableAdapter.
        //}
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

            UpdateRowToDatabase();
            //track the current row for next PositionChanged event
            LastDataRow = ThisDataRow;
        }

        private void regionDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.regionDataGridView.Rows[e.RowIndex];

            if (((DataRowView)row.DataBoundItem).Row.RowState != DataRowState.Unchanged)
            {
                row.HeaderCell.Value = "***";
            }
        }

        private void regionDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView thisGrid = (DataGridView)sender;
            BindingSource thisBindingSource = (BindingSource)thisGrid.DataSource;
            DataRow ThisDataRow = ((DataRowView)thisBindingSource.Current).Row;

            //DataTable table = (DataTable)thisGrid.DataSource;
            //DataRow ThisDataRow = table.Rows[thisGrid.CurrentCell.RowIndex];

            //Convert the current selected row in the DataGridView to a DataRow
            //DataRowView ThisDataRow = (DataRowView)regionDataGridView.CurrentRow.DataBoundItem;
            //mainDataRow = employee.Select("PLU='" + currentDataRowView[0].ToString() + "'"); //get the primary key id

            //BindingSource thisBindingSource = (BindingSource)sender;
            //DataRow ThisDataRow = ((DataRowView)thisBindingSource.Current).Row;
            if (null != ThisDataRow)
            {
                using (var f = new RowDetails { dataRow = ThisDataRow, Owner = this })
                {
                    Program._serialPort.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedHandler);
                    f.ShowDialog();
                }
                
                //form2 = new RowDetails(ThisDataRow);
                //form2.ShowDialog();

                //form2.ShowDataGridViewRow(ThisDataRow);
            }
            //do whatever you do in double click
        }
        #region Form events
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateRowToDatabase();
            SetKeyboardLayout(myCurrentLanguage);
            Program._serialPort.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textBox1.SelectionStart = textBox1.Text.Length;
            this.mikavi.Articles.DefaultView.RowFilter = "Name LIKE '*" + textBox1.Text + "*'";
            this.articlesBindingSource.DataSource = this.mikavi.Articles.DefaultView;

            //this.articlesBindingSource = this.mikavi.Articles.DefaultView;
            //regionDataGridView.DataSource = this.mikavi.Articles.DefaultView;
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

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //Thread.Sleep(500);
                SerialPort sp = (SerialPort)sender;
                string indata = sp.ReadLine();
                indata = RegExReplace(indata, String.Empty).PadLeft(13, '0'); 
                //indata = indata.Replace(System.Environment.NewLine, "");
                //SetText(indata);

                if (InvokeRequired)
                {
                    // after we've done all the processing, 
                    this.Invoke(new MethodInvoker(delegate
                    {
                        // load the control with the appropriate data
                            this.mikavi.Articles.DefaultView.RowFilter = "Bar_code = '" + indata + "'";
                            this.articlesBindingSource.DataSource = this.mikavi.Articles.DefaultView;
                    }));
                    return;
                }

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


        

    }
}