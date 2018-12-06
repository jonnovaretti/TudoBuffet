using ImageMagick;
using System.IO;

namespace TudoBuffet.Website.Tools
{
    public class ImageManipulator
    {
        private Stream file;
        private MagickImage image;

        public int Height { get { return image.Height; } }
        public int Width { get { return image.Width; } }

        private ImageManipulator(Stream file)
        {
            this.file = file;
        }

        public static ImageManipulator CreateImageManipulation(Stream file)
        {
            return new ImageManipulator(file);
        }

        public MemoryStream Resize(int width, int height)
        {
            MemoryStream fileResized;

            using (image)
            {
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