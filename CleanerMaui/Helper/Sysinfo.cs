using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CleanerMaui.Helper
{
    internal class Sysinfo
    {

        public string GetWinVer()
        {
            try
            {
                return Environment.OSVersion.ToString();

            }
            catch {
                return "Windows";
            
            }

            
        }

        public string GetHardWareInfos() {
            StringBuilder sb = new StringBuilder();

            //CPU
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);
            if (processor_name != null)
            {
                sb.AppendLine($"{processor_name.GetValue("processorNameString")}");
            }
            return sb.ToString();
        }
    }
}
