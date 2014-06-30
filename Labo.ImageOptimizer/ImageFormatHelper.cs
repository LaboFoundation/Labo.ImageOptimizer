// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageFormatHelper.cs" company="Labo">
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
//   Defines the ImageFormatHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Labo.ImageOptimizer
{
    using ImageMagick;

    /// <summary>
    /// Image format helper class that helps to retrieve image format using the image file information.
    /// </summary>
    internal static class ImageFormatHelper
    {
        /// <summary>
        /// Gets the image format using the image data.
        /// </summary>
        /// <param name="imageData">The image data.</param>
        /// <returns>The image format.</returns>
        public static ImageFormat GetImageFormat(byte[] imageData)
        {
            using (MagickImage magickImage = new MagickImage(imageData))
            {
                switch (magickImage.Format)
                {
                    case MagickFormat.Jpeg:
                    case MagickFormat.Jpg:
                    case MagickFormat.Pjpeg:
                        return ImageFormat.Jpeg;
                    case MagickFormat.Png:
                    case MagickFormat.Png00:
                    case MagickFormat.Png24:
                    case MagickFormat.Png32:
                    case MagickFormat.Png48:
                    case MagickFormat.Png64:
                    case MagickFormat.Png8:
                        return ImageFormat.Png;
                    default:
                        return ImageFormat.Unknown;
                }
            }
        }

        /// <summary>
        /// Gets the image format by the file extension of the image file.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>The file format.</returns>
        public static ImageFormat GetImageFormat(string fileExtension)
        {
            fileExtension = fileExtension.ToUpperInvariant();

            switch (fileExtension)
            {
                case ".JPEG":
                case ".JPG":
                    return ImageFormat.Jpeg;
                case ".PNG":
                    return ImageFormat.Png;
                default:
                    return ImageFormat.Unknown;
            }
        }
    }
}
