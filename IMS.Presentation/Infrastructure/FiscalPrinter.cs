using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Viktor.IMS.BusinessObjects;
using Viktor.IMS.BusinessObjects.Enums;
using Viktor.IMS.Fiscal.AccentFiscal;
using Viktor.IMS.Presentation.UI;

namespace Viktor.IMS.Presentation.Infrastructure
{
    public class FiscalPrinter : NativeWindow
    {
        private SY50 _fiscalPrinter { get; set; }

        public FiscalPrinter(Form form)
        {
            IntPtr hwnd;

            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            hwnd = form.Handle;

            _fiscalPrinter = ((BaseForm)form)._fiscalPrinter;
            this.InitializeFiscalPrinter();
            ((BaseForm)form)._fiscalPrinter = _fiscalPrinter;

            //HookRawInput(hwnd);
            //this.HookHandleEvents(form);
            //this.AssignHandle(hwnd);
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
    }
}
