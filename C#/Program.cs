using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace udp_turn_off
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            LoadResoureDll.RegistDLL();

            //如果有参数就忽略
            //禁止多开
            if (args.Length == 0)
            {
                Process instance = RunningInstance();
                if (instance == null)
                {
                    Application.EnableVisualStyles();
                    Application.DoEvents();
                    Application.Run(new Form1(args));
                }
                else
                {
                    EndProcess(instance.Id);
                }
            }
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }
        public static Process RunningInstance()
        {

            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 结束指定进程
        /// </summary>
        /// <param name="pid">进程的 Process ID</param>
        public static bool EndProcess(int pid)
        {

            Process process = Process.GetProcessById(pid);
            if (process == null)
            {
                return false;
            }
            else
            {
                process.Kill();
                process.WaitForExit();
                process.Close();
                return true;
            }
        }

    }
}