using Common.Helpers;
using IMS.Fiscal.AccentFiscal;
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

namespace Viktor.IMS.Presentation.UI
{
    public partial class Sale : BaseForm
    {
        List<Product> orderDetails;
        //private BarcodeListener listener;
        private NumberFormatInfo nfi;
        private SY50 _fiscalPrinter { get; set; }

        public Sale()
        {
            InitializeComponent();
            lblCurrentProduct.Text = string.Empty;
            lblTotalValue.Text = "0.00";
            this.nfi = new NumberFormatInfo();
            this.nfi.NumberDecimalSeparator = ".";
            convertor = new ISO9TransliterationProvider();
            myCurrentLanguage = InputLanguage.CurrentInputLanguage;
            listener = new BarcodeListener(this);
            listener.BarcodeScanned += this.OnBarcodeScanned;
            
            InitializeFiscalPrinter();
            
            
            dataGridView1.AutoGenerateColumns = false;
            orderDetails = new List<Product>();

            this.ActiveControl = dataGridView1;
        }

        #region OldMethod
        /*
        private void InitializeFiscalPrinter2()
        {
            try
            {
                if (_fiscalPrinter == null)
                {
                    HardwareConfigurationSection config;
                    HardwareConfigurationElementCollection hardwareIdsConfig;
                    List<KeyValuePair<string, string>> hardware;

                    config = HardwareConfigurationSection.GetConfiguration();
                    hardwareIdsConfig = config.HardwareIds;
                    hardware = new List<KeyValuePair<string, string>>();

                    foreach (HardwareConfigurationElement hardwareId in hardwareIdsConfig)
                    {
                        hardware.Add(new KeyValuePair<string, string>(hardwareId.Name, hardwareId.Id));
                    }

                    string VID = hardware.FirstOrDefault(x => x.Key == "FiscalPrinter").Value.Split('&')[0].Replace("VID_", "");
                    string PID = hardware.FirstOrDefault(x => x.Key == "FiscalPrinter").Value.Split('&')[1].Replace("PID_", "");
                    var ports = Common.Helpers.DeviceHelper.GetPortByVPid(VID, PID).Distinct(); //("067B", "2303")
                    var portName = SerialPort.GetPortNames().Intersect(ports).FirstOrDefault();
                    var baudRate = 9600;
                    _fiscalPrinter = new PF550(portName, baudRate);
                    _fiscalPrinter.OpenPort();
                    Program.IsFiscalPrinterConnected = true;
                    MessageBox.Show("Успешно поврзување со касата, на port :: " + portName);
                }

            }
            catch (Exception ex)
            {
                Program.IsFiscalPrinterConnected = false;
                SplashScreen.SplashScreen.CloseForm();
                MessageBox.Show(this, "Неуспешно поврзување со Фискалната каса, проверете дали е приклучена!\n\nOpening serial port result :: " + ex.Message, "Информација!");
            }
        }
        */
        #endregion

        private void InitializeFiscalPrinter()
        {
            try
            {
                if (!Program.IsFiscalPrinterConnected) return;
                if (_fiscalPrinter == null)
                {
                    HardwareConfigurationSection config;
                    HardwareConfigurationElementCollection hardwareIdsConfig;
                    List<KeyValuePair<string, string>> hardware;

                    config = HardwareConfigurationSection.GetConfiguration();
                    hardwareIdsConfig = config.HardwareIds;
                    hardware = new List<KeyValuePair<string, string>>();

                    foreach (HardwareConfigurationElement hardwareId in hardwareIdsConfig)
                    {
                        hardware.Add(new KeyValuePair<string, string>(hardwareId.Name, hardwareId.Id));
                    }

                    string VID = hardware.FirstOrDefault(x => x.Key == "FiscalPrinter").Value.Split('&')[0].Replace("VID_", "");
                    string PID = hardware.FirstOrDefault(x => x.Key == "FiscalPrinter").Value.Split('&')[1].Replace("PID_", "");
                    var ports = Common.Helpers.DeviceHelper.GetPortByVPid(VID, PID).Distinct(); //("067B", "2303")
                    var portName = SerialPort.GetPortNames().Intersect(ports).FirstOrDefault();
                    this.CheckPort(portName);
                    _fiscalPrinter = new SY50(portName);
                    Program.IsFiscalPrinterConnected = true;
                    //MessageBox.Show("Успешно поврзување со касата, на port :: " + portName);
                }

            }
            catch (Exception ex)
            {
                Program.IsFiscalPrinterConnected = false;
                SplashScreen.SplashScreen.CloseForm();
                MessageBox.Show(this, "Неуспешно поврзување со Фискалната каса, проверете дали е приклучена!\n\nOpening serial port result :: " + ex.Message, "Информација!");
            }
        }

        

