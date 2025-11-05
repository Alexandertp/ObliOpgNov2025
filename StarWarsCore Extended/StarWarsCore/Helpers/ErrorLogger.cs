using System;
using System.IO;

namespace StarWarsCore.Helpers
{
    public class ErrorLogger
    {
        private static string _rootPath = Directory.GetCurrentDirectory();

        // Because this method is defined as public static, 
        // we don't need to instantiate a new ErrorLogger() before using it.
        public static void SaveMsg(string errmsg)
        {         
            using (StreamWriter w = File.AppendText(_rootPath + "/Logfiles/log.txt"))
            {
                Log(errmsg, w);                
                // Close the writer and underlying file.
                w.Close();
            }           
        }
        
        private static void Log(String logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());            
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
            // Update the underlying file.
            w.Flush();
        }
       
    }
}


