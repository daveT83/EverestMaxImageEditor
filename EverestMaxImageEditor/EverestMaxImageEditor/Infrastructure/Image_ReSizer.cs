using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace EverestMaxImageEditor.Infrastructure
{
    public class Image_ReSizer
    {
        public string ImportPath;
        public string ExportPath;
        public string ExportFileName;
        public bool IsMediaButton;
        private int guiHeightWidth;
        private int guiMediaHeightWidth;
        private int keyboardHeightWidth;
        private int keyboardMediaHeightWidth;

        public Image_ReSizer()
        {
            this.guiHeightWidth = 26;
            this.guiMediaHeightWidth = 75;
            this.keyboardHeightWidth = 245;
            this.keyboardMediaHeightWidth = 245;
        }

        /// <summary>
        /// Save the keyboard image
        /// </summary>
        public void SaveKeyboardImage()
        {
            if (ImportPath != null && !ImportPath.Equals(""))
            {
                Image image = Resize(Image.FromFile(ImportPath), keyboardHeightWidth, keyboardHeightWidth);
                string extension = GetExtension(ImportPath);
                this.ExportFileName = GetFileName();
                this.ExportFileName = this.ExportFileName + "_ProgrammableButton." + GetExtension(ImportPath);

                if (extension.ToLower().Equals("png"))
                {
                    image.Save(Path.Combine(ExportPath, ExportFileName), ImageFormat.Png);
                }
                else if (extension.ToLower().Equals("jpg"))
                {
                    image.Save(Path.Combine(ExportPath, ExportFileName), ImageFormat.Jpeg);
                }
                else if (extension.ToLower().Equals("bmp"))
                {
                    image.Save(Path.Combine(ExportPath, ExportFileName), ImageFormat.Bmp);
                }
            }
        }

        /// <summary>
        /// Save the keyboard media image
        /// </summary>
        public void SaveKeyboardMediaImage()
        {
            if (ImportPath != null && !ImportPath.Equals(""))
            {
                Image image = Resize(Image.FromFile(ImportPath), keyboardMediaHeightWidth, keyboardMediaHeightWidth);
                string extension = GetExtension(ImportPath);
                this.ExportFileName = GetFileName();
                this.ExportFileName = this.ExportFileName + "_MediaDisplay." + GetExtension(ImportPath);

                if (extension.ToLower().Equals("png"))
                {
                    image.Save(Path.Combine(ExportPath, ExportFileName), ImageFormat.Png);
                }
                else if (extension.ToLower().Equals("jpg"))
                {
                    image.Save(Path.Combine(ExportPath, ExportFileName), ImageFormat.Jpeg);
                }
                else if (extension.ToLower().Equals("bmp"))
                {
                    image.Save(Path.Combine(ExportPath, ExportFileName), ImageFormat.Bmp);
                }
            }
        }

        /// <summary>
        /// Resizes the image
        /// </summary>
        /// <returns></returns>
        private Image Resize(Image image, int height, int width)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        /// <summary>
        /// gets the file extension
        /// </summary>
        /// <returns></returns>
        private string GetExtension(string str)
        {
            return str.Split('.')[1];
        }

        /// <summary>
        /// Gets the file name from the file
        /// </summary>
        /// <returns></returns>
        private string GetFileName()
        {
            if (ImportPath.Split('\\').Length > 1)
            {
                return ImportPath.Split('\\')[ImportPath.Split('\\').Length - 1].Split('.')[0];
            }
            else
            {
                return ImportPath.Split('/')[ImportPath.Split('/').Length - 1].Split('.')[0];
            }
        }

        /// <summary>
        /// Returns the keyboard button Image
        /// </summary>
        /// <returns></returns>
        public Image GetKeyboardButtons()
        {
            return Resize(Image.FromFile(ImportPath), guiHeightWidth, guiHeightWidth);
        }

        /// <summary>
        /// Returns the Media Button Image
        /// </summary>
        /// <returns></returns>
        public Image GetMediaButton()
        {
            return Resize(Image.FromFile(ImportPath), guiMediaHeightWidth, guiMediaHeightWidth);
        }
    }
}
