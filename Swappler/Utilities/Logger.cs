using System;
using System.Configuration;
using System.IO;

namespace Swappler.Utilities
{
    public enum LogType
    {
        Event,
        Exception,
    }
    public static class Logger
    {
        public static readonly string RootDirectory;
        private static readonly string NewLine = Environment.NewLine;
        static Logger()
        {
            RootDirectory = ConfigurationManager.AppSettings["Logger.RootDirectory"];
        }

        public static string ExceptionMessage(Exception exception)
        {
            string message = string.Empty;

            var currentException = exception;
            while (currentException!=null)
            {
                message += "Exception name: " + currentException.GetType().Name + NewLine;
                message += "Exception message: "+currentException.Message + NewLine;
                currentException = currentException.InnerException;
            }

            return message;
        }

        public static void Write(LogType logType, string message)
        {
            // Pluralizing main directory name depending on LogType
            string logTypeDirectory = 
                logType.ToString().EndsWith("s")
                ? logType + "es"
                : logType + "s";

            string filePath = Logger.RootDirectory+@"\"+logTypeDirectory+@"\";

            string fileExtension = "log";

            string fileName = logType+"_"+DateTime.Now.ToString("dd-MM-yyyy")+"."+fileExtension;

            // Format is [yyyy/MM/dd HH:mm:ss]         
            string dateTime = "["+DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")+"] ";

            string dataToAppend = dateTime + NewLine + message + NewLine;
            
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            File.AppendAllText(filePath+"\\"+ fileName, dataToAppend);
        }
    }
}