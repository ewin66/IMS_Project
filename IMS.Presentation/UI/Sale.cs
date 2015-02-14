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
using System.Windows.Forms;

namespace Viktor.IMS.Presentation.UI
{
    public partial class Sale : BaseForm
    {
        List<Product> orderDetails;
        private BarcodeListener listener;
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
            ///
            /// Test Method
            NewOrder();
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
                    _fiscalPrinter = new SY50(portName);
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
        private void NewOrder()
        {
            orderDetails = new List<Product>();
            AddOrders();
        }

        private void AddOrders()
        {
            orderDetails.Add(new Product()
            {
                ProductId = 16,
                ProductName = "МЛЕКО",
                Quantity = 1,
                UnitPrice = 42,
                Price = 42,
            });

            dataGridView1.DataSource = orderDetails;
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            var product = new Product()
            {
                ProductId = 31,
                ProductName = "РОДЕО ЛАИТ",
                Quantity = 1,
                UnitPrice = 58,
                Price = 58,
            };

            var query = orderDetails.Where(x => x.ProductId == product.ProductId);
            if (query.Count() > 0)
            {
                ++query.Single().Quantity;
                query.Single().Price = query.Single().Quantity * query.Single().UnitPrice;
            }
            else
                orderDetails.Add(product);

            dataGridView1.DataSource = orderDetails.ToArray();
            lblCurrentProduct.Text = string.Format("{0} {1} ком x {2} = {3}", query.Single().ProductName, query.Single().Quantity, ((decimal)query.Single().UnitPrice).ToString("N2", nfi), ((decimal)query.Single().Price).ToString("N2", nfi));
            lblTotalValue.Text = ((decimal)orderDetails.Sum(x => x.Price)).ToString("N2", nfi);
        }

        #region Barcode Events
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
                }
                else
                    orderDetails.Add(product);

            }
            dataGridView1.DataSource = orderDetails;
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
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(KeyEvent);
        }

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
                    using (var search = new Search(this._serialPort))
                    {
                        search._repository = this._repository;
                        search.Owner = this;
                        search.ShowDialog();

                        if (search.CurrentProduct != null && search.CurrentProduct.ProductId > 0)
                        {
                            /// Add Product to LIST
                            /// ===================
                            var query = orderDetails.Where(x => x.ProductId == search.CurrentProduct.ProductId);
                            if (query.Count() > 0)
                            {
                                ++query.Single().Quantity;
                                query.Single().Price = query.Single().Quantity * query.Single().UnitPrice;
                            }
                            else
                            {
                                var product = _repository.GetProduct(search.CurrentProduct.ProductId, null, null);
                                this.orderDetails.Add(product);
                            }
                            this.dataGridView1.DataSource = orderDetails.ToArray();
                        }
                        e.Handled = true;
                    }
                    break;
                case Keys.F9:
                    ExecuteOrder();
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
        private void ExecuteOrder()
        {
            try
            {
                var stavki = Mapper.FiscalMapper.PrepareFiscalReceipt(orderDetails);
                _fiscalPrinter.Stavki = stavki;
                _fiscalPrinter.FiskalnaSmetka(SY50.PaidMode.VoGotovo);
                //_fiscalPrinter.PrintReceipt(PaidMode.VoGotovo);
                //_fiscalPrinter.ClosePort();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        #endregion
    }
}
