// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LaboImageOptimizationConfig.cs" company="Labo">
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
//   Defines the LaboImageOptimizationConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.ImageOptimizer.Configuration
{
    using System;
    using System.Configuration;
    using System.Xml;

    /// <summary>
    /// Image optimization config class 
    /// </summary>
    public sealed class LaboImageOptimizationConfig : IConfigurationSectionHandler, IImageOptimizationConfiguration
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static readonly LaboImageOptimizationConfig s_Instance = InitConfigInstance();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static LaboImageOptimizationConfig Instance
        {
            get
            {
                return s_Instance;
            }
        }

        /// <summary>
        /// Gets or sets the jpegtran application path.
        /// </summary>
        /// <value>
        /// The jpegtran application path.
        /// </value>
        public string JpegTranApplicationPath { get; set; }

        /// <summary>
        /// Gets or sets the pngout application path.
        /// </summary>
        /// <value>
        /// The pngout application path.
        /// </value>
        public string PngOutApplicationPath { get; set; }

        /// <summary>
        /// Gets or sets the optipng application path.
        /// </summary>
        /// <value>
        /// The opti optipng application path.
        /// </value>
        public string OptiPngApplicationPath { get; set; }

        /// <summary>
        /// Creates a configuration section handler.
        /// </summary>
        /// <returns>
        /// The created section handler object.
        /// </returns>
        /// <param name="parent">Parent object.</param><param name="configContext">Configuration context object.</param><param name="section">Section XML node.</param><filterpriority>2</filterpriority>
        public object Create(object parent, object configContext, XmlNode section)
        {
            if (section == null)
            {
                throw new ArgumentNullException("section");
            }

            LaboImageOptimizationConfig config = new LaboImageOptimizationConfig();
            XmlNode pngNode = section.SelectSingleNode("Png");
            if (pngNode != null && pngNode.Attributes != null)
            {
                XmlAttribute attribute = pngNode.Attributes["OptiPngApplicationPath"];
                if (attribute != null)
                {
                    config.OptiPngApplicationPath = GetApplicationPath(attribute.Value);
                }

                attribute = pngNode.Attributes["PngOutApplicationPath"];
                if (attribute != null)
                {
                    config.PngOutApplicationPath = GetApplicationPath(attribute.Value);
                }
            }

            XmlNode jpegNode = section.SelectSingleNode("Jpeg");
            if (jpegNode != null && jpegNode.Attributes != null)
            {
                XmlAttribute attribute = jpegNode.Attributes["JpegTranApplicationPath"];
                if (attribute != null)
                {
                    config.JpegTranApplicationPath = GetApplicationPath(attribute.Value);
                }
            }

            return config;
        }

        /// <summary>
        /// Gets the application path.
        /// </summary>
        /// <param name="applicationPathString">The application path string.</param>
        /// <returns>The application path.</returns>
        private static string GetApplicationPath(string applicationPathString)
        {
            return applicationPathString.Replace("%EnvironmentCurrentDirectory%", Environment.CurrentDirectory);
        }

        /// <summary>
        /// Initializes the configuration instance.
        /// </summary>
        /// <returns>Image optimization configuration instance.</returns>
        private static LaboImageOptimizationConfig InitConfigInstance()
        {
            return ConfigurationManager.GetSection("LaboImageOptimizationConfig") as LaboImageOptimizationConfig;
        }
    }
}
