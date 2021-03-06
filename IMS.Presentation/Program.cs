using LinqDataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.IO.Ports;
using System.Windows.Forms;
using System.Globalization;
using Viktor.IMS.Presentation.UI;
using Viktor.IMS.Presentation.Infrastructure;
using Viktor.IMS.BusinessObjects;

namespace Viktor.IMS.Presentation
{
    static class Program
    {
        public static string activeFormName = "";
        public static List<Device> UserDevices { get; set; }
        public static List<FormData> ActiveForms { get; set; }
        public static string ConnectionString { get; set; }
        public static bool EventHasSubscribers = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            UserDevices = new List<Device>();
            ActiveForms = new List<FormData>();
            CultureInfo culture = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name, true);
            culture.NumberFormat.NumberDecimalSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SplashScreen.SplashScreen.ShowSplashScreen();
            Application.DoEvents();

            SplashScreen.SplashScreen.SetStatus("Loading module 1");
            System.Threading.Thread.Sleep(100);
            SplashScreen.SplashScreen.SetStatus("Loading module 2");
            System.Threading.Thread.Sleep(200);
            SplashScreen.SplashScreen.SetStatus("Loading module 14");
            System.Threading.Thread.Sleep(300);


            #region INIT USER DEVICES


            #endregion
            /// Init seral barcode reader
            /*
            var ports = Common.Helpers.DeviceHelper.GetPortByVPid("067B", "2303").Distinct();
            var COM_PORT = SerialPort.GetPortNames().Intersect(ports).FirstOrDefault();
            //Common.Helpers.DeviceHelper.NullModemCheck(ref ports);

            //SerialPort _serialPort = null;
            SerialPort _serialPort = new SerialPort(COM_PORT); //give your barcode serial port 
            _serialPort.BaudRate = 9600;
            _serialPort.Parity = Parity.None;
            _serialPort.StopBits = StopBits.One;
            _serialPort.DataBits = 8;
            _serialPort.Handshake = Handshake.None;
            //_serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            // Makes sure serial port is open before trying to write
            try
            {
                if (!_serialPort.IsOpen)
                    _serialPort.Open();

                //_serialPort.Write("SI\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
            }
            */

            /// Na pocetok pretpostavuvame deka BarcodeScanner-ot e konektiran
            /// Dokolku se pojavi greshka pri obidot za inicijallizacija na istiot ova property ke se postavi na FALSE


            /*
            MainForm main = new MainForm();
            main._repository = new DataRepository(connectionString);
            //main._serialPort = _serialPort;
            Application.Run(main);
            */
            //Home home = new Home();
            HomeTabbed home = new HomeTabbed();
            ConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            home._repository = new DataRepository(ConnectionString);

            Application.Run(home);
        }
    }
}
    /*
  public static class ControlExtension
  {
      public static void ThreadSafeInvoke(this Control control, MethodInvoker method)
      {
          if (control != null)
          {
              if (control.InvokeRequired)
              {
                  control.Invoke(method);
              }
              else
              {
                  method.Invoke();
              }
          }
      }
  }
    */
