using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace D2REditorLauncher
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var main = new MainForm();
            main.Init();

            bool exist1 = main.IsDownloadCompleted();

            List<string> files = new List<string>();
            bool exist2 = main.CheckUpdate(ref files);

            if (exist1 && !exist2)
            {
                main.QuitNow();
            }
            else
            {
                Application.Run(main);
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            MessageBox.Show("发生了未知错误！\r\n" + ex.ToString() + "\r\n" + ex.StackTrace);
        }
    }
}