        #region BARCODE EVENTS
        public void ResumeSerialEventListener()
        {
            listener.Resume();
        }
        public void AddProduct(string barcode)
        {
            if (barcode != null)
            {
                var product = _repository.GetProduct(null, barcode, null);
                var query = orderDetails.Where(x => x.ProductId == product.ProductId);
                if (query.Count() > 0)
                {
                    ++query.Single().Quantity;
                    query.Single().Price = query.Single().Quantity * query.Single().UnitPrice;
                    this.refreshUI(query.Single());
                }
                else
                {
                    orderDetails.Add(product);
                    this.refreshUI(product);
                }
            }
        }
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
                        AddProduct(be.Barcode);
                    }));
                    return;
                }
                //SetText(be.Barcode);
            }
        }
        #endregion

        #region FORM EVENTS
        private void Sale_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(KeyEvent);
        }
        /// <summary>
        /// F9-Execute Order, F6-Save, F3-LookUp.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyEvent(object sender, KeyEventArgs e) //Keyup Event 
        {
            switch (e.KeyCode)
            {
                case Keys.F4:
                    listener.Pause();
                    using (var searchForm = new Search(this._serialPort))
                    {
                        searchForm._repository = this._repository;
                        searchForm.Owner = this;
                        searchForm.StartPosition = FormStartPosition.CenterParent;
                        searchForm.ShowDialog();
                        var itemNumber = 0;
                        if (searchForm.CurrentProduct != null && searchForm.CurrentProduct.ProductId > 0)
                        {
                            /// Add Product to LIST
                            /// ===================
                            var query = orderDetails.Where(x => x.ProductId == searchForm.CurrentProduct.ProductId);
                            if (query.Count() > 0)
                            {
                                ++query.Single().Quantity;
                                query.Single().Price = query.Single().Quantity * query.Single().UnitPrice;
                                itemNumber = query.Single().ItemNumber;
                                this.refreshUI(query.Single());
                            }
                            else
                            {
                                var product = _repository.GetProduct(searchForm.CurrentProduct.ProductId, null, null);
                                product.ItemNumber = itemNumber = this.orderDetails.Count + 1;
                                this.orderDetails.Add(product);
                                this.refreshUI(product);
                            }
                            //this.RefreshUI(product);
                            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[itemNumber - 1].Cells["Quantity"];
                        }
                        e.Handled = true;
                    }
                    break;

                /// Pecatenje na fisskalna smetka
                case Keys.F9:
                    ExecuteOrder(true);
                    e.Handled = true;
                    break;

                /// Bez pecatenje na fiskalana smetka
                case Keys.Space:
                    ExecuteOrder(false);
                    e.Handled = true;
                    break;

                case Keys.Delete:
                    delete_Click();
                    e.Handled = true;
                    break;
                case Keys.Up:
                    moveUp();
                    e.Handled = true;
                    break;
                case Keys.Down:
                    moveDown();
                    e.Handled = true;
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Quantity" ||
                dataGridView1.Columns[e.ColumnIndex].Name == "UnitPrice")
            {
                var query = orderDetails.Where(x => x.ItemNumber == e.RowIndex + 1);
                query.Single().Quantity = decimal.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                query.Single().Price = Math.Round(query.Single().Quantity * query.Single().UnitPrice, 2);
                this.refreshUI(query.Single());
            }
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {

        }
        #endregion

        



        private void moveUp()
        {
            if (dataGridView1.RowCount > 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowCount = dataGridView1.Rows.Count;
                    int index = dataGridView1.SelectedCells[0].OwningRow.Index;

                    if (index == 0)
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = dataGridView1.Rows;

                    // remove the previous row and add it behind the selected row.
                    DataGridViewRow prevRow = rows[index - 1];
                    rows.Remove(prevRow);
                    prevRow.Frozen = false;
                    rows.Insert(index, prevRow);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[index - 1].Selected = true;
                }
            }
        }
        private void moveDown()
        {
            if (dataGridView1.RowCount > 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowCount = dataGridView1.Rows.Count;
                    int index = dataGridView1.SelectedCells[0].OwningRow.Index;

                    if (index == (rowCount - 2)) // include the header row
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = dataGridView1.Rows;

                    // remove the next row and add it in front of the selected row.
                    DataGridViewRow nextRow = rows[index + 1];
                    rows.Remove(nextRow);
                    nextRow.Frozen = false;
                    rows.Insert(index, nextRow);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[index + 1].Selected = true;
                }
            }
        }
        private void refreshUI(Product product)
        {
            if (product != null)
            {
                lblCurrentProduct.Text = string.Format("{0} {1} ком x {2} = {3}", product.ProductName, product.Quantity, ((decimal)product.UnitPrice).ToString("N2", nfi), ((decimal)product.Price).ToString("N2", nfi));
            }
            else
            {
                lblCurrentProduct.Text = "";
            }
            lblTotalValue.Text = ((decimal)orderDetails.Sum(x => x.Price)).ToString("N2", nfi);
            this.dataGridView1.DataSource = orderDetails.ToArray();
        }
        private void delete_Click()
        {
            var rowIndex = this.dataGridView1.CurrentCell.RowIndex;
            if (orderDetails.Any(x => x.ItemNumber == rowIndex + 1))
                orderDetails.RemoveAt(rowIndex);
            refreshUI(null);
        }
        private void ExecuteOrder(bool printReceipt)
        {
            try
            {
                AddOrderResult addOrderResult;
                using (var transactionScope = new TransactionScope())
                {
                    #region Add Order to Database
                    addOrderResult = _repository.AddOrder(1, Common.Helpers.OrderNumberHelper.GetOrderID(4, ""), string.Empty).FirstOrDefault();
                    
                    foreach (var product in orderDetails)
                    {
                        _repository.AddOrderDetails((int)addOrderResult.OrderId, 
                                                         product.ProductId, 
                                                         product.Quantity, 
                                                         product.UnitPrice, 
                                                         product.Discount);
                    } 
                    
                    #endregion

                    #region Pecati Fiskalna Smetka
                    if (printReceipt)
                    {
                        var stavki = Mapper.FiscalMapper.PrepareFiscalReceipt(orderDetails);
                        //_fiscalPrinter = new SY50("COM1");
                        _fiscalPrinter.Stavki = stavki;
                        _fiscalPrinter.FiskalnaSmetka(SY50.PaidMode.VoGotovo);
                    }
                    #endregion

                    transactionScope.Complete();
                }

                #region Update Order if Smetkata e ispecatena
                if (printReceipt)
                {
                    InfoDialog infoDialog = new InfoDialog("Дали се испечати сметка?", true);
                    infoDialog.ShowDialog();
                    if (infoDialog.DialogResult == DialogResult.Yes)
                    {
                        _repository.UpdateOrder((int)addOrderResult.OrderId, true);
                    }
                    else if (infoDialog.DialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                }
                #endregion

                // Pripremi forma za nova smetka
                NewOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void NewOrder()
        {
            orderDetails = new List<Product>();
            refreshUI(null);
        }
    }
}
