using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using udp_turn_off;

namespace udp_turn_off_Service
{

    public partial class Service1 : ServiceBase
    {
        Form1 Form1 = new Form1();
        public Service1()
        {
            InitializeComponent();
        }

        //服务启动
        protected override void OnStart(string[] args)
        {
            //监听 UDP 端口
            Thread recvThread = new Thread(RecvMsg);
            recvThread.IsBackground = true;
            recvThread.Start();

            Form1.SendMsg("the_computer_is_on");
        }

        //服务停止
        protected override void OnStop()
        {
        }
        //休眠、睡眠
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
        public void RecvMsg()
        {
            string Reg_port = Regedit.Read(@"SOFTWARE\\tty228\\udp_turn_off", "port", "LocalMachine");
            string Reg_msg = Regedit.Read(@"SOFTWARE\\tty228\\udp_turn_off", "msg", "LocalMachine");
            string Reg_Shutdown_Options = Regedit.Read(@"SOFTWARE\\tty228\\udp_turn_off", "Shutdown_Options", "LocalMachine");
            //Form1.SendMsg(Reg_port);
            //Form1.SendMsg(Reg_msg);
            //Form1.SendMsg(Reg_Shutdown_Options);

            UdpClient recvClient = new UdpClient(new IPEndPoint(IPAddress.Any, int.Parse(Reg_port)));//接收方的IP
            while (1 == 1)
            {
                IPEndPoint remoteHost = null;
                byte[] recvByte = recvClient.Receive(ref remoteHost);
                string msg = Encoding.UTF8.GetString(recvByte);
                if (msg == Reg_msg)
                {
                    if (Reg_Shutdown_Options == "shutdown")
                    {
                        Process.Start("shutdown", "/s /t 0");
                        //Form1.SendMsg("shutdown_the_pc");
                    }
                    if (Reg_Shutdown_Options == "dormancy")
                    {
                        SetSuspendState(true, true, true);
                        //Form1.SendMsg("dormancy_the_pc");
                    }
                    if (Reg_Shutdown_Options == "sleep")
                    {
                        SetSuspendState(false, true, true);
                        //Form1.SendMsg("sleep_the_pc");
                    }
                }
                else if (msg == "is_the_computer_on?")
                {
                    Form1.SendMsg("the_computer_is_on");
                }
            }
        }
    }
}
