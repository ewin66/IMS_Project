using Common.Helpers;
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
        private decimal? cumulativeProfit;
        
        private NumberFormatInfo nfi;
        private const string NRFormat = "### ### ##0.00";

        public Report()
        {
            InitializeComponent();
            PopulateComboBox();
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.AutoGenerateColumns = false;
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridView1.ColumnHeadersDefaultCellStyle = cellStyle;
            this.dataGridView1.Columns["Total"].DefaultCellStyle.Format = NRFormat;
            this.dataGridView1.Focus();

            this.txtFromDate.Format = DateTimePickerFormat.Custom;
            this.txtToDate.Format = DateTimePickerFormat.Custom;
            this.txtFromDate.CustomFormat = "dd-MM-yyyy";
            this.txtToDate.CustomFormat = "dd-MM-yyyy";
            this.txtToDate.Value = DateTime.Today.AddDays(1);

            this.nfi = new NumberFormatInfo();
            this.nfi.NumberDecimalSeparator = ".";

            //this.chart1.Series[0].Points.DataBind(((DataView)DataGridView1.DataSource, "SignalStength");
            //this.chart1.Series[0].Points.DataBindY((DataView)DataGridView1.DataSource, "SignalStength");
            //this.reportDataBar.DataBindings = 
            //this.reportDataBar.
        }
        private void refreshUI()
        {
            var report = _repository.GetReport(
                                txtFromDate.Value.Date.ToString("yyyy.MM.dd"), 
                                txtToDate.Value.Date.ToString("yyyy.MM.dd"),
                                int.Parse(CustomerComboBox.SelectedValue.ToString()),
                                false, 
                                ref cumulativeAmount,
                                ref cumulativeProfit);
            lblCumulativeTotal.Text = decimal.Round((decimal)cumulativeAmount).ToString("N2", nfi);
            lblCumulativeProfit.Text = decimal.Round((decimal)cumulativeProfit).ToString("N2", nfi);
            SortableBindingList<LinqDataModel.GetReportResult> sortableReport = new SortableBindingList<LinqDataModel.GetReportResult>(report);
            this.dataGridView1.DataSource = sortableReport;
            this.dataGridView1.Focus();

            this.chart1.Legends.FirstOrDefault().Enabled = false;
            this.chart1.Series[0].Points.DataBind(sortableReport, "ProductName", "Total", "");
            //this.chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            //this.chart1.ChartAreas[0].AxisX.IsLabelAutoFit = false;
            this.chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            
            //lblTotalValue.Text = decimal.Round(((decimal)orderDetails.Sum(x => x.Price))).ToString("N2", nfi);

            /// Add empty product to create and empty row
            //orderDetails.Add(new Product());

            //this.dataGridView1.DataSource = orderDetails.ToArray();

            /// Set focus
            //dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1];
            //dataGridView1.CurrentCell.ReadOnly = false;
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

        private void showReportButton_Click(object sender, EventArgs e)
        {
            refreshUI();
        }
    }
}
