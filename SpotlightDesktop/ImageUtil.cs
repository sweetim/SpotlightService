using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpotlightDesktop
{
    public static class ImageUtil
    {
        public static RenderTargetBitmap MirrorImage(BitmapImage image)
        {
            var flipImage = new TransformedBitmap(image, new ScaleTransform(-1, 1, 0, 0));

            BitmapFrame frame1 = BitmapFrame.Create(image);
            BitmapFrame frame2 = BitmapFrame.Create(flipImage);

            int imageWidth = frame1.PixelWidth;
            int imageHeight = frame1.PixelHeight;

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen()) {
                drawingContext.DrawImage(frame1, new Rect(0, 0, imageWidth, imageHeight));
                drawingContext.DrawImage(frame2, new Rect(imageWidth, 0, imageWidth, imageHeight));

            }

            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(imageWidth * 2, 
                imageHeight, 
                96, 
                96, 
                PixelFormats.Pbgra32);

            renderBitmap.Render(drawingVisual);

            return renderBitmap; 
        }
    }
}
