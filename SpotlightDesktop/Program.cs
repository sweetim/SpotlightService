using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpotlightDesktop
{
    class Program
    {
        static void Main(string[] args)
        {
            string executablePath = Environment.CurrentDirectory + @"\" + AppDomain.CurrentDomain.FriendlyName;

            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (registryKey.GetValue("SpotlightDesktop") == null) {
                registryKey.SetValue("SpotlightDesktop", executablePath);
            }


            var folderPath = SpotlightFolder.GetPath();
            var filePath = SpotlightImage.GetImages(folderPath);

            var random = new Random();
            var imageIndex = random.Next(filePath.Length);

            var imagePath = filePath[imageIndex].UriSource.AbsolutePath.ToString();

            DesktopWallpaper.Set(imagePath, DesktopWallpaper.Style.Stretched);
        }
    }
}
