using Common.Helpers;
using Viktor.IMS.Fiscal.AccentFiscal;
using LinqDataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using Viktor.IMS.BusinessObjects;
using Viktor.IMS.BusinessObjects.Enums;
using Viktor.IMS.Presentation.Infrastructure;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Viktor.IMS.Presentation.UI
{
    public partial class Orders : BaseForm
    {
        private ProgressBar progBar;
        List<Product> orderDetails;
        CustomerType _currentCustomer;
        private NumberFormatInfo nfi;
        private int? totalArticles;
        private int? articlesWithStock;
        private decimal? cumulativeAmount;

        int rowindex;
        int colindex;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxEditingControl temp_text;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxEditingControl editigncntrl;
        private const string NRFormat = "### ### ##0.00";

        private void DataGridView1_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                rowindex = e.RowIndex;
                colindex = e.ColumnIndex;
            }
        }

        private void DataGridView1_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {
            editigncntrl = (ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxEditingControl)e.Control;
        }

        public Orders()
        {
            InitializeComponent();
            
            this.updateFont();
            PopulateComboBox();
            this.txtFromDate.Format = DateTimePickerFormat.Custom;
            this.txtToDate.Format = DateTimePickerFormat.Custom;
            this.txtFromDate.CustomFormat = "dd-MM-yyyy";
            this.txtToDate.CustomFormat = "dd-MM-yyyy";

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns["UnitPrice"].DefaultCellStyle.Format = NRFormat;
            this.dataGridView1.Columns["Price"].DefaultCellStyle.Format = NRFormat;

            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridView1_RowPrePaint);

            this.nfi = new NumberFormatInfo();
            this.nfi.NumberDecimalSeparator = ".";
            convertor = new ISO9TransliterationProvider();
            myCurrentLanguage = InputLanguage.CurrentInputLanguage;
            
            this.ActiveControl = dataGridView1;            
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        protected override void OnLoad(EventArgs e)
        {
            refreshUI();
            // do stuff before Load-event is raised
            base.OnLoad(e);
            // do stuff after Load-event was raised
        }

        private void refreshUI()
        {
            var orderDetails = _repository.GetOrderDetails(
                                txtFromDate.Value.Date.ToString("yyyy.MM.dd"),
                                txtToDate.Value.Date.AddDays(1).ToString("yyyy.MM.dd"),
                                int.Parse(CustomerComboBox.SelectedValue.ToString()),
                                null,
                                ref cumulativeAmount);
            this.dataGridView1.DataSource = orderDetails;
            if (this.dataGridView1.RowCount > 0)
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.dataGridView1.RowCount - 1].Cells[0];
            this.dataGridView1.Focus();
        }
        private void PopulateComboBox()
        {
            var dict = new Dictionary<int?, string>();
            dict.Add(0, "Сите");
            dict.Add(1, "Дома");
            dict.Add(2, "Муштерии");

            CustomerComboBox.DataSource = new BindingSource(dict, null);
            CustomerComboBox.DisplayMember = "Value";
            CustomerComboBox.ValueMember = "Key";
        }
        private void updateFont()
        {
            float fontSize = float.Parse(ConfigurationManager.AppSettings["FontSize"].ToString());
            int paddingSize = int.Parse(ConfigurationManager.AppSettings["PaddingSize"].ToString());
            //Change cell font
            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial Narrow", fontSize, FontStyle.Bold);//Arial Narrow
                c.DefaultCellStyle.Padding = new Padding(paddingSize);
            }
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial Narrow", fontSize, FontStyle.Bold);//Tahoma
            this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Red;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            //this.dataGridView1.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        private void increaseFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
            {
                float size = c.DefaultCellStyle.Font.Size;
                size += 2;
                c.DefaultCellStyle.Font = new Font("Arial Narrow", size, System.Drawing.FontStyle.Bold);
            }
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //this.dataGridView1.AutoResizeColumns();//DataGridViewAutoSizeColumnsMode.Fill
        }
        private void decreaseFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
            {
                float size = c.DefaultCellStyle.Font.Size;
                size -= 2;
                c.DefaultCellStyle.Font = new Font("Arial Narrow", size, System.Drawing.FontStyle.Bold);
            }
        }

        private void getOrdersButton_Click(object sender, EventArgs e)
        {
            refreshUI();
        }

    }
}
