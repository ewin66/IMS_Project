using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.Helpers
{
    public class FileHelper
    {
        //Progress indikator dodeka se ceka na fajlto da bide osloboden: http://www.experts-exchange.com/Programming/Languages/C_Sharp/Q_27981523.html
        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        /// <summary>
        /// Blocks until the file is not locked any more.
        /// </summary>
        /// <param name="fullPath"></param>
        public static bool WaitForFile(string fullPath)
        {
            int numTries = 0;
            while (true)
            {
                ++numTries;
                try
                {
                    // Attempt to open the file exclusively.
                    using (FileStream fs = new FileStream(fullPath,
                        FileMode.Open, FileAccess.ReadWrite,
                        FileShare.None, 100))
                    {
                        fs.ReadByte();

                        // If we got this far the file is ready
                        break;
                    }
                }
                catch (Exception ex)
                {
                    //Log.LogWarning("WaitForFile {0} failed to get an exclusive lock: {1}",fullPath, ex.ToString());

                    if (numTries > 10)
                    {
                        //Log.LogWarning("WaitForFile {0} giving up after 10 tries",fullPath);
                        return false;
                    }

                    // Wait for the lock to be released
                    System.Threading.Thread.Sleep(500);
                }
            }

            //Log.LogTrace("WaitForFile {0} returning true after {1} tries",fullPath, numTries);
            return true;
        }
    }
}
