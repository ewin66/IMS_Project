using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Viktor.IMS.BusinessObjects.Enums;

namespace Viktor.IMS.Presentation.UI
{
    public partial class Report : BaseForm
    {
        private decimal? cumulativeAmount;
        private NumberFormatInfo nfi;
        private const string NRFormat = "### ### ##0.00";

        public Report()
        {
            InitializeComponent();
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.AutoGenerateColumns = false;
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle = cellStyle;
            this.dataGridView1.Columns["Total"].DefaultCellStyle.Format = NRFormat;

            this.nfi = new NumberFormatInfo();
            this.nfi.NumberDecimalSeparator = ".";
        }
        private void refreshUI()
        {
            var report = _repository.GetReport(
                                txtFromDate.Value.Date.ToString("yyyy.MM.dd"), 
                                txtToDate.Value.Date.AddDays(1).ToString("yyyy.MM.dd"), 
                                (int)CustomerType.HOME, 
                                false, 
                                ref cumulativeAmount);
            lblCumulativeTotal.Text = decimal.Round((decimal)cumulativeAmount).ToString("N2", nfi);
            this.dataGridView1.DataSource = report;

            //lblTotalValue.Text = decimal.Round(((decimal)orderDetails.Sum(x => x.Price))).ToString("N2", nfi);

            /// Add empty product to create and empty row
            //orderDetails.Add(new Product());

            //this.dataGridView1.DataSource = orderDetails.ToArray();

            /// Set focus
            //dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1];
            //dataGridView1.CurrentCell.ReadOnly = false;
        }

        private void showReportButton_Click(object sender, EventArgs e)
        {
            refreshUI();
        }
    }
}
