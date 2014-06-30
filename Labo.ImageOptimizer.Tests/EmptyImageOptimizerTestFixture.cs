namespace Labo.ImageOptimizer.Tests
{
    using System;
    using System.IO;
    using System.Linq;

    using Labo.ImageOptimizer.Optimizers;

    using NUnit.Framework;

    [TestFixture]
    public class EmptyImageOptimizerTestFixture
    {
        [Test]
        public void Optimize()
        {
            string imagePath = Path.Combine(Environment.CurrentDirectory, "Images", "fig4.gif");
            string opimizedImagePath = Path.Combine(Environment.CurrentDirectory, "Images", string.Format("{0}-optimized{1}", Path.GetFileNameWithoutExtension(imagePath), Path.GetExtension(imagePath)));

            EmptyImageOptimizer pngOptimizer = new EmptyImageOptimizer();
            byte[] imageData = File.ReadAllBytes(imagePath);
            byte[] optimizedImageData = pngOptimizer.Optimize(imageData);
            File.WriteAllBytes(opimizedImagePath, optimizedImageData);

            long optimizedImageLength = new FileInfo(opimizedImagePath).Length;

            Assert.IsTrue(optimizedImageLength > 0);
            Assert.IsTrue(optimizedImageData.SequenceEqual(imageData));
            Assert.IsTrue(optimizedImageLength == new FileInfo(imagePath).Length);
        }

        [Test]
        public void OptimizeByFilePath()
        {
            string imagePath = Path.Combine(Environment.CurrentDirectory, "Images", "fig4.gif");
            string opimizedImagePath = Path.Combine(Environment.CurrentDirectory, "Images", string.Format("{0}-optimized{1}", Path.GetFileNameWithoutExtension(imagePath), Path.GetExtension(imagePath)));

            EmptyImageOptimizer pngOptimizer = new EmptyImageOptimizer();
            pngOptimizer.Optimize(imagePath, opimizedImagePath);

            byte[] imageData = File.ReadAllBytes(imagePath);            
            byte[] optimizedImageData = File.ReadAllBytes(opimizedImagePath);

            long optimizedImageLength = new FileInfo(opimizedImagePath).Length;

            Assert.IsTrue(optimizedImageLength > 0);
            Assert.IsTrue(optimizedImageData.SequenceEqual(imageData));
            Assert.IsTrue(optimizedImageLength == new FileInfo(imagePath).Length);
        }
    }
}