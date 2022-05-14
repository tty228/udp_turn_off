using Microsoft.Win32;
using System;
using System.Collections;
using System.Configuration.Install;
using System.Diagnostics;//命名空间关机，重启，注销，锁定，休眠，睡眠
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;//命名空间关机，重启，注销，锁定，休眠，睡眠
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace udp_turn_off
{
    //by tty228
    //https://github.com/tty228
    public partial class Form1 : Form
    {
        string[] args;
        public static Form1 frm1;//一个类中调用另一个窗体的控件
        public Form1(string[] args)//检测启动参数
        {
            this.args = args;
            InitializeComponent();
            frm1 = this;//一个类中调用另一个窗体的控件
        }

        private void Start_Logo()
        {
            notifyIcon1.ShowBalloonTip(10000, "软件已启动", "现在是：" + DateTime.Now.ToString("MM/dd dddd HH:mm:ss") + "\r\n" + ChinaDate.logo(), ToolTipIcon.None);
            //None（无）、Info（蓝色感叹号）、Warnning（黄色感叹号）、Error（小红叉）
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //监听气泡点击 所有托盘行为都要重新链接，暂时弃用
            //notifyIcon1 = new NotifyIcon();
            //notifyIcon1.BalloonTipClicked += new EventHandler(notifyIcon_BalloonTipClicked);
            //notifyIcon1.MouseDoubleClick += new MouseEventHandler(NotifyIcon1_MouseDoubleClick);
            //notifyIcon1.Visible = true;

            Icon = Properties.Resources._001;
            notifyIcon1.Icon = Properties.Resources._001; //更改托盘图标
            EnableElevateIcon_BCM_SETSHIELD(button4); //按钮显示 UAC 小盾牌
            Start_Logo(); //气泡logo

            if (this.IsServiceExisted(serviceName) && this.ServiceIsRunning(serviceName)) this.ServiceStop(serviceName); //如果服务存在且在运行，关闭它，避免端口冲突

            //初始化设置
            if (Regedit.Read("Software\\tty228\\udp_turn_off", "countdown", "") == "" || Regedit.Read("Software\\tty228\\udp_turn_off", "port", "") == "" || Regedit.Read("Software\\tty228\\udp_turn_off", "msg", "") == "" || Regedit.Read("Software\\tty228\\udp_turn_off", "Shutdown_Options", "") == "")
            {
                Button1_Click(null, null);
                Button2_Click(null, null);
            }
            else
            {
                textBox1.Text = Regedit.Read("Software\\tty228\\udp_turn_off", "countdown", "");
                textBox2.Text = Regedit.Read("Software\\tty228\\udp_turn_off", "port", "");
                textBox3.Text = Regedit.Read("Software\\tty228\\udp_turn_off", "msg", "");
                switch (Regedit.Read("Software\\tty228\\udp_turn_off", "Shutdown_Options", ""))
                {
                    case "shutdown":
                        ToolStripMenuItem_shutdown_Click(null, null);
                        break;
                    case "dormancy":
                        ToolStripMenuItem_dormancy_Click(null, null);
                        break;
                    case "sleep":
                        ToolStripMenuItem_sleep_Click(null, null);
                        break;
                    case "LockWorkStation":
                        ToolStripMenuItem_LockWorkStation_Click(null, null);
                        break;
                    default:
                        ToolStripMenuItem_shutdown_Click(null, null);
                        break;
                }
                if (Regedit.Read("Software\\Microsoft\\Windows\\CurrentVersion\\Run", "udp_turn_off", "") == "")
                {
                    checkBox1.Checked = false;
                    开机启动ToolStripMenuItem.Checked = false;
                }
                else
                {
                    checkBox1.Checked = true;
                    开机启动ToolStripMenuItem.Checked = true;
                }

                if (!this.IsServiceExisted(serviceName)) DeleteDirectory(serviceFolderPath);

            }

            //等待服务端口关闭
            for (int i = 1; i < 100; i++)
            {
                if (PortInUse(int.Parse(textBox2.Text)) == false) i = 100;
                Thread.Sleep(100);
            }

            //关闭超时或被其他软件占用
            if (PortInUse(int.Parse(textBox2.Text)) == true)
            {
                MessageBox.Show("通常每个套接字地址(协议/网络地址/端口)只允许使用一次。\n\n请重新设置。", "端口被占用");
                this.Opacity = 100;
            }
            else
            {
                this.BeginInvoke(new Action(() => { this.Hide(); this.Opacity = 1; }));//隐藏窗口并透明化
            }
            //监听 UDP 端口
            Thread recvThread = new Thread(RecvMsg);
            recvThread.IsBackground = true;
            recvThread.Start();

            SystemEvents.PowerModeChanged += OnPowerChange; //监听电源改变事件
            SendMsg("the_computer_is_on");
            
        }

        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        /// <summary>
        /// 检测UDP端口是否被占用
        /// </summary>
        /// <param name="port"> 端口号 </param>
        /// <returns></returns>
        public static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveUdpListeners();//UDP端口
            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    return inUse;
                }
            }
            return inUse;
        }

        //等待网络可用
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);

        private void Waiting_for_networking()
        {
            System.Int32 dwFlag = new int();
            while (!InternetGetConnectedState(ref dwFlag, 0))
            {
                //Application.DoEvents();
                Thread.Sleep(1000);
            }
            Thread.Sleep(1000);
        }

        /// <summary>
        /// 监听UDP端口
        /// </summary>
        private void RecvMsg()
        {
            if (PortInUse(int.Parse(textBox2.Text)) == false)
            {
                UdpClient recvClient = new UdpClient(new IPEndPoint(IPAddress.Any, int.Parse(textBox2.Text)));//接收方的IP
                while (1 == 1)
                {
                    IPEndPoint remoteHost = null;
                    byte[] recvByte = recvClient.Receive(ref remoteHost);
                    string msg = Encoding.UTF8.GetString(recvByte);
                    if (msg == textBox3.Text)
                    {
                        Countdown();
                    }
                    else if (msg == "is_the_computer_on?")
                    {
                        SendMsg("the_computer_is_on");
                    }
                }
            }
        }

        private System.Timers.Timer timer_msg = new System.Timers.Timer();

        /// <summary>
        /// UDP 发送广播信息
        /// </summary>
        public void SendMsg(string Msg)
        {
            timer_msg.Elapsed += new ElapsedEventHandler(OnTimer); //创建 timer
            timer_msg.AutoReset = false; //只运行一次
            timer_msg.Start();
            void OnTimer(Object source, ElapsedEventArgs e)
            {
                Waiting_for_networking(); //等待网络连接
                IPEndPoint broadcastIpEndPoint;
                broadcastIpEndPoint = new IPEndPoint(IPAddress.Broadcast, 2333); //广播到 2333 端口
                UdpClient client = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
                byte[] buf = Encoding.UTF8.GetBytes(Msg);
                client.Send(buf, buf.Length, broadcastIpEndPoint);
                timer_msg.Stop();
            }
        }

        /// <summary>
        /// 监听 电源改变事件
        /// </summary>
        private void OnPowerChange(object s, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    SendMsg("the_computer_is_on");
                    Start_Logo();
                    break;
                case PowerModes.Suspend:
                    SendMsg("the_computer_is_about_to_shut_down");
                    break;
            }
        }

        //休眠、睡眠
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        //锁定
        [DllImport("user32")]
        public static extern void LockWorkStation();

        //注销
        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        //窗口倒计时
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string IpClassName, string IpWindowName);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowText(IntPtr hWnd, string text);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        /// <summary>
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        const int WM_CLOSE = 0x10;
        int t;

        /// <summary>
        /// 执行关机操作
        /// </summary>
        private void Turn_off()
        {
            if (ToolStripMenuItem_shutdown.Checked == true)
            {
                Process.Start("shutdown", "/s /t 0");  // 参数 /s 的意思是要关闭计算机 参数 /t 0 的意思是告诉计算机 0 秒之后执行命令
            }
            if (ToolStripMenuItem_dormancy.Checked == true)
            {
                SetSuspendState(true, true, true);
            }
            if (ToolStripMenuItem_sleep.Checked == true)
            {
                SetSuspendState(false, true, true);
            }
            if (ToolStripMenuItem_LockWorkStation.Checked == true)
            {
                LockWorkStation();
            }
        }

        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1); //窗体置顶
        public const uint SWP_NOMOVE = 0x0002; //不调整窗体位置
        public const uint SWP_NOSIZE = 0x0001; //不调整窗体大小
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
        private void Timer1_Tick(object sender, EventArgs e)
        {
            IntPtr hwnd = FindWindow(null, "系统将于" + t.ToString() + "秒后关机");
            t--;
            SetWindowText(hwnd, "系统将于" + t.ToString() + "秒后关机");
            SetWindowPos(hwnd, HWND_TOPMOST, 1, 1, 1, 1, SWP_NOMOVE | SWP_NOSIZE);

            if (hwnd == IntPtr.Zero && t != 0)//如果窗口已经关闭
            {
                timer1.Enabled = false;
            }
            if (t == 0)
            {
                timer1.Enabled = false;
                SendMessage(hwnd, WM_CLOSE, 0, 0);
                Turn_off();
            }
        }

        /// <summary>
        /// 开启关机倒计时
        /// </summary>
        private void Countdown()
        {
            DialogResult result;
            t = int.Parse(textBox1.Text);
            timer1.Enabled = true;
            result = MessageBox.Show("系统关机提示：\n\n 立即关机请点击【确定】\n\n 取消请点击【取消】", "系统将于" + t + "秒后关机", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.OK)
            {
                Turn_off();
            }
            else if (t > 0)
            {
                SendMsg("the_computer_is_on");
            }
        }

        private void ToolStripMenuItem_shutdown_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_shutdown.Checked = true;
            ToolStripMenuItem_dormancy.Checked = false;
            ToolStripMenuItem_sleep.Checked = false;
            ToolStripMenuItem_LockWorkStation.Checked = false;
            comboBox1.Text = "关机";
            Regedit.Save("Software\\tty228\\udp_turn_off", "Shutdown_Options", "shutdown", "");
            Regedit.Save("Software\\WOW6432Node\\tty228\\udp_turn_off", "Shutdown_Options", "shutdown", "LocalMachine");
        }

        private void ToolStripMenuItem_dormancy_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_shutdown.Checked = false;
            ToolStripMenuItem_dormancy.Checked = true;
            ToolStripMenuItem_sleep.Checked = false;
            ToolStripMenuItem_LockWorkStation.Checked = false;
            comboBox1.Text = "休眠";
            Regedit.Save("Software\\tty228\\udp_turn_off", "Shutdown_Options", "dormancy", "");
            Regedit.Save("Software\\WOW6432Node\\tty228\\udp_turn_off", "Shutdown_Options", "dormancy", "LocalMachine");
        }

        private void ToolStripMenuItem_sleep_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_shutdown.Checked = false;
            ToolStripMenuItem_dormancy.Checked = false;
            ToolStripMenuItem_sleep.Checked = true;
            ToolStripMenuItem_LockWorkStation.Checked = false;
            comboBox1.Text = "睡眠";
            Regedit.Save("Software\\tty228\\udp_turn_off", "Shutdown_Options", "sleep", "");
            Regedit.Save("Software\\WOW6432Node\\tty228\\udp_turn_off", "Shutdown_Options", "sleep", "LocalMachine");
        }

        private void ToolStripMenuItem_LockWorkStation_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_shutdown.Checked = false;
            ToolStripMenuItem_dormancy.Checked = false;
            ToolStripMenuItem_sleep.Checked = false;
            ToolStripMenuItem_LockWorkStation.Checked = true;
            comboBox1.Text = "锁定";
            Regedit.Save("Software\\tty228\\udp_turn_off", "Shutdown_Options", "LockWorkStation", "");
        }

        private void 开机启动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (开机启动ToolStripMenuItem.Checked == false)
            {
                checkBox1.Checked = true;
                开机启动ToolStripMenuItem.Checked = true;
                Regedit.Save("Software\\Microsoft\\Windows\\CurrentVersion\\Run", "udp_turn_off", Process.GetCurrentProcess().MainModule.FileName, "");
            }
            else
            {
                checkBox1.Checked = false;
                开机启动ToolStripMenuItem.Checked = false;
                Regedit.Delete("Software\\Microsoft\\Windows\\CurrentVersion\\Run", "udp_turn_off", "");
            }
        }

        /// <summary>
        /// 托盘右键退出
        /// </summary>
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        // 设置此窗体为活动窗体：
        // 将创建指定窗口的线程带到前台并激活该窗口。键盘输入直接指向窗口，并为用户更改各种视觉提示。
        // 系统为创建前台窗口的线程分配的优先级略高于其他线程。
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        // 设置此窗体为活动窗体：
        // 激活窗口。窗口必须附加到调用线程的消息队列。
        [DllImport("user32.dll", EntryPoint = "SetActiveWindow")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        /// <summary>
        /// 双击托盘显示设置窗体
        /// </summary>
        /// void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Opacity = 100;
            this.Show();
            if (this.IsServiceExisted(serviceName))
            {
                button4.Text = "关闭服务";
            }
            else
            {
                button4.Text = "注册为服务";
            }
        }

        //监听气泡点击
        void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            MessageBox.Show(ChinaDate.logo());
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotifyIcon1_MouseDoubleClick(null, null);
        }

        /// <summary>
        /// 下一个节日--点击行为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 下一个节日的日期ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ChinaDate.NextDay(DateTime.Now));
        }

        //窗体关闭事件
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            e.Cancel = true;
            this.Hide();

            //判断是否为 windows 关闭事件
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                SendMsg("the_computer_is_about_to_shut_down");
            }
        }

        /// <summary>
        /// 恢复默认设置
        /// </summary>
        private void Button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "5";
            textBox2.Text = "8080";
            textBox3.Text = "turn_off_the_computer";
            ToolStripMenuItem_shutdown_Click(null, null);
            checkBox1.Checked = false;
        }

        /// <summary>
        /// 限制输入（只能输入数字）
        /// </summary>
        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        /// <summary>
        /// 限制输入（只能输入0-100）
        /// </summary>
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                textBox1.Text = 0.ToString();
            int number = int.Parse(textBox1.Text);
            textBox1.Text = number.ToString();
            if (number <= 100)
            {
                return;
            }
            textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        /// <summary>
        /// 限制输入（只能输入数字）
        /// </summary>
        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        /// <summary>
        /// 限制输入（只能输入0-65535）
        /// </summary>
        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                textBox2.Text = 0.ToString();
            int number = int.Parse(textBox2.Text);
            textBox2.Text = number.ToString();
            if (number <= 65535)
            {
                return;
            }
            textBox2.Text = textBox2.Text.Substring(0, textBox2.Text.Length - 1);
            textBox2.SelectionStart = textBox2.Text.Length;
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        private void Button2_Click(object sender, EventArgs e)
        {
            Regedit.Save("Software\\tty228\\udp_turn_off", "countdown", textBox1.Text, "");
            Regedit.Save("Software\\tty228\\udp_turn_off", "port", textBox2.Text, "");
            Regedit.Save("Software\\tty228\\udp_turn_off", "msg", textBox3.Text, "");

            Regedit.Save(@"Software\\WOW6432Node\\tty228\\udp_turn_off", "port", textBox2.Text, "LocalMachine");
            Regedit.Save(@"Software\\WOW6432Node\\tty228\\udp_turn_off", "msg", textBox3.Text, "LocalMachine");

            switch (comboBox1.Text)
            {
                case "关机":
                    ToolStripMenuItem_shutdown_Click(null, null);
                    break;
                case "休眠":
                    ToolStripMenuItem_dormancy_Click(null, null);
                    break;
                case "睡眠":
                    ToolStripMenuItem_sleep_Click(null, null);
                    break;
                case "锁定":
                    ToolStripMenuItem_LockWorkStation_Click(null, null);
                    break;
                default:
                    ToolStripMenuItem_shutdown_Click(null, null);
                    break;
            }
            if (checkBox1.Checked != 开机启动ToolStripMenuItem.Checked)
            {
                开机启动ToolStripMenuItem_Click(null, null);
            }
            notifyIcon1.Visible = false;
            Application.Restart();
            //Form1_Load(null, null);
            
        }

        /// <summary>
        /// 右下角菜单_暂停屏幕休眠--点击行为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_PauseSleep_Click(object sender, EventArgs e)
        {
            SystemSleepAPI.ResotreSleep();
            ToolStripMenuItem_PauseSleep.Checked = true;
            ToolStripMenuItem_DisplaySleep.Checked = false;
            ToolStripMenuItem_SystemSleep.Checked = false;
            //notifyIcon1.Icon = Properties.Resources._002;
        }

        /// <summary>
        /// 右下角菜单_禁用系统休眠--点击行为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_SystemSleep_Click(object sender, EventArgs e)
        {
            SystemSleepAPI.SystemSleep();
            ToolStripMenuItem_PauseSleep.Checked = false;
            ToolStripMenuItem_SystemSleep.Checked = true;
            ToolStripMenuItem_DisplaySleep.Checked = false;
            //NotifyIcon1.Icon = Properties.Resources._001;
        }

        /// <summary>
        /// 右下角菜单_禁用屏幕休眠--点击行为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_DisplaySleep_Click(object sender, EventArgs e)
        {
            SystemSleepAPI.DisplaySleep();
            ToolStripMenuItem_PauseSleep.Checked = false;
            ToolStripMenuItem_SystemSleep.Checked = false;
            ToolStripMenuItem_DisplaySleep.Checked = true;
            //NotifyIcon1.Icon = Properties.Resources._001;
        }

        /// <summary>
        /// 关于
        /// </summary>
        private void Button3_Click(object sender, EventArgs e)
        {
            AboutBox1 about_box = new AboutBox1();
            about_box.Show(this);
        }
        string serviceFolderPath = @"C:\Program Files\udp_turn_off\";
        string serviceFilePath = @"C:\Program Files\udp_turn_off\udp_turn_off_Service.exe";
        string serviceName = "udp_turn_off_Service";

        /// <summary>
        /// 判断服务是否存在
        /// </summary>
        /// <param name="serviceName">要判断的Windows服务名称</param>
        /// <returns>存在返回true，否则返回false</returns>
        private bool IsServiceExisted(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController sc in services)
            {
                if (sc.ServiceName.ToLower() == serviceName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断服务是否运行
        /// </summary>
        /// <param name="serviceName">要判断的Windows服务名称</param>
        /// <returns>处于运行状态返回true，否则返回false</returns>
        private bool ServiceIsRunning(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Running)
                {
                    return true;
                }
            }
            return false;
        }

        //安装服务
        private void InstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                IDictionary savedState = new Hashtable();
                installer.Install(savedState);
                installer.Commit(savedState);
            }
        }

        //卸载服务
        private void UninstallService()
        {
            string Path = Regedit.Read("SYSTEM\\CurrentControlSet\\Services\\udp_turn_off_Service", "ImagePath", "LocalMachine");
            string newstr = Path.Replace("\"", "");
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = newstr;
                installer.Uninstall(null);
            }
        }
        //启动服务
        private void ServiceStart(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Stopped)
                {
                    control.Start();
                }
            }
        }

        //停止服务
        private void ServiceStop(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Running)
                {
                    control.Stop();
                }
            }
        }

        //注册服务
        private void Enable_Service()
        {
            //导出嵌入的资源
            byte[] res;//创建byte数组，装资源
            res = new byte[Properties.Resources.udp_turn_off_Service.Length];//确定数组大小。
            Properties.Resources.udp_turn_off_Service.CopyTo(res, 0);//将资源导入byte数组中
            if (!Directory.Exists(serviceFolderPath)) Directory.CreateDirectory(serviceFolderPath);//创建该文件夹
            FileStream fs = new FileStream(serviceFilePath, FileMode.Create, FileAccess.Write);
            fs.Write(res, 0, res.Length);
            fs.Close();

            //复制自身到指定文件夹,提供程序集支持
            File.Copy(Application.ExecutablePath, serviceFolderPath + "udp_turn_off.exe", true);

            //安装服务
            if (this.IsServiceExisted(serviceName))
            {
                this.ServiceStop(serviceName);
                this.UninstallService();
            }
            this.InstallService(serviceFilePath);

            //启动服务
            //if (this.IsServiceExisted(serviceName)) this.ServiceStart(serviceName);
        }

        //禁用并删除服务
        private void Disable_Service()
        {
            //停止服务
            if (this.IsServiceExisted(serviceName)) this.ServiceStop(serviceName);

            //卸载服务
            if (this.IsServiceExisted(serviceName))
            {
                this.ServiceStop(serviceName);
                this.UninstallService();
            }
        }

        //删除文件夹
        static void DeleteDirectory(string path)
        {
            // 如果文件夹存在则进入目录下
            if (Directory.Exists(path))
            {
                foreach (string p in Directory.GetFileSystemEntries(path))// 返回所有文件及目录
                {
                    if (File.Exists(p))
                    {
                        File.Delete(p);// 删除文件
                    }
                    else
                    {
                        DeleteDirectory(p);// 删除子目录
                    }
                }
                Directory.Delete(path, true);// 删除当前空目录
            }
        }

        // 给 UAC 添加小盾牌
        private void EnableElevateIcon_BCM_SETSHIELD(Button ThisButton)
        {
            // Input validation, validate that ThisControl is not null
            if (ThisButton == null)
            {
                return;
            }

            // Define BCM_SETSHIELD locally, declared originally in Commctrl.h
            uint BCM_SETSHIELD = 0x0000160C;

            // Set button style to the system style
            ThisButton.FlatStyle = FlatStyle.System;

            // Send the BCM_SETSHIELD message to the button control
            SendMessage(new System.Runtime.InteropServices.HandleRef(ThisButton, ThisButton.Handle), BCM_SETSHIELD, new IntPtr(0), new IntPtr(1));
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(System.Runtime.InteropServices.HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private void Button4_Click(object sender, EventArgs e)
        {
            if (this.IsServiceExisted(serviceName))
            {
                Disable_Service();
                MessageBox.Show("服务已关闭");
                button4.Text = "注册为服务";
                checkBox1.Checked = false;
                开机启动ToolStripMenuItem.Checked = false;
                Button2_Click(null, null);
            }
            else
            {
                Enable_Service();
                MessageBox.Show("服务已注册");
                button4.Text = "关闭服务";
                checkBox1.Checked = true;
                开机启动ToolStripMenuItem.Checked = true;
                Button2_Click(null, null);
            }
        }
    }
}