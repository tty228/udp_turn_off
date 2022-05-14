using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace udp_turn_off
{
    internal class SystemSleepAPI
    {
        //定义API函数
        [DllImport("kernel32.dll")]
        static extern uint SetThreadExecutionState(ExecutionFlag flags);

        [Flags]
        enum ExecutionFlag : uint
        {
            System = 0x00000001,//禁用系统自动休眠
            Display = 0x00000002,//禁用屏幕自动休眠
            Continus = 0x80000000,//表示在下一次调用该API前，本次设置会一直生效，单独使用则会恢复睡眠策略
            //ES_AWAYMODE_REQUIRED = 0x00000040,//离开模式
            //ES_USER_PRESENT = 0x00000004,//笔记本合上盖子不关闭电源
        }

        /// <summary>
        ///阻止系统休眠，直到线程结束恢复休眠策略
        /// </summary>
        public static void SystemSleep()
        {
            SetThreadExecutionState(ExecutionFlag.System | ExecutionFlag.Continus);
        }

        /// <summary>
        ///阻止屏幕休眠，直到线程结束恢复休眠策略
        /// </summary>
        public static void DisplaySleep()
        {
            SetThreadExecutionState(ExecutionFlag.Display | ExecutionFlag.System | ExecutionFlag.Continus);
        }

        /// <summary>
        ///恢复系统休眠策略
        /// </summary>
        public static void ResotreSleep()
        {
            SetThreadExecutionState(ExecutionFlag.Continus);
        }

        /// <summary>
        ///重置系统休眠计时器
        /// </summary>
        public static void ResetSleepTimer(bool includeDisplay = false)
        {
            if (includeDisplay)
                SetThreadExecutionState(ExecutionFlag.System | ExecutionFlag.Display);
            else
                SetThreadExecutionState(ExecutionFlag.System);
        }
    }
}
