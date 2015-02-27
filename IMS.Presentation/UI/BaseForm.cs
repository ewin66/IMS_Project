using Common.Helpers;
using LinqDataModel;
using LinqDataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Reflection;
using System.Threading.Tasks;

namespace Viktor.IMS.Presentation.UI
{
    public partial class BaseForm : KryptonForm
    {
        #region Public Properies
        public InputLanguage myCurrentLanguage;
        public ISO9TransliterationProvider convertor;
        public IDataRepository _repository { get; set; }
        public BarcodeListener _listener { get; set; }
        public Viktor.IMS.Fiscal.AccentFiscal.SY50 _fiscalPrinter { get; set; }
        public SerialPort _serialPort { get; set; }
        public DataRow LastDataRow = null; //tracks for the PositionChanged event the last row
        public Product CurrentProduct { get; set; }
        public bool IsActive { get; set; }
        public int FormId { get; set; }
        //public string activeFormName = "";
        #endregion

        public BaseForm()
        {
            InitializeComponent();
            //this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BaseForm_FormClosed);
            //this.Enter += new System.EventHandler(this.BaseForm_Enter);
            //this.Activated += new EventHandler(this.BaseForm_GotFocus);
            //this.FormClosed += this.OnFormClosed;
        }
        //private async void MainForm_Load(object sender, EventArgs args)
        //{
        //    // Do stuff here that does not depend on _store or _repository.
        //    Task wakeUp = InitializeRepositoryAsync();
        //    Task.WaitAll(wakeUp);
        //    //await InitializeRepositoryAsync();
        //    // Now you can use _store and _repository here.
        //}

        //private Task InitializeRepositoryAsync()
        //{
        //    return Task.Factory.StartNew<int>(() =>
        //    {
        //        this._repository = new DataRepository(Program.ConnectionString);
        //        return 1;
        //    });
        //}

        #region Helper Methods
        public InputLanguage GetInputLanguageByName(string inputName)
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

        public void SerialEventListener_Start()
        {
            this._listener.BarcodeScanned += this.OnBarcodeScanned;
        }
        public void SerialEventListener_Pause()
        {
            this._listener.BarcodeScanned -= this.OnBarcodeScanned;
        }
        public void SerialEventListener_Resume()
        {
            this._listener.BarcodeScanned += this.OnBarcodeScanned;
        }

        public void BarcodeListener_PauseOnAllForms()
        {
            /// Metod 1
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);

            foreach (BaseForm f in openForms)
                f._listener.RemoveDataReceivedHandler();
        /*
        /// Metod 2
        for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
        {
            if (Application.OpenForms[i].Name != "Menu")
                Application.OpenForms[i].Close();
        }
        */
        /*
        /// Metod 3
        foreach (Form f in Application.OpenForms.Reverse())
        {
            if (f.Name != "Menu")
                f.Close();
        }
        */
        }
        #endregion

        #region Vitual Methods
        public virtual void textBox_Enter(object sender, EventArgs e)
        {
            SetKeyboardLayout(GetInputLanguageByName("mk"));
        }
        public virtual void Form_Activated(object sender, EventArgs e)
        {
            SetKeyboardLayout(GetInputLanguageByName("mk"));
        }
        public virtual void OnBarcodeScanned(object sender, EventArgs e)
        {
            BarcodeScannedEventArgs be;
            be = e as BarcodeScannedEventArgs;
        }
        #endregion



        private void BaseForm_GotFocus(object sender, EventArgs e)
        {
            //var lastActiveForm = Program.OpenForms.FirstOrDefault(x => ((BaseForm)x).IsActive);
            //if (lastActiveForm != null)
            //{
            //    if (((BaseForm)lastActiveForm).listener != null)
            //        ((BaseForm)lastActiveForm).listener.Pause();

            //    ((BaseForm)lastActiveForm).IsActive = false;
            //}

            /// Pauziranje na Listener-ot na prethodno aktivnata forma
            /// =======================================================
            foreach (Form lastActiveForm in Application.OpenForms)
            {
                var isDerivedForm = IsSubclassOfRawGeneric(this.GetType(), typeof(BaseForm));
                if (isDerivedForm && ((BaseForm)lastActiveForm).IsActive && ((BaseForm)lastActiveForm) != this)
                {
                    if (((BaseForm)lastActiveForm)._listener != null)
                        ((BaseForm)lastActiveForm)._listener.RemoveDataReceivedHandler();

                    ((BaseForm)lastActiveForm).IsActive = false;
                    break;
                }
            }
            /// =======================================================
            /// Dodavanje na ovaa forma 
            /// =======================================================
            this.IsActive = true;
            if (this._listener != null)
                this._listener.AddDataReceivedHandler();
        }
        static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
        public void CheckPort(string com_port)
        {
            var port = new SerialPort(com_port); //give your barcode serial port 
            port.BaudRate = 9600;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;
            port.DataBits = 8;
            port.Handshake = Handshake.None;
            port.Open();
            System.Threading.Thread.Sleep(100);
            port.DiscardInBuffer();
            port.Close();
            port.Dispose();
            port = null;
        }
        public bool CloseSerialConnection()
        {
            try
            {
                //_serialPort.Write("LOCAL\r");
                //System.Threading.Thread.Sleep(100);
                _listener.RemoveDataReceivedHandler();
                //string test = _serialPort.ReadLine();
                _serialPort.DiscardInBuffer();
                _serialPort.Close();
                _serialPort.Dispose();
                _serialPort = null;
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void BaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseSerialConnection();
        }

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }
        // thread-safe equivalent of
        // myLabel.Text = status;
        //SetControlPropertyThreadSafe(myLabel, "Text", status);
        //If you're using .NET 3.0 or above, you could rewrite the above method as an extension method of the Control class, which would then simplify the call to:
        //myLabel.SetPropertyThreadSafe("Text", status);
    }
}
