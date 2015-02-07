using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;

namespace Viktor.IMS {
  static class Program {
    public static string activeFormName = "";
    public static SerialPort _serialPort;
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
      _serialPort = new SerialPort("COM1"); //give your barcode serial port 
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
          //MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
      }

      Application.Run(new MainForm());
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