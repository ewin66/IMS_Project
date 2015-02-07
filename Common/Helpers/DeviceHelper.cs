using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    public class DeviceHelper
    {
        /// <summary>
        /// Compile an array of COM port names associated with given VID and PID
        /// </summary>
        /// <param name="VID"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        List<string> ComPortNames(String VID, String PID)
        {
            String pattern = String.Format("^VID_{0}.PID_{1}", VID, PID);
            Regex _rx = new Regex(pattern, RegexOptions.IgnoreCase);
            List<string> comports = new List<string>();
            RegistryKey rk1 = Registry.LocalMachine;
            RegistryKey rk2 = rk1.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum");
            foreach (String s3 in rk2.GetSubKeyNames())
            {
                RegistryKey rk3 = rk2.OpenSubKey(s3);
                foreach (String s in rk3.GetSubKeyNames())
                {
                    if (_rx.Match(s).Success)
                    {
                        RegistryKey rk4 = rk3.OpenSubKey(s);
                        foreach (String s2 in rk4.GetSubKeyNames())
                        {
                            RegistryKey rk5 = rk4.OpenSubKey(s2);
                            RegistryKey rk6 = rk5.OpenSubKey("Device Parameters");
                            comports.Add((string)rk6.GetValue("PortName"));
                        }
                    }
                }
            }
            return comports;
        } 

        /// <summary>
        /// Compile an array of COM port names associated with given VID and PID
        /// </summary>
        /// <param name="VID">string representing the vendor id of the USB/Serial convertor</param>
        /// <param name="PID">string representing the product id of the USB/Serial convertor</param>
        /// <returns></returns>
        public static List<string> GetPortByVPid(String VID, String PID)
        {
            String pattern = String.Format("^VID_{0}.PID_{1}", VID, PID);
            Regex _rx = new Regex(pattern, RegexOptions.IgnoreCase);
            List<string> comports = new List<string>();
            RegistryKey rk1 = Registry.LocalMachine;
            RegistryKey rk2 = rk1.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum");
            foreach (String s3 in rk2.GetSubKeyNames())
            {
                RegistryKey rk3 = rk2.OpenSubKey(s3);
                foreach (String s in rk3.GetSubKeyNames())
                {
                    if (_rx.Match(s).Success)
                    {
                        RegistryKey rk4 = rk3.OpenSubKey(s);
                        foreach (String s2 in rk4.GetSubKeyNames())
                        {
                            RegistryKey rk5 = rk4.OpenSubKey(s2);
                            RegistryKey rk6 = rk5.OpenSubKey("Device Parameters");
                            comports.Add((string)rk6.GetValue("PortName"));
                        }
                    }
                }
            }
            return comports;
        }

        /// <summary>
        /// Removes any comm ports that are not explicitly defined as allowed in ALLOWED_TYPES
        /// </summary>
        /// <param name="allPorts">reference to List that will be checked</param>
        /// <returns></returns>
        public static void NullModemCheck(ref List<string> allPorts)
        {
            // Open registry to get the COM Ports available with the system
            RegistryKey regKey = Registry.LocalMachine;
            regKey = regKey.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum"); //(REG_COM_STRING);

            Dictionary<string, string> tempDict = new Dictionary<string, string>();
            foreach (string p in allPorts)
                tempDict.Add(p, p);

            // This holds any matches we may find
            string match = "";
            foreach (string subKey in regKey.GetValueNames())
            {
                // Name must contain either VCP or Seial to be valid. Process any entries NOT matching
                // Compare to subKey (name of RegKey entry)
                if (!(subKey.Contains("Serial") || subKey.Contains("VCP")))
                {
                    // Okay, this might be an illegal port.
                    // Peek in the dictionary, do we have this key? Compare to regKey.GetValue(subKey)
                    if (tempDict.TryGetValue(regKey.GetValue(subKey).ToString(), out match))
                    {
                        // Kill it!
                        allPorts.Remove(match);

                        // Reset our output string
                        match = "";
                    }

                }

            }

            regKey.Close();
        }
    }
}
