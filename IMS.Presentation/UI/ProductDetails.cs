using Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Viktor.IMS.Presentation.UI
{
    public partial class ProductDetails : BaseForm
    {
        public DataRow dataRow = null;
        private bool _isDirty = false;

        public ProductDetails(SerialPort serialPort)
        {
            this.InitializeComponent();

            convertor = new ISO9TransliterationProvider();
            myCurrentLanguage = InputLanguage.CurrentInputLanguage;

            this._serialPort = serialPort;
            this._listener = new BarcodeListener(this);
            this.SerialEventListener_Start();
            //_listener.BarcodeScanned += this.OnBarcodeScanned;
        }

        public ProductDetails(DataRow prodDetails)
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

                Products products = this.Owner as Products;
                var updated = dataRow;
                updated["ProductName"] = textBox1.Text.Trim();
                updated["UnitPrice"] = textBox2.Text.Trim();
                updated["UnitPurchasePrice"] = txtUnitPurchasePrice.Text.Trim();
                updated["UnitsInStock"] = textBox3.Text.Trim();
                updated["BarCode1"] = textBox4.Text.Trim();
                updated["IsDomestic"] = rbDomestic.Checked;
                if (comboCategory.SelectedIndex != 0)
                {
                    updated["CategoryId"] = comboCategory.SelectedValue;
                }
                updated["ReorderLevel"] = txtReorderLevel.Text.Trim();

                _repository.AddProduct(updated);
                products.LastDataRow = updated;

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
            txtUnitPurchasePrice.Text = dataRow["UnitPurchasePrice"].ToString();
            textBox3.Text = dataRow["UnitsInStock"].ToString();
            bool? isDomestic = dataRow["IsDomestic"].ToString() == "" ? true : (bool?)dataRow["IsDomestic"];
            rbDomestic.Checked = (isDomestic != null && isDomestic == true);
            rbForeign.Checked = (isDomestic != null && isDomestic == false);
            textBox4.Text = dataRow["BarCode1"].ToString();
            var categories = _repository.GetCategories();
            var categoryId = DataHelper.GetNullableInt(dataRow["CategoryId"]);
            categories.Insert(0, new LinqDataModel.GetCategoriesResult { CategoryId = -1, CategoryName = "-- Избери категорија --" });
            categories.Add(new LinqDataModel.GetCategoriesResult { CategoryId = -1000, CategoryName = "-- Додај нова категорија --" });
            comboCategory.Refresh();
            comboCategory.DataSource = categories;
            comboCategory.DisplayMember = "CategoryName";
            comboCategory.ValueMember = "CategoryId";
            //comboCategory.Items.Insert(0, "Избери категорија");
            if (categoryId != null)
            {
                comboCategory.SelectedValue = categoryId;
            }
            txtReorderLevel.Text = dataRow["ReorderLevel"].ToString();
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

        private void btnAddNewCategory_Click(object sender, EventArgs e)
        {
            var selectedItem = (LinqDataModel.GetCategoriesResult)comboCategory.SelectedItem;
            if (selectedItem == null)
            {
                var addCategoryResult = _repository.AddCategory(comboCategory.Text).FirstOrDefault();
                var categories = _repository.GetCategories();
                var categoryId = addCategoryResult.CategoryId;
                //categories.Insert(0, new LinqDataModel.GetCategoriesResult { CategoryId = -1, CategoryName = "-- Избери категорија --" });
                categories.Add(new LinqDataModel.GetCategoriesResult { CategoryId = -1000, CategoryName = "-- Додај нова категорија --" });
                comboCategory.Refresh();
                comboCategory.DataSource = categories;
                comboCategory.DisplayMember = "CategoryName";
                comboCategory.ValueMember = "CategoryId";
                //comboCategory.Items.Insert(0, "Избери категорија");
                if (categoryId != null)
                {
                    comboCategory.SelectedValue = categoryId;
                }
            }
        }
    }
}
