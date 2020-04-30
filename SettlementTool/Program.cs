using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SettlementTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AutoRegister();
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (process.MainModule.FileName
                    == current.MainModule.FileName)
                    {
                        MessageBoxEx.Show("程序已经运行！", Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        private static void AutoRegister()
        {
            RegisterDllOcx("HSUNFK.dll");
            RegisterDllOcx("QHKS.dll");         
            RegisterDllOcx("HSCPID.dll");
        }
        unsafe internal delegate UInt32 DllRegisterServer();
        [DllImport("kernel32.dll")]
        private extern static IntPtr LoadLibrary(String LibFileName);
        [DllImport("kernel32.dll")]
        private extern static IntPtr GetProcAddress(IntPtr hModule, String ProcName);
        [DllImport("kernel32.dll")]
        private extern static bool FreeLibrary(IntPtr hModule);
        private static void RegisterDllOcx(string fileName)
        {
            IntPtr hLib = LoadLibrary(fileName);
            if (hLib != IntPtr.Zero)
            {
                IntPtr proc = GetProcAddress(hLib, "DllRegisterServer");
                if (proc != IntPtr.Zero)
                {
                    try
                    {
                        DllRegisterServer drs = (DllRegisterServer)Marshal.GetDelegateForFunctionPointer(proc, typeof(DllRegisterServer));
                        drs();
                    }
                    catch
                    {
                    }
                    finally
                    {
                    }
                }
                FreeLibrary(hLib);
            }
        }

    }
}
