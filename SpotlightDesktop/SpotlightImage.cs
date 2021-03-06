﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpotlightDesktop
{
    public sealed class SpotlightImage
    {
        private static int MAX_WIDTH = 1920;
        private static int MAX_HEIGHT = 1080;
        private static int MIN_SIZE = 500;

        public static string GetFolderPath()
        {
            string userName = Environment.UserName;

            string contentDeliveryPath = String.Format(@"C:\Users\{0}\AppData\Local\Packages",
                userName);

            string[] contentDeliveryFolderPath = Directory.GetDirectories(contentDeliveryPath)
                .Where(x => x.Contains("Microsoft.Windows.ContentDeliveryManager")).ToArray();
            
            return String.Format(@"{0}\LocalState\Assets", 
                contentDeliveryFolderPath[0]);
        }

        public static BitmapImage[] GetImages()
        {
            string folderPath = GetFolderPath();

            return Directory.GetFiles(folderPath)
              .Select(x =>
              {
                  FileInfo info = new FileInfo(x);

                  return new
                  {
                      Path = x,
                      info.Length,
                      info.CreationTime
                  };
              })
              .Where(x => x.Length > MIN_SIZE)
              .OrderByDescending(x => x.CreationTime)
              .Select(x =>
              {
                  try {
                      return new BitmapImage(new Uri(x.Path));
                  } catch (Exception) {
                      return null;
                  }
              })
              .Where(x =>
              {
                  return (x != null
                      && x.Width >= MAX_WIDTH
                      && x.Height >= MAX_HEIGHT);
              }).ToArray();
        }
    }
}
