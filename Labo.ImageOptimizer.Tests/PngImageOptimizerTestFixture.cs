namespace Labo.ImageOptimizer.Tests
{
    using System;
    using System.IO;

    using Labo.ImageOptimizer.Optimizers;

    using NUnit.Framework;

    [TestFixture]
    public class PngImageOptimizerTestFixture
    {
        [Test, Sequential]
        public void Optimize(
            [Values("KarimNafatni10.png", "KarimNafatni11.png", "KarimNafatni3.png",
                "KarimNafatni5.png", "KarimNafatni7.png", "KarimNafatni8.png")]
                string imageName,
            [Values(PngImageOptimizationSpeed.Fast, PngImageOptimizationSpeed.Medium, PngImageOptimizationSpeed.Slow)]
            PngImageOptimizationSpeed optimizationSpeed)
        {
            string imagePath = Path.Combine(Environment.CurrentDirectory, "Images", imageName);
            string opimizedImagePath = Path.Combine(Environment.CurrentDirectory, "Images", string.Format("{0}-optimized{1}", Path.GetFileNameWithoutExtension(imagePath), Path.GetExtension(imagePath)));

            PngImageOptimizer pngOptimizer = new PngImageOptimizer(optimizationSpeed);
            byte[] optimizedImageData = pngOptimizer.Optimize(File.ReadAllBytes(imagePath));
            File.WriteAllBytes(opimizedImagePath, optimizedImageData);

            long optimizedImageLength = new FileInfo(opimizedImagePath).Length;

            Assert.IsTrue(optimizedImageLength > 0);
            Assert.IsTrue(optimizedImageLength < new FileInfo(imagePath).Length);
        }
    }
}