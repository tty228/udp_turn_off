
namespace udp_turn_off
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label3 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.关机选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_LockWorkStation = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_sleep = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_dormancy = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_shutdown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.下一个节日的日期ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.禁用系统休眠ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_PauseSleep = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_DisplaySleep = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_SystemSleep = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_PowerOn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "关机倒计时 (0-100)";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "小爱UDP关机";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关机选项ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.下一个节日的日期ToolStripMenuItem,
            this.toolStripSeparator2,
            this.禁用系统休眠ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.设置ToolStripMenuItem,
            this.ToolStripMenuItem_PowerOn,
            this.toolStripSeparator1,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 182);
            // 
            // 关机选项ToolStripMenuItem
            // 
            this.关机选项ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_LockWorkStation,
            this.ToolStripMenuItem_sleep,
            this.ToolStripMenuItem_dormancy,
            this.ToolStripMenuItem_shutdown});
            this.关机选项ToolStripMenuItem.Name = "关机选项ToolStripMenuItem";
            this.关机选项ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.关机选项ToolStripMenuItem.Text = "关机选项";
            // 
            // ToolStripMenuItem_LockWorkStation
            // 
            this.ToolStripMenuItem_LockWorkStation.Name = "ToolStripMenuItem_LockWorkStation";
            this.ToolStripMenuItem_LockWorkStation.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItem_LockWorkStation.Text = "锁定";
            this.ToolStripMenuItem_LockWorkStation.Click += new System.EventHandler(this.ToolStripMenuItem_LockWorkStation_Click);
            // 
            // ToolStripMenuItem_sleep
            // 
            this.ToolStripMenuItem_sleep.Name = "ToolStripMenuItem_sleep";
            this.ToolStripMenuItem_sleep.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItem_sleep.Text = "睡眠";
            this.ToolStripMenuItem_sleep.Click += new System.EventHandler(this.ToolStripMenuItem_sleep_Click);
            // 
            // ToolStripMenuItem_dormancy
            // 
            this.ToolStripMenuItem_dormancy.Name = "ToolStripMenuItem_dormancy";
            this.ToolStripMenuItem_dormancy.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItem_dormancy.Text = "休眠";
            this.ToolStripMenuItem_dormancy.Click += new System.EventHandler(this.ToolStripMenuItem_dormancy_Click);
            // 
            // ToolStripMenuItem_shutdown
            // 
            this.ToolStripMenuItem_shutdown.Name = "ToolStripMenuItem_shutdown";
            this.ToolStripMenuItem_shutdown.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItem_shutdown.Text = "关机";
            this.ToolStripMenuItem_shutdown.Click += new System.EventHandler(this.ToolStripMenuItem_shutdown_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // 下一个节日的日期ToolStripMenuItem
            // 
            this.下一个节日的日期ToolStripMenuItem.Name = "下一个节日的日期ToolStripMenuItem";
            this.下一个节日的日期ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.下一个节日的日期ToolStripMenuItem.Text = "下一个节日的日期";
            this.下一个节日的日期ToolStripMenuItem.Click += new System.EventHandler(this.下一个节日的日期ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // 禁用系统休眠ToolStripMenuItem
            // 
            this.禁用系统休眠ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_PauseSleep,
            this.ToolStripMenuItem_DisplaySleep,
            this.ToolStripMenuItem_SystemSleep});
            this.禁用系统休眠ToolStripMenuItem.Name = "禁用系统休眠ToolStripMenuItem";
            this.禁用系统休眠ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.禁用系统休眠ToolStripMenuItem.Text = "禁用系统休眠";
            // 
            // ToolStripMenuItem_PauseSleep
            // 
            this.ToolStripMenuItem_PauseSleep.Name = "ToolStripMenuItem_PauseSleep";
            this.ToolStripMenuItem_PauseSleep.Size = new System.Drawing.Size(172, 22);
            this.ToolStripMenuItem_PauseSleep.Text = "不启用";
            this.ToolStripMenuItem_PauseSleep.Click += new System.EventHandler(this.ToolStripMenuItem_SystemSleep_Click);
            // 
            // ToolStripMenuItem_DisplaySleep
            // 
            this.ToolStripMenuItem_DisplaySleep.Name = "ToolStripMenuItem_DisplaySleep";
            this.ToolStripMenuItem_DisplaySleep.Size = new System.Drawing.Size(172, 22);
            this.ToolStripMenuItem_DisplaySleep.Text = "禁用屏幕自动休眠";
            // 
            // ToolStripMenuItem_SystemSleep
            // 
            this.ToolStripMenuItem_SystemSleep.Name = "ToolStripMenuItem_SystemSleep";
            this.ToolStripMenuItem_SystemSleep.Size = new System.Drawing.Size(172, 22);
            this.ToolStripMenuItem_SystemSleep.Text = "禁用系统自动休眠";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.设置ToolStripMenuItem.Text = "设置";
            this.设置ToolStripMenuItem.Click += new System.EventHandler(this.设置ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem_PowerOn
            // 
            this.ToolStripMenuItem_PowerOn.Name = "ToolStripMenuItem_PowerOn";
            this.ToolStripMenuItem_PowerOn.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItem_PowerOn.Text = "开机启动";
            this.ToolStripMenuItem_PowerOn.Click += new System.EventHandler(this.ToolStripMenuItem_PowerOn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 216);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "默认设置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(143, 216);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "保存设置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(265, 216);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "关于";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(194, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 13;
            this.textBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "监听端口号 (0-65535)";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(194, 63);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 15;
            this.textBox2.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox2_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "监听关机指令";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(194, 100);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "关机动作";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "关机",
            "休眠",
            "睡眠",
            "锁定"});
            this.comboBox1.Location = new System.Drawing.Point(194, 139);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 20);
            this.comboBox1.TabIndex = 19;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(53, 176);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 21;
            this.checkBox1.Text = "开机启动";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(194, 172);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 23);
            this.button4.TabIndex = 22;
            this.button4.Text = "注册为服务";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(361, 262);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0D;
            this.Text = "udp_turn_off";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_PowerOn;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button4;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 下一个节日的日期ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 禁用系统休眠ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_PauseSleep;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_DisplaySleep;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SystemSleep;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 关机选项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_LockWorkStation;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_sleep;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_dormancy;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_shutdown;
    }
}

