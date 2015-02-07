using LinqDataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.IO.Ports;
using System.Windows.Forms;

namespace Viktor.IMS.Presentation
{
  static class Program {
    public static string activeFormName = "";
    public static bool IsBarcodeScannerConnected { get; set; }
    //public SerialPort _serialPort;
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
      
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
      IsBarcodeScannerConnected = true;

      var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
      MainForm main = new MainForm();
      main._repository = new DataRepository(connectionString);
      //main._serialPort = _serialPort;
      Application.Run(main);
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
}