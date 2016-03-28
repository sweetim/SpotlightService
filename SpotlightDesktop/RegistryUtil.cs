using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpotlightDesktop
{
    public static class RegistryUtil
    {
        public static void StartUpRegister()
        {
            string name = Assembly.GetExecutingAssembly().GetName().Name;
            string executablePath = Environment.CurrentDirectory + @"\" + AppDomain.CurrentDomain.FriendlyName;

            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (registryKey.GetValue(name) == null) {
                registryKey.SetValue(name, executablePath);
            }
        }
        
    }
}
