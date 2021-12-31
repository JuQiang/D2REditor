using D2REditor.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace D2REditor
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length != 1) return;
            bool safe = false;


            int pid = -1;
            if (args.Length > 0 && Int32.TryParse(args[0], out pid))
            {
                try
                {
                    safe = (System.Diagnostics.Process.GetProcessById(pid).ProcessName.ToLower() == "d2reditorlauncher");
                    WriteLog(String.Format("{0},{1},{2}", args[0], System.Diagnostics.Process.GetProcessById(pid).ProcessName.ToLower(), safe.ToString()));
                    //throw new Exception(String.Format("{0},{1}", args[0], System.Diagnostics.Process.GetProcessById(pid).ProcessName.ToLower()));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (!safe) return;

            WriteLog("Begin call select d2r");
            Application.Run(new FormSelectD2R());
        }

        static void WriteLog(string msg)
        {
            using (StreamWriter sw = new StreamWriter("log.txt", true))
            {
                sw.WriteLine(msg);
            }
        }
    }
}
