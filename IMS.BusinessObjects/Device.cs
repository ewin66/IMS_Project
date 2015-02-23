using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using Viktor.IMS.BusinessObjects.Enums;

namespace Viktor.IMS.BusinessObjects
{
    public class Device
    {
        public DeviceType DeviceType { get; set; }
        public string PortName { get; set; }
        public bool IsConnected { get; set; }
        public SerialPort SerialPort { get; set; }
    }
}
