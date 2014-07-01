// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PngImageOptimizer.cs" company="Labo">
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
//   Defines the PngImageOptimizer type.
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
    /// Png image optimizer class.
    /// </summary>
    public sealed class PngImageOptimizer : BaseImageOptimizer
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IImageOptimizationConfiguration m_Configuration;

        /// <summary>
        /// The image optimization speed
        /// </summary>
        private readonly PngImageOptimizationSpeed m_OptimizationSpeed;

        /// <summary>
        /// Initializes a new instance of the <see cref="PngImageOptimizer"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="optimizationSpeed">The optimization speed.</param>
        public PngImageOptimizer(IImageOptimizationConfiguration configuration, PngImageOptimizationSpeed optimizationSpeed = PngImageOptimizationSpeed.Slow)
        {
            m_Configuration = configuration;
            m_OptimizationSpeed = optimizationSpeed;
        }

        /// <summary>
        /// Optimizes the specified input image and writes the output to the output image path.
        /// </summary>
        /// <param name="inputImagePath">The input image path.</param>
        /// <param name="outputImagePath">The output image path.</param>
        public override void Optimize(string inputImagePath, string outputImagePath)
        {
            RunProcess(string.IsNullOrWhiteSpace(m_Configuration.PngOutApplicationPath) ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools", "pngout.exe") : m_Configuration.PngOutApplicationPath, string.Format(CultureInfo.InvariantCulture, "\"{0}\" \"{1}\"", inputImagePath, outputImagePath));
            RunProcess(string.IsNullOrWhiteSpace(m_Configuration.OptiPngApplicationPath) ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools", "optipng.exe") : m_Configuration.OptiPngApplicationPath, string.Format(CultureInfo.InvariantCulture, "-force {1} \"{0}\"", outputImagePath, GetOptimizationSpeedArgument()));
        }

        /// <summary>
        /// Gets the new generated temporary image path.
        /// </summary>
        /// <returns>The temporary image path</returns>
        protected override string GetNewTempImagePath()
        {
            return Path.Combine(Path.GetTempPath(), string.Format(CultureInfo.InvariantCulture, "{0}.png", Path.GetTempFileName()));
        }

        /// <summary>
        /// Runs the process.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="arguments">The arguments.</param>
        private static void RunProcess(string fileName, string arguments)
        {
            using (Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo(fileName, arguments) { CreateNoWindow = true, UseShellExecute = false };
                process.Start();
                process.WaitForExit();
            }
        }

        /// <summary>
        /// Gets the optimization speed argument of the 'optipng.exe' program.
        /// </summary>
        /// <returns>The optimization speed argument.</returns>
        private string GetOptimizationSpeedArgument()
        {
            switch (m_OptimizationSpeed)
            {
                 case PngImageOptimizationSpeed.Slow:
                    return "-o7";
                 case PngImageOptimizationSpeed.Medium:
                    return "-o5";
                 case PngImageOptimizationSpeed.Fast:
                    return string.Empty;
                default:
                    return "-o7";
            }
        }
    }
}