using MemeMakerWPF.Models.API;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemeMakerWPF.Utility.Extension
{
    public static class BitmapOperations
    {
        /// <summary>
        /// Conversion from RenderTargetBitmap to Bitmap type
        /// </summary>
        /// <param name="renderTarget">RenderTargetBitmap</param>
        /// <returns>Bitmap</returns>
        public static Bitmap ToBitmap(this RenderTargetBitmap renderTarget)
        {
            MemoryStream stream = new MemoryStream();
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTarget));
            encoder.Save(stream);

            return new Bitmap(stream);
        }

        public static Bitmap ToBitmap(this BitmapImage bitmapImage)
        {
            MemoryStream stream = new MemoryStream();
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            encoder.Save(stream);

            return new Bitmap(stream);
        }

        public static BitmapImage GetBitmap(this TemplateRawData template)
        {
            ImageFormat format = ImageFormat.Png;
            if(Path.GetExtension(template.Path).ToLower() != ".png")
                format = ImageFormat.Jpeg;

            ImageConverter imageConverter = new ImageConverter();
            Bitmap bm = (Bitmap)imageConverter.ConvertFrom(template.ImageData);

            if (bm != null && (bm.HorizontalResolution != (int)bm.HorizontalResolution ||
                               bm.VerticalResolution != (int)bm.VerticalResolution))
            {
                bm.SetResolution((int)(bm.HorizontalResolution + 0.5f),
                                 (int)(bm.VerticalResolution + 0.5f));
            }

            return bm.ToBitmapImage(format);
        }

        public static BitmapImage GetBitmap(this MemeRawData template)
        {
            ImageFormat format = ImageFormat.Png;
            if (Path.GetExtension(template.Path).ToLower() != ".png")
                format = ImageFormat.Jpeg;

            ImageConverter imageConverter = new ImageConverter();
            Bitmap bm = (Bitmap)imageConverter.ConvertFrom(template.ImageData);

            if (bm != null && (bm.HorizontalResolution != (int)bm.HorizontalResolution ||
                               bm.VerticalResolution != (int)bm.VerticalResolution))
            {
                bm.SetResolution((int)(bm.HorizontalResolution + 0.5f),
                                 (int)(bm.VerticalResolution + 0.5f));
            }

            return bm.ToBitmapImage(format);
        }

        public static BitmapImage ToBitmapImage(this Bitmap bitmap, ImageFormat imageFormat)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, imageFormat);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        /// <summary>
        /// Crop Bitmap by removing transparent part of image
        /// </summary>
        /// <param name="oryginalBitmap">RenderTargetBitmap</param>
        /// <returns>Bitmap cropped to remove transparent parts</returns>
        public static Bitmap CropTransparent(RenderTargetBitmap oryginalBitmap)
        {
            Bitmap bitmap = oryginalBitmap.ToBitmap();

            System.Drawing.Point min = new System.Drawing.Point(int.MaxValue, int.MaxValue);
            System.Drawing.Point max = new System.Drawing.Point(int.MinValue, int.MinValue);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    System.Drawing.Color pixelColor = bitmap.GetPixel(x, y);
                    if (pixelColor.A > 0)
                    {
                        if (x < min.X) min.X = x;
                        if (y < min.Y) min.Y = y;

                        if (x > max.X) max.X = x;
                        if (y > max.Y) max.Y = y;
                    }
                }
            }

            Rectangle cropRectangle = new Rectangle(min.X, min.Y, max.X - min.X, max.Y - min.Y + 1);
            Bitmap newBitmap = new Bitmap(cropRectangle.Width, cropRectangle.Height);
            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                g.DrawImage(bitmap, 0, 0, cropRectangle, GraphicsUnit.Pixel);
            }

            return newBitmap;
        }

        /// <summary>
        /// Gets Bitmap and saves it as PNG in given location
        /// </summary>
        /// <param name="bitmap">TransformedBitmap from GetBitmapFromCanvas() method</param>
        public static string SavePng(Bitmap bitmap, string fileName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.Filter = "Image files (*.png)|*.png";
            saveFileDialog.FileName = fileName;

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var file = File.OpenWrite(saveFileDialog.FileName))
                    bitmap.Save(file, ImageFormat.Png);

                return saveFileDialog.FileName;
            }

            return null;
        }

        /// <summary>
        /// Gets TransformedBitmap and saves it as PNG in given location
        /// </summary>
        /// <param name="bitmap">RenderTargetBitmap from GetBitmapFromCanvas() method</param>
        public static void SavePng(RenderTargetBitmap bitmap, string fileName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.FileName = fileName;


            if (saveFileDialog.ShowDialog() == true)
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                using (var file = File.OpenWrite(saveFileDialog.FileName))
                    encoder.Save(file);
            }
        }

        /// <summary>
        /// Convert canvas into image by cropping and transforming content size
        /// </summary>
        /// <param name="canvas">Main canvas of application</param>
        /// <returns>TrandgotmedBitmap that can be used in SavePng method</returns>
        public static Bitmap GetBitmapFromCanvas(Canvas canvas)
        {
            if (canvas == null)
                return null;

            var rect = new Rect(canvas.RenderSize);
            var visual = new DrawingVisual();
            using (var dc = visual.RenderOpen())
            {
                dc.DrawRectangle(new VisualBrush(canvas), null, rect);
            }

            RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)rect.Width, (int)rect.Height, 96d, 96d, PixelFormats.Default);
            renderBitmap.Render(visual);

            //cropp image to remove right and bottom margins
            var crop = CropTransparent(renderBitmap);

            return crop;
        }

        /// <summary>
        /// Tries to scale image to given size. Fails if desired size is lesser than current size. Keeps aspect ratio, so output one of given dimensions might differ.
        /// </summary>
        /// <param name="bitmap">Bitmap to scale</param>
        /// <param name="desiredWidth">Width od scaled image</param>
        /// <param name="desiredHeight">Height of scaled image</param>
        /// <returns>Scaled or unchanged image</returns>
        public static Bitmap TryScaleUpImage(Bitmap bitmap, double desiredWidth, double desiredHeight)
        {
            double ratioX = desiredWidth / bitmap.Width;
            double ratioY = desiredHeight / bitmap.Height;

            if (ratioX < 1 || ratioY < 1)
                return bitmap;

            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(bitmap.Width * ratio);
            var newHeight = (int)(bitmap.Height * ratio);

            Bitmap result = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.DrawImage(bitmap, 0, 0, newWidth, newHeight);
            }

            //var newImage = new Bitmap(bitmap, newWidth, newHeight);

            return result;
        }
    }
}
