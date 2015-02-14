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
        private PF550 _fiscalPrinter { get; set; }

        public Sale()
        {
            InitializeComponent();
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
                    _fiscalPrinter = new PF550(portName);
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
            lblCurrentProduct.Text = string.Format("{0} {1} ком x {2} = {3}", query.Single().ProductName, query.Single().Quantity, query.Single().UnitPrice, query.Single().Price);
            lblTotalValue.Text = orderDetails.Sum(x => x.Price).ToString();
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

        /// <summary>
        /// F9-Execute Order, F6-Save, F3-LookUp.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyEvent(object sender, KeyEventArgs e) //Keyup Event 
        {
            if (e.KeyCode == Keys.F9)
            {
                //MessageBox.Show("Function F9");
                ExecuteOrder();
            }
            if (e.KeyCode == Keys.F6)
            {
                MessageBox.Show("Function F6");
            }
            else
                MessageBox.Show("No Function");
        }
        private void ExecuteOrder()
        {
            try
            {
                var stavki = Mapper.FiscalMapper.PrepareFiscalReceipt(orderDetails);
                _fiscalPrinter.Stavki = stavki;
                _fiscalPrinter.FiskalnaSmetka(PF550.PaidMode.VoGotovo);
                //_fiscalPrinter.PrintReceipt(PaidMode.VoGotovo);
                //_fiscalPrinter.ClosePort();
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }
        #endregion
    }
}
