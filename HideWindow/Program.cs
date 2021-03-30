using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace HideWindow
{
    internal class Program
    {
        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        [STAThread]
        private static void Main()
        {
            Process[] processes = Process.GetProcesses();
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            if (commandLineArgs.Length > 1)
            {
                string[] array = commandLineArgs;
                foreach (string value in array)
                {
                    Process[] array2 = processes;
                    foreach (Process process in array2)
                    {
                        try
                        {
                            if (process.Id == Convert.ToInt32(value))
                            {
                                int hwnd = process.MainWindowHandle.ToInt32();
                                ShowWindow(hwnd, 0);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(defaultValue: false);
            Application.Run(new Form1());
        }
    }
}
