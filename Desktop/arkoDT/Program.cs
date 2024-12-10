using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace arkoDT
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Handle UI thread exceptions
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            // Handle non-UI thread exceptions
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmLogin());
            }
            catch (Exception ex)
            {
                // Log any exception that occurs during startup
                LogException(ex);
            }
        }

        // Method to log exceptions to a file
        private static void LogException(Exception ex)
        {
            string logFilePath = @"C:Documents\app_log.txt";
            string logMessage = $"Exception occurred at {DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n\n";
            File.AppendAllText(logFilePath, logMessage);
        }

        // Handles exceptions thrown in UI threads
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogException(e.Exception);
        }

        // Handles exceptions thrown in non-UI threads
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            LogException(ex);
        }
    }
}
