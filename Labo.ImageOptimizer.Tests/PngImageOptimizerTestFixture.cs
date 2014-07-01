namespace Labo.ImageOptimizer.Tests
{
    using System;
    using System.IO;

    using Labo.ImageOptimizer.Configuration;
    using Labo.ImageOptimizer.Optimizers;

    using NUnit.Framework;

    [TestFixture]
    public class PngImageOptimizerTestFixture
    {
        [Test, Sequential]
        public void Optimize(
            [Values("450px-Almond_flower.png", "450px-Almond_flowers.png",
                "800px-Red_Flower.png", "Taif_Flowers.PNG")]
                string imageName,
            [Values(PngImageOptimizationSpeed.Fast, PngImageOptimizationSpeed.Medium, PngImageOptimizationSpeed.Slow)]
            PngImageOptimizationSpeed optimizationSpeed)
        {
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", imageName);
            string opimizedImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", string.Format("{0}-optimized{1}", Path.GetFileNameWithoutExtension(imagePath), Path.GetExtension(imagePath)));

            PngImageOptimizer pngOptimizer = new PngImageOptimizer(LaboImageOptimizationConfig.Instance, optimizationSpeed);
            byte[] optimizedImageData = pngOptimizer.Optimize(File.ReadAllBytes(imagePath));
            File.WriteAllBytes(opimizedImagePath, optimizedImageData);

            long optimizedImageLength = new FileInfo(opimizedImagePath).Length;

            Assert.IsTrue(optimizedImageLength > 0);
            Assert.IsTrue(optimizedImageLength < new FileInfo(imagePath).Length);
        }
    }
}