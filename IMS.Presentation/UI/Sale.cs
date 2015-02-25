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

namespace Viktor.IMS.Presentation.UI
{
    public partial class Sale : BaseForm
    {
        List<Product> orderDetails;
        CustomerType _currentCustomer;
        //private BarcodeListener listener;
        private NumberFormatInfo nfi;

        private int? totalArticles;
        private int? articlesWithStock;
        private decimal? cumulativeAmount;


        public Sale(SerialPort serialPort, SY50 fiscalPrinter, CustomerType currentCustomer)
        {
            InitializeComponent();
            this.updateFont();
            this._currentCustomer = currentCustomer;
            this._serialPort = serialPort;
            this._fiscalPrinter = fiscalPrinter;
            //if (Program.ActiveForms.Count == 0) 
            this._listener = new BarcodeListener(this);
            this.SerialEventListener_Start();
            this.InitializeFiscalPrinter();
            
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(KeyEvent);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns["Quantity"].DefaultCellStyle.Format = "n";
            //dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;
            //dataGridView1.Columns["Quantity"].ReadOnly = false;
            //dataGridView1.Columns["UnitPrice"].ReadOnly = false;

            lblCurrentProduct.Text = string.Empty;
            lblTotalValue.Text = "0.00";
            this.nfi = new NumberFormatInfo();
            this.nfi.NumberDecimalSeparator = ".";
            convertor = new ISO9TransliterationProvider();
            myCurrentLanguage = InputLanguage.CurrentInputLanguage;
            
            orderDetails = new List<Product>();
            this.ActiveControl = dataGridView1;            
        }
        protected override void OnLoad(EventArgs e)
        {
            var result = _repository.GetTodayTurnover().FirstOrDefault();
            lblTodayTurnover.Text = decimal.Round(result.TodayTurnover).ToString("N2", nfi);
            // do stuff before Load-event is raised
            base.OnLoad(e);
            // do stuff after Load-event was raised
            this.FormId = Program.ActiveForms.Count + 1;
            Program.ActiveForms.Add(new FormData(this, true));
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
            var device = Program.UserDevices.FirstOrDefault(x => x.DeviceType == DeviceType.FiscalPrinter);
            if (device != null && !device.IsConnected) return;
            try
            {
                if (_fiscalPrinter == null)
                {
                    if (device == null)
                    {
                        device = new Device();
                        device.DeviceType = DeviceType.FiscalPrinter;
                        device.IsConnected = true;
                        Program.UserDevices.Add(device);
                    }

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

                    string VID = hardware.FirstOrDefault(x => x.Key == "SerialDevice").Value.Split('&')[0].Replace("VID_", "");
                    string PID = hardware.FirstOrDefault(x => x.Key == "SerialDevice").Value.Split('&')[1].Replace("PID_", "");
                    var ports = Common.Helpers.DeviceHelper.GetPortByVPid(VID, PID).Distinct(); //("067B", "2303")
                    var portName = SerialPort.GetPortNames().Intersect(ports).FirstOrDefault(x => !Program.UserDevices.Any(y => y.PortName == x));
                    this.CheckPort(portName);
                    _fiscalPrinter = new SY50(portName);

                    device.PortName = portName;
                    //MessageBox.Show("Успешно поврзување со касата, на port :: " + portName);
                }

            }
            catch (Exception ex)
            {
                device.IsConnected = false;
                MessageBox.Show(this, "Неуспешно поврзување со Фискалната каса, проверете дали е приклучена!\n\nOpening serial port result :: " + ex.Message, "Информација!");
            }
        }

        #region BARCODE EVENTS
        public void ResumeSerialEventListener()
        {
            _listener.AddDataReceivedHandler();
        }
        public void AddProduct(int? productId, string productName, string barcode)
        {
            if (productId != null || productName != null || barcode != null)
            {
                var itemNumber = 0;
                var product = _repository.GetProduct(productId, productName, barcode);
                if (product != null)
                {
                    var query = orderDetails.Where(x => x.ProductId == product.ProductId);
                    if (query.Count() > 0)
                    {
                        ++query.Single().Quantity;
                        query.Single().Price = query.Single().Quantity * query.Single().UnitPrice;
                        itemNumber = query.Single().ItemNumber;
                        this.refreshUI(query.Single());
                    }
                    else
                    {
                        product.ItemNumber = itemNumber = this.orderDetails.Count + 1;
                        this.orderDetails.Add(product);
                        this.refreshUI(product);
                    }

                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[itemNumber - 1].Cells["Quantity"];
                }
            }
        }
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
                        AddProduct(null, null, be.Barcode);
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
            //this.KeyPreview = true;
            //this.KeyDown += new System.Windows.Forms.KeyEventHandler(KeyEvent);
        }
        /// <summary>
        /// F9-Execute Order, F6-Save, F3-LookUp.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyEvent(object sender, KeyEventArgs e) //Keyup Event 
        {
            /*
            // Boolean flag used to determine when a character other than a number is entered. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace. 
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        //nonNumberEntered = true;
                        e.SuppressKeyPress = true;
                    }
                }
            }
            */

            if (e.KeyData == Keys.Decimal || e.KeyData == Keys.OemPeriod)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send(",");
            }

