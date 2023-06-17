using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PipettingCode
{

    #region ini文件的读取和写入
    public class IniUtil
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, [In][Out] char[] lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileSection(string lpAppName, string lpString, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        public static string[] INIGetAllSectionNames(string iniFile)
        {
            uint num = 32767u;
            string[] result = new string[0];
            IntPtr intPtr = Marshal.AllocCoTaskMem((int)(num * 2));
            uint privateProfileSectionNames = GetPrivateProfileSectionNames(intPtr, num, iniFile);
            if (privateProfileSectionNames != 0)
            {
                string text = Marshal.PtrToStringAuto(intPtr, (int)privateProfileSectionNames).ToString();
                result = text.Split(new char[1], StringSplitOptions.RemoveEmptyEntries);
            }

            Marshal.FreeCoTaskMem(intPtr);
            return result;
        }

        public static string[] INIGetAllItems(string iniFile, string section)
        {
            uint num = 32767u;
            string[] result = new string[0];
            IntPtr intPtr = Marshal.AllocCoTaskMem((int)(num * 2));
            uint privateProfileSection = GetPrivateProfileSection(section, intPtr, num, iniFile);
            if (privateProfileSection != num - 2 || privateProfileSection == 0)
            {
                string text = Marshal.PtrToStringAuto(intPtr, (int)privateProfileSection);
                result = text.Split(new char[1], StringSplitOptions.RemoveEmptyEntries);
            }

            Marshal.FreeCoTaskMem(intPtr);
            return result;
        }

        public static string[] INIGetAllItemKeys(string iniFile, string section)
        {
            string[] result = new string[0];
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            char[] array = new char[10240];
            if (GetPrivateProfileString(section, null, null, array, 10240u, iniFile) != 0)
            {
                result = new string(array).Split(new char[1], StringSplitOptions.RemoveEmptyEntries);
            }

            array = null;
            return result;
        }

        public static string INIGetStringValue(string iniFile, string section, string key, string defaultValue)
        {
            string result = defaultValue;
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("必须指定键名称(key)", "key");
            }

            StringBuilder stringBuilder = new StringBuilder(10240);
            if (GetPrivateProfileString(section, key, defaultValue, stringBuilder, 10240u, iniFile) != 0)
            {
                result = stringBuilder.ToString();
            }

            stringBuilder = null;
            return result;
        }

        public static bool INIWriteItems(string iniFile, string section, string items)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            if (string.IsNullOrEmpty(items))
            {
                throw new ArgumentException("必须指定键值对", "items");
            }

            return WritePrivateProfileSection(section, items, iniFile);
        }

        public static bool INIWriteValue(string iniFile, string section, string key, string value)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("必须指定键名称", "key");
            }

            if (value == null)
            {
                throw new ArgumentException("值不能为null", "value");
            }

            return WritePrivateProfileString(section, key, value, iniFile);
        }

        public static bool INIDeleteKey(string iniFile, string section, string key)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("必须指定键名称", "key");
            }

            return WritePrivateProfileString(section, key, null, iniFile);
        }

        public static bool INIDeleteSection(string iniFile, string section)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            return WritePrivateProfileString(section, null, null, iniFile);
        }

        public static bool INIEmptySection(string iniFile, string section)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            return WritePrivateProfileSection(section, string.Empty, iniFile);
        }
    }
    #endregion
}
