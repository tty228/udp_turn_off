using Microsoft.Win32;

namespace udp_turn_off
{
    public class Regedit
    {
        /// <summary>
        /// 写入注册表(创建项）
        /// </summary>
        /// <param name="key"> 需要创建的注册表路径 </param>
        /// <param name="branch"> 为空时写入 CurrentUser，非空时写入 LocalMachine </param>
        /// <returns></returns>
        public static void Save(string key, string branch)
        {
            if (branch == "")
            {
                RegistryKey Root_key = Registry.CurrentUser;
                RegistryKey sleep = Root_key.CreateSubKey(key);
                sleep.Close();
            }
            else
            {
                RegistryKey Root_key = Registry.LocalMachine;
                RegistryKey sleep = Root_key.CreateSubKey(key);
                sleep.Close();
            }

        }

        /// <summary>
        /// 写入注册表
        /// </summary>
        /// <param name="key"> 需要创建的注册表路径 </param>
        /// <param name="software"> 需要创建的 项 名称 </param>
        /// <param name="regedit"> 需要创建的 键值 </param>
        /// <param name="branch"> 为空时写入 CurrentUser，非空时写入 LocalMachine </param>
        /// <returns></returns>
        public static void Save(string key, string software, string regedit, string branch)
        {
            if (branch == "")
            {
                RegistryKey Root_key = Registry.CurrentUser;
                RegistryKey sleep = Root_key.CreateSubKey(key);
                sleep.SetValue(software, regedit);
                sleep.Close();
            }
            else
            {
                RegistryKey Root_key = Registry.LocalMachine;
                RegistryKey sleep = Root_key.CreateSubKey(key);
                sleep.SetValue(software, regedit);
                sleep.Close();
            }
        }

        /// <summary>
        /// 读取注册表 键值
        /// </summary>
        /// <param name="key"> 需要读取的注册表路径 </param>
        /// <param name="software"> 需要读取的 键值 名称 </param>
        /// <param name="branch"> 为空时写入 CurrentUser，非空时写入 LocalMachine </param>
        /// <returns></returns>
        public static string Read(string key, string software, string branch)//
        {
            if (branch == "")
            {
                string str = "";
                RegistryKey Root_key = Registry.CurrentUser;
                Save(key, "");//必须创建 项 或判断是否存在，不然会报错
                RegistryKey sleep = Root_key.OpenSubKey(key);
                //不能使用 Save(key, software)，会创建空值覆盖原数据，需要判断是否存在
                string[] subkeyNames;
                subkeyNames = sleep.GetValueNames();
                //取得该项下所有键值的名称的序列，并传递给预定的数组中
                foreach (string keyName in subkeyNames)
                {
                    if (keyName == software) //判断键值的名称
                    {
                        str = sleep.GetValue(software).ToString();
                    }

                }
                sleep.Close();
                return str;
            }
            else
            {
                string str = "";
                RegistryKey Root_key = Registry.LocalMachine;
                Save(key, "LocalMachine");//必须创建 项 或判断是否存在，不然会报错
                RegistryKey sleep = Root_key.OpenSubKey(key);
                //不能使用 Save(key, software)，会创建空值覆盖原数据，需要判断是否存在
                string[] subkeyNames;
                subkeyNames = sleep.GetValueNames();
                //取得该项下所有键值的名称的序列，并传递给预定的数组中
                foreach (string keyName in subkeyNames)
                {
                    if (keyName == software) //判断键值的名称
                    {
                        str = sleep.GetValue(software).ToString();
                    }

                }
                sleep.Close();
                return str;
            }
        }

        /// <summary>
        /// 删除注册表 子项 和 键值
        /// </summary>
        /// <param name="key"> 需要删除的注册表路径 </param>
        /// <param name="software"> 需要删除的 键值 名称 </param>
        /// <param name="branch"> 为空时写入CurrentUser，非空时写入 LocalMachine </param>
        /// <returns></returns>
        public static void Delete(string key, string software, string branch)
        {
            if (branch == "")
            {
                Save(key, software, "", "");//创建注册表 空值，避免删除键值出错
                RegistryKey Root_key = Registry.CurrentUser.OpenSubKey(key, true);
                Root_key.DeleteValue(software);
                Root_key.Close();
            }
            else
            {
                Save(key, software, "", "");//创建注册表 空值，避免删除键值出错
                RegistryKey Root_key = Registry.LocalMachine.OpenSubKey(key, true);
                Root_key.DeleteValue(software);
                Root_key.Close();
            }
        }
    }
}