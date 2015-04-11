using AC.ExtendedRenderer.Navigator;
using AC.ExtendedRenderer.Toolkit.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Viktor.IMS.BusinessObjects;
using Viktor.IMS.BusinessObjects.Enums;
using Viktor.IMS.Fiscal.AccentFiscal;
using Viktor.IMS.Presentation.Infrastructure;

namespace Viktor.IMS.Presentation.UI
{
    public partial class HomeTabbed : BaseForm
    {
        
        private bool m_bLayoutCalled = false;
        private DateTime m_dt;
        private int _oldIndex = -1;
        private bool reverseInit = false;

        public HomeTabbed()
        {
            InitializeComponent();

            #region Custom Init
            //this.tabContainer.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabContainer_Deselecting);
            //this.tabContainer.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabContainer_Selecting);
            this.tabContainer.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabContainer_Selected);
            #endregion

            //this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.WindowState = FormWindowState.Maximized;
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.SplashScreen_Layout);
            this.FormId = Program.ActiveForms.Count + 1;
        }

        #region TAB EVENTS
        // PAUZIRAJ EVENT NA PRETHODNO SELEKTIRAN TAB
        private void tabContainer_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            //_oldIndex = e.TabPageIndex;
            TabPage previousTab = e.TabPage;
            if (previousTab != null)
            {
                BaseForm baseForm = (BaseForm)previousTab.Controls.Cast<Control>().FirstOrDefault(x => x is BaseForm);
                if (baseForm != null)
                {
                    baseForm.Select();
                    baseForm._listener.RemoveDataReceivedHandler();
                }
            }
        }
        // PAUZIRAJ EVENT NA PRETHODEN TAB
        private void tabContainer_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //TabPage previousTab = this.tabContainer.SelectedTab.Controls[0] as TabPage;
            //BaseForm baseForm = (BaseForm)previousTab.Controls.Cast<Control>().FirstOrDefault(x => x is BaseForm);
            if (this.tabContainer.SelectedTab != null)
            {
                BaseForm baseForm = (BaseForm)this.tabContainer.SelectedTab.Controls.Cast<Control>().FirstOrDefault(x => x is BaseForm);
                if (baseForm != null)
                {
                    baseForm.Select();
                    baseForm._listener.RemoveDataReceivedHandler();
                }
            }
        }
        // AKTIVIRAJ EVENT NA SELECTIRAN TAB
        private void tabContainer_Selected(object sender, TabControlEventArgs e)
        {
            foreach (var formData in Program.ActiveForms.Where(x => x.HasBarcodeScannedEvent))
            {
                //formData.Form._listener.ReleaseHandle();
                //formData.Form._listener.DestroyHandle();//.ReleaseHandle();
                //formData.Form._listener = new BarcodeListener(formData.Form);

                formData.Form.SerialEventListener_Pause();
                formData.Form._listener.RemoveDataReceivedHandler();
                //formData.Form._listener.RemoveHandleEvents(formData.Form);
                //formData.Form._listener = null;
                formData.HasBarcodeScannedEvent = false;
            }
            
            if (this.tabContainer.SelectedTab != null)
            {
                BaseForm baseForm = (BaseForm)this.tabContainer.SelectedTab.Controls.Cast<Control>().FirstOrDefault(x => x is BaseForm);
                if (baseForm != null)
                {
                    baseForm.Select();
                    if (baseForm._listener != null)
                    {
                        baseForm._listener.AddDataReceivedHandler();
                        baseForm.SerialEventListener_Resume();
                    }
                    //baseForm._listener.HookHandleEvents(baseForm);
                    //baseForm._listener = new BarcodeListener();
                    var element = Program.ActiveForms.FirstOrDefault(x => x.Form.FormId == baseForm.FormId);
                    if (element != null)
                        element.HasBarcodeScannedEvent = true;
                }
            }
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            InitAllHArdware();
            // do stuff before Load-event is raised

            base.OnLoad(e);
            // do stuff after Load-event was raised
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

        private void exitButton_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Close();
            }
            System.Windows.Forms.Application.Exit();
        }

        private void productsButton_Click(object sender, EventArgs e)
        {
            //Konstruktor
            MainForm main = new MainForm(this._serialPort);

            main._repository = this._repository;
            main.FormBorderStyle = FormBorderStyle.None;
            main.TopLevel = false;

            //Added new TabPage
            TabPage tabPage = NewTabPage(string.Format("Производи {0}", this.tabContainer.TabCount), "tbpProducts", (Control)main);
            this.tabContainer.TabPages.Add(tabPage);
            

            //Added form to tabpage
            main.Dock = DockStyle.Fill;
            main.WindowState = FormWindowState.Maximized;
            main.Show(); //So ovaa naredba se aktivira OnLoad event na samata forma

            // Pauziraj listener vo prethodno aktivniot tab
            //PausePreviousSelectedTab_Listener();

            // Duri otkako formata ke se loadira go selektirame soodvetniot tab za aktiven
            this.tabContainer.SelectTab(tabPage);
            this.tabContainer.SelectedTab.Controls[0].Select();
        }

        private void saleButton_Click(object sender, EventArgs e)
        {
            Sale sale = new Sale(this._serialPort, this._fiscalPrinter, CustomerType.RETAIL);
            sale.FormBorderStyle = FormBorderStyle.None;
            sale._repository = this._repository;
            sale.TopLevel = false;
            
            //Added new TabPage
            TabPage tabPage = NewTabPage(string.Format("Продажба {0}", this.tabContainer.TabCount), "tbpSale", (Control)sale);
            this.tabContainer.TabPages.Add(tabPage);
            

            //Added form to tabpage
            sale.Dock = DockStyle.Fill;
            sale.WindowState = FormWindowState.Maximized;
            sale.Show();

            // Pauziraj listener vo prethodno aktivniot tab
            //PausePreviousSelectedTab_Listener();

            this.tabContainer.SelectTab(tabPage);
            this.tabContainer.SelectedTab.Controls[0].Select();
        }

        private void saleHomeButton_Click(object sender, EventArgs e)
        {
            Sale sale = new Sale(this._serialPort, this._fiscalPrinter, CustomerType.HOME);
            sale.FormBorderStyle = FormBorderStyle.None;
            sale._repository = this._repository;
            sale.TopLevel = false;

            //Added new TabPage
            TabPage tabPage = NewTabPage(string.Format("Продажба дома {0}", this.tabContainer.TabCount), "tbpSale", (Control)sale);
            this.tabContainer.TabPages.Add(tabPage);


            //Added form to tabpage
            sale.Dock = DockStyle.Fill;
            sale.WindowState = FormWindowState.Maximized;
            sale.Show();

            // Pauziraj listener vo prethodno aktivniot tab
            //PausePreviousSelectedTab_Listener();

            this.tabContainer.SelectTab(tabPage);
            this.tabContainer.SelectedTab.Controls[0].Select();
        }

        private TabPage NewTabPage(string title, string name, Control control)
        {
            TabPage tabPage = new TabPage();
            tabPage.BackColor = SystemColors.Window;
            tabPage.Controls.Add(control);
            tabPage.Location = new Point(4, 29);
            tabPage.Margin = new Padding(0);
            tabPage.Name = "tabPage1";
            tabPage.Size = new Size(633, 468);
            tabPage.TabIndex = 0;
            tabPage.Tag = (object)false;
            tabPage.Text = string.Format(" {0} ", title);
            tabPage.Name = name;
            return tabPage;
        }

        /// <summary>
        ///  Sega za sega ne se koristat ovie metodi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (_oldIndex != -1)
            {
                if (CanChangeTab(_oldIndex, e.TabPageIndex))
                {
                    e.Cancel = true;
                }
            }
        }
        private bool CanChangeTab(int fromIndex, int toIndex)
        {
            // Put your logic here
            return true;
        }
        private void tabContainer_Selected()
        {
            //var picBox = this.tabContainer.TabPages.Cast<Control>()
            //.SelectMany(page => page.Controls.OfType<PictureBox>())
            //.First();
            //picBox.ImageLocation = "...";
            //TabPage tabPage = this.tabContainer.SelectedTab.Controls[0];
            //if (this.tabContainer.SelectedTab.Controls[0].Contains(BaseForm)     Contains.ContainsKey("rtb"))
            //RichTextBox selectedRtb = (RichTextBox)selectedTab.Controls["rtb"];
        }

        private void InitAllHArdware()
        {
            _listener = new BarcodeListener(this);
            _listener.RemoveDataReceivedHandler();
            InitializeFiscalPrinter();
            reverseInit = true;
        }

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

        private void reverseInitDevices_Click(object sender, EventArgs e)
        {
            Program.UserDevices = new List<Device>();
            this.CloseSerialConnection();
            _listener = null;
            _fiscalPrinter = null;
            if (reverseInit)
            {
                InitializeFiscalPrinter();
                _listener = new BarcodeListener(this);
                reverseInit = false;
            }
            else
            {
                _listener = new BarcodeListener(this);
                InitializeFiscalPrinter();
                reverseInit = true;
            }
        }

        private void PausePreviousSelectedTab_Listener()
        {
            if (this.tabContainer.SelectedTab != null)
            {
                TabPage previousTab = this.tabContainer.SelectedTab.Controls[0] as TabPage;
                BaseForm baseForm = (BaseForm)previousTab.Controls.Cast<Control>().FirstOrDefault(x => x is BaseForm);
                if (baseForm != null)
                {
                    baseForm.Select();
                    baseForm._listener.RemoveDataReceivedHandler();
                }
            }
        }

        private void showReportButton_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.FormBorderStyle = FormBorderStyle.None;
            report._repository = this._repository;
            report.TopLevel = false;

            //Added new TabPage
            TabPage tabPage = NewTabPage(string.Format("Извештај {0}", this.tabContainer.TabCount), "tbpReport", (Control)report);
            this.tabContainer.TabPages.Add(tabPage);


            //Added form to tabpage
            report.Dock = DockStyle.Fill;
            report.WindowState = FormWindowState.Maximized;
            report.Show();

            this.tabContainer.SelectTab(tabPage);
            this.tabContainer.SelectedTab.Controls[0].Select();
        }

        private void orderDetailsButton_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.FormBorderStyle = FormBorderStyle.None;
            orders._repository = this._repository;
            orders.TopLevel = false;

            //Added new TabPage
            TabPage tabPage = NewTabPage(string.Format("Сметки {0}", this.tabContainer.TabCount), "tbpOrders", (Control)orders);
            this.tabContainer.TabPages.Add(tabPage);


            //Added form to tabpage
            orders.Dock = DockStyle.Fill;
            orders.WindowState = FormWindowState.Maximized;
            orders.Show();

            this.tabContainer.SelectTab(tabPage);
            this.tabContainer.SelectedTab.Controls[0].Select();
        }        
    }
}

/* Comment
        private void tabContainer_DrawItem(object sender, DrawItemEventArgs e)
        {
            //This code will render a "x" mark at the end of the Tab caption. 
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4);
            e.Graphics.DrawString(this.tabContainer.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        private void tabContainer_MouseDown(object sender, MouseEventArgs e)
        {
            //Looping through the controls.
            for (int i = 0; i < this.tabContainer.TabPages.Count; i++)
            {
                Rectangle r = tabContainer.GetTabRect(i);
                //Getting the position of the "x" mark.
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
                if (closeButton.Contains(e.Location))
                {
                    if (MessageBox.Show("Would you like to Close this Tab?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.tabContainer.TabPages.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        */