            //if (e.KeyValue == 46) e.KeyValue = 44;
            //if (e.KeyValue == 110 || e.KeyValue == 190)
            //{
            //    e.Handled = true;
            //    base.OnKeyPress(new KeyPressEventArgs(','));
            //}
            switch (e.KeyCode)
            {
                #region LEFT ALT/RIGHT ALT: SearchForm (Prebaruvanje na proizvod)
                case Keys.F4:
                case Keys.RButton | Keys.ShiftKey:

                    //this.SerialEventListener_Pause();
                    //_listener.RemoveDataReceivedHandler();

                    using (var searchForm = new Search())
                    {
                        searchForm._repository = this._repository;
                        searchForm.Owner = this;
                        searchForm.StartPosition = FormStartPosition.CenterParent;
                        searchForm.ShowDialog();
                        if (searchForm.CurrentProduct != null && searchForm.CurrentProduct.ProductId > 0)
                        {
                            /// Add Product to LIST
                            /// ===================
                            AddProduct(searchForm.CurrentProduct.ProductId, null, null);
                        }
                        e.Handled = true;
                    }
                    break;
                #endregion

                #region SPACE: Pecatenje na fiskalna smetka
                case Keys.Space:
                    ExecuteOrder(true);
                    e.Handled = true;
                    break;
                #endregion

                #region RIGHT SHIFT : Zatvaranje na smetka bez pecatenje na fiskalana smetka
                case Keys.ShiftKey:
                    ExecuteOrder(false);
                    e.Handled = true;
                    break;
                #endregion

                #region DELETE Brishenje na prozvod
                case Keys.Delete:
                case Keys.Back:
                    delete_Click();
                    e.Handled = true;
                    break;
                #endregion

                case Keys.Up:
                    moveUp();
                    e.Handled = true;
                    break;
                case Keys.Down:
                    moveDown();
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Quantity" ||
                dataGridView1.Columns[e.ColumnIndex].Name == "UnitPrice")
            {
                var query = orderDetails.Where(x => x.ItemNumber == e.RowIndex + 1);
                query.Single().Quantity = decimal.Parse(this.dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value.ToString());
                query.Single().UnitPrice = decimal.Parse(this.dataGridView1.Rows[e.RowIndex].Cells["UnitPrice"].Value.ToString());
                query.Single().Price = Math.Round(query.Single().Quantity * query.Single().UnitPrice, 2);
                this.refreshUI(query.Single());
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells["Quantity"];
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

                    if (index == (rowCount-1)) // -2 include the header row
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
                lblCurrentProduct.Text = string.Format("{0}  {1} ком.  x  {2}  =  {3}", product.ProductName, product.Quantity, ((decimal)product.UnitPrice).ToString("N2", nfi), ((decimal)product.Price).ToString("N2", nfi));
            }
            else
            {
                lblCurrentProduct.Text = "";
            }
            lblTotalValue.Text = decimal.Round(((decimal)orderDetails.Sum(x => x.Price))) .ToString("N2", nfi);
            this.dataGridView1.DataSource = orderDetails.ToArray();
        }
        private void delete_Click()
        {
            if (orderDetails.Count > 0 && this.dataGridView1.CurrentCell != null && this.dataGridView1.CurrentCell.RowIndex >= 0)
            {
                var productId = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["ProductId"].Value.ToString();
                var product = orderDetails.FirstOrDefault(x => x.ProductId.ToString() == productId);
                if (product != null)
                {
                    orderDetails.Remove(product);
                    refreshUI(null);
                }
            }
        }
        private void ExecuteOrder(bool printReceipt)
        {
            try
            {
                if (orderDetails.Count == 0)
                {
                    MessageBox.Show(this, "Нема производи за продавање!", "Информација!");
                    return;
                }
                if (printReceipt && _fiscalPrinter == null)
                {
                    MessageBox.Show(this, "Фискалната каса не е успешно поврзана!", "Информација!");
                    return;
                }

                AddOrderResult addOrderResult;
                using (var transactionScope = new TransactionScope())
                {
                    #region Add Order to Database
                    addOrderResult = _repository.AddOrder((int)_currentCustomer, null, 1, Common.Helpers.OrderNumberHelper.GetOrderID(4, ""), string.Empty).FirstOrDefault();
                    
                    foreach (var product in orderDetails)
                    {
                        _repository.AddOrderDetails((int)addOrderResult.OrderId, 
                                                         product.ProductId,
                                                         product.ProductName, 
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

                    var result = _repository.UpdateOrder((int)addOrderResult.OrderId, true).FirstOrDefault();
                    lblTodayTurnover.Text = decimal.Round(result.TodayTurnover).ToString("N2", nfi);

                    transactionScope.Complete();
                }

                #region Update Order if Smetkata e ispecatena
                if (printReceipt)
                {
                    InfoDialog infoDialog = new InfoDialog("Сметката е процесирана.", true);
                    infoDialog.ShowDialog();
                    if (infoDialog.DialogResult == DialogResult.Yes)
                    {
                        //_repository.UpdateOrder((int)addOrderResult.OrderId, true);
                    }
                    else if (infoDialog.DialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                }
                else
                {
                    InfoDialog infoDialog = new InfoDialog("Сметката е процесирана.", true);
                    infoDialog.ShowDialog();
                    if (infoDialog.DialogResult == DialogResult.Yes)
                    {
                        //nema potreba za update
                        //_repository.UpdateOrder((int)addOrderResult.OrderId, false);
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

        /// <summary>
        /// Form.KeyPreview is a bit of an anachronism, inherited from the Visual Basic object model for form design. 
        /// Back in the VB6 days, you needed KeyPreview to be able to implement short-cut keystrokes. 
        /// That isn't needed anymore in Windows Forms, overriding the ProcessCmdKey() is the better solution:
        /// But KeyPreview was supported to help the legion of VB6 programmers switch to .NET back in the early 2000's. 
        /// The point of KeyPreview or ProcessCmdKey() is to allow your UI to respond to shortcut keystrokes. 
        /// Keyboard messages are normally sent to the control that has the focus. The Windows Forms message loop 
        /// allows code to have a peek at that message before the control sees it. That's important for short-cut keys, 
        /// implementing the KeyDown event for every control that might get the focus to detect them is very impractical.
        /// Setting KeyPreview to True doesn't cause problems. The form's KeyDown event will run, 
        /// it will only have an affect if it has code that does something with the keystroke. 
        /// But do beware that it closely follows the VB6 usage, you can't see the kind of keystrokes that are used 
        /// for navigation. Like the cursor keys and Tab, Escape and Enter for a dialog. 
        /// Not a problem with ProcessCmdKey().
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                //DoSomething();   // Implement the Ctrl+F short-cut keystroke
                return true;     // This keystroke was handled, don't pass to the control with the focus
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private bool alreadyExist(string _text, ref char KeyChar)
        {
            if (_text.IndexOf('.') > -1)
            {
                KeyChar = '.';
                return true;
            }
            if (_text.IndexOf(',') > -1)
            {
                KeyChar = ',';
                return true;
            }
            return false;
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
        /*
        private void txtValormetrocubico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar)
                    && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            //check if '.' , ',' pressed
            char sepratorChar = 's';
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                // check if it's in the beginning of text not accept
                if (txtValormetrocubico.Text.Length == 0) e.Handled = true;
                // check if it's in the beginning of text not accept
                if (txtValormetrocubico.SelectionStart == 0) e.Handled = true;
                // check if there is already exist a '.' , ','
                if (alreadyExist(txtValormetrocubico.Text, ref sepratorChar)) e.Handled = true;
                //check if '.' or ',' is in middle of a number and after it is not a number greater than 99
                if (txtValormetrocubico.SelectionStart != txtValormetrocubico.Text.Length && e.Handled == false)
                {
                    // '.' or ',' is in the middle
                    string AfterDotString = txtValormetrocubico.Text.Substring(txtValormetrocubico.SelectionStart);

                    if (AfterDotString.Length > 2)
                    {
                        e.Handled = true;
                    }
                }
            }
            //check if a number pressed

            if (Char.IsDigit(e.KeyChar))
            {
                //check if a coma or dot exist
                if (alreadyExist(txtValormetrocubico.Text, ref sepratorChar))
                {
                    int sepratorPosition = txtValormetrocubico.Text.IndexOf(sepratorChar);
                    string afterSepratorString = txtValormetrocubico.Text.Substring(sepratorPosition + 1);
                    if (txtValormetrocubico.SelectionStart > sepratorPosition && afterSepratorString.Length > 1)
                    {
                        e.Handled = true;
                    }

                }
            }


        }
        */
    }
}
