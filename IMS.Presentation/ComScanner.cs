using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.IO.Ports;
namespace IMS.Presentation
{
    public class CommandCode
    {
        public string CommandCode { get; set; }
    }
    public interface IComScanner
    {
        public delegate void RunCommandCodeInvoker(CommandCode objCC);
        void RunCommandCode(CommandCode objCC);
    }
    public class ComScanner
    {
        private int m_iBaudRate;
        private Parity m_eParity;
        private int m_iDataBits;
        private StopBits m_eStopBits;

        private IComScanner m_objCallForm;
        private SerialPort withEventsField_m_modComScanner = new SerialPort();
        public SerialPort m_modComScanner
        {
            get { return withEventsField_m_modComScanner; }
            set
            {
                if (withEventsField_m_modComScanner != null)
                {
                    withEventsField_m_modComScanner.DataReceived -= m_modComScanner_DataReceived;
                }
                withEventsField_m_modComScanner = value;
                if (withEventsField_m_modComScanner != null)
                {
                    withEventsField_m_modComScanner.DataReceived += m_modComScanner_DataReceived;
                }
            }
        }
        public event CommandCodeReceivedEventHandler CommandCodeReceived;
        public delegate void CommandCodeReceivedEventHandler(object sender, CommandCode objCC);

        public bool ComScanOpen()
        {

            // Set the com port setttings for the scanner
            var _with1 = m_modComScanner;
            _with1.BaudRate = 9600;
            _with1.Parity = System.IO.Ports.Parity.None;
            _with1.DataBits = 8;
            _with1.StopBits = System.IO.Ports.StopBits.One;

            try
            {
                // Open the scanner object
                m_modComScanner.Open();
            }
            catch (Exception ex)
            {
                // Ignore it... scanner isn't connected
                ex = null;
            }

            return m_modComScanner.IsOpen;
        }


        public void ComScanClose()
        {
            if (m_modComScanner.IsOpen)
                m_modComScanner.Close();

            m_modComScanner.Dispose();
            m_modComScanner = null;
        }

        public int BaudRate
        {
            get { return m_iBaudRate; }
            set { m_iBaudRate = value; }
        }

        public Parity eParity
        {
            get { return m_eParity; }
            set { m_eParity = value; }
        }

        public int DataBits
        {
            get { return m_iDataBits; }
            set { m_iDataBits = value; }
        }

        public StopBits eStopBits
        {
            get { return m_eStopBits; }
            set { m_eStopBits = value; }
        }


        public void m_modComScanner_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (m_modComScanner.IsOpen)
            {
                string sCode = m_modComScanner.ReadExisting();

                // Use an array in case the scanner catches more than one code
                string[] asCodes = sCode.Split(Convert.ToChar(Constants.vbCrLf));

                for (int i = 0; i <= Information.UBound(asCodes); i++)
                {
                    if (!string.IsNullOrEmpty(asCodes(i).Trim()))
                    {
                        CommandCode objCC = new CommandCode();
                        objCC.CommandCode = asCodes(i);

                        if (CommandCodeReceived != null)
                        {
                            CommandCodeReceived(this, objCC);
                        }

                    }
                }
            }

        }

    }
}
