using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;

namespace Viktor.IMS.Main
{
    static class Program
    {
        public static string activeFormName = "";
        public static SerialPort _serialPort;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

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
            Form mainForm = GetForm("MainForm");
            Application.Run(mainForm);
        }

        private static Form GetForm()
        {
            IUnityContainer myContainer = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection ("unity");
            string containerName = "PresentationContainer";
            section.Configure(myContainer, containerName);
            Form form = myContainer.Resolve<Form>("MainForm");
            return form;
        }
        private static Form GetForm(string formName)
        {
            IUnityContainer myContainer = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            string containerName = "PresentationContainer";
            section.Configure(myContainer, containerName);
            Form form = myContainer.Resolve<Form>(formName);
            return form;
        }
    }
}
