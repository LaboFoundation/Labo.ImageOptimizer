namespace Labo.ImageOptimizer.Tests
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using Labo.ImageOptimizer.Optimizers;

    using NUnit.Framework;

    [TestFixture]
    public class JpegImageOptimizerTestFixture
    {
        [Test, Sequential]
        public void Optimize(
            [Values("freeimage-1634087-web.jpg", "freeimage-2197584-web.jpg",
                    "freeimage-2387463-web.jpg", "freeimage-2979180-web.jpg")]
            string imageName)
        {
            string imagePath = Path.Combine(Environment.CurrentDirectory, "Images", imageName);
            string opimizedImagePath = Path.Combine(Environment.CurrentDirectory, "Images", string.Format("{0}-optimized{1}", Path.GetFileNameWithoutExtension(imagePath), Path.GetExtension(imagePath)));
            
            JpegImageOptimizer jpegOptimizer = new JpegImageOptimizer();
            byte[] optimizedImageData = jpegOptimizer.Optimize(File.ReadAllBytes(imagePath));
            File.WriteAllBytes(opimizedImagePath, optimizedImageData);

            long optimizedImageLength = new FileInfo(opimizedImagePath).Length;
            
            Assert.IsTrue(optimizedImageLength > 0);
            Assert.IsTrue(optimizedImageLength < new FileInfo(imagePath).Length);
        }
    }
}
