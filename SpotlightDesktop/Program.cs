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
            RegistryUtil.StartUpRegister();

            var filePath = SpotlightImage.GetImages();

            var random = new Random();
            var imageIndex = random.Next(filePath.Length);
            var mirrorImage = ImageUtil.MirrorImage(filePath[imageIndex]);

            string imagePath = String.Format(@"{0}\{1}.jpeg",
                GetLocalApplicationFolderPath(),
                Assembly.GetExecutingAssembly().GetName().Name);

            StoreImage(imagePath, mirrorImage);
            
            DesktopWallpaper.Set(imagePath, DesktopWallpaper.Style.Tiled);
        }

        private static string GetLocalApplicationFolderPath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string company = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(
                    Assembly.GetExecutingAssembly(),
                    typeof(AssemblyCompanyAttribute),
                    false)
                ).Company;

            string appName = Assembly.GetExecutingAssembly().GetName().Name;

            return String.Format(@"{0}\{1}\{2}",
                folderPath,
                company,
                appName);
        }

        private static void StoreImage(string path, RenderTargetBitmap image)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate)) {
                encoder.Save(fs);
            }
        }
    }
}
