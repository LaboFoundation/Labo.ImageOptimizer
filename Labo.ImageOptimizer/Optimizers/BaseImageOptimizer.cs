// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseImageOptimizer.cs" company="Labo">
//   The MIT License (MIT)
//   
//   Copyright (c) 2014 Bora Akgun
//   
//   Permission is hereby granted, free of charge, to any person obtaining a copy of
//   this software and associated documentation files (the "Software"), to deal in
//   the Software without restriction, including without limitation the rights to
//   use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//   the Software, and to permit persons to whom the Software is furnished to do so,
//   subject to the following conditions:
//   
//   The above copyright notice and this permission notice shall be included in all
//   copies or substantial portions of the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
//   FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
//   COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//   IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//   CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary>
//   Defines the BaseImageOptimizer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.ImageOptimizer.Optimizers
{
    using System.IO;

    /// <summary>
    /// Base image optimizer class.
    /// </summary>
    public abstract class BaseImageOptimizer : IImageOptimizer
    {
        /// <summary>
        /// Optimizes the image using the specified image data.
        /// </summary>
        /// <param name="imageData">The image data.</param>
        /// <returns>Optimized image data.</returns>
        public byte[] Optimize(byte[] imageData)
        {
            string imagePath = GetNewTempImagePath();
            File.WriteAllBytes(imagePath, imageData);

            string outputImagePath = GetNewTempImagePath();

            Optimize(imagePath, outputImagePath);

            return File.ReadAllBytes(outputImagePath);
        }

        /// <summary>
        /// Optimizes the specified input image and writes the output to the output image path.
        /// </summary>
        /// <param name="inputImagePath">The input image path.</param>
        /// <param name="outputImagePath">The output image path.</param>
        public abstract void Optimize(string inputImagePath, string outputImagePath);

        /// <summary>
        /// Gets the new generated temporary image path.
        /// </summary>
        /// <returns>The temporary image path</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        protected virtual string GetNewTempImagePath()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
        }
    }
}