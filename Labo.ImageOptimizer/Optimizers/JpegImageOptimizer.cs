// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JpegImageOptimizer.cs" company="Labo">
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
//   Defines the JpegImageOptimizer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.ImageOptimizer.Optimizers
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;

    using Labo.ImageOptimizer.Configuration;

    /// <summary>
    /// The jpeg image optimizer class.
    /// </summary>
    public sealed class JpegImageOptimizer : BaseImageOptimizer
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IImageOptimizationConfiguration m_Configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="JpegImageOptimizer"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public JpegImageOptimizer(IImageOptimizationConfiguration configuration)
        {
            m_Configuration = configuration;
        }

        /// <summary>
        /// Optimizes the specified input image and writes the output to the output image path.
        /// </summary>
        /// <param name="inputImagePath">The input image path.</param>
        /// <param name="outputImagePath">The output image path.</param>
        public override void Optimize(string inputImagePath, string outputImagePath)
        {
            string jpegTranPath = string.IsNullOrWhiteSpace(m_Configuration.JpegTranApplicationPath) ? Path.Combine(Environment.CurrentDirectory, "Tools", "jpegtran.exe") : m_Configuration.JpegTranApplicationPath;

            using (Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo(jpegTranPath, string.Format(CultureInfo.InvariantCulture, "-optimize -progressive -copy none \"{0}\" \"{1}\"", inputImagePath, outputImagePath))
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                process.Start();
                process.WaitForExit();
            }
        }
    }
}
