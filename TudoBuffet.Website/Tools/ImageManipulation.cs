﻿using ImageMagick;
using System;
using System.IO;

namespace TudoBuffet.Website.Tools
{
    public class ImageManipulation
    {
        public MemoryStream Resize(Stream originalFile, int width, int height)
        {
            MemoryStream fileResized;

            using (MagickImage image = new MagickImage(originalFile))
            {
                if (image.Width > image.Height)
                {
                    height = Convert.ToInt32(image.Height * width / (double)image.Width);
                }
                else
                {
                    width = Convert.ToInt32(image.Width * height / (double)image.Height);
                }

                image.Resize(width, height);
                image.Format = MagickFormat.Jpeg;
                image.Strip();

                fileResized = new MemoryStream();
                image.Write(fileResized);
                fileResized.Position = 0;
            }

            return fileResized;
        }
    }
}
