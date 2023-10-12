using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace M4_Project.Utilities
{
    public static class Images
    {
        /// <summary>
        /// Converts an Image to a byte array.
        /// </summary>
        /// <param name="image">The Image to be converted.</param>
        /// <returns>A byte array representing the image.</returns>
        static byte[] ImageToBytes(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        public static byte[] GetImage(string url)
        {
            string defaultImagePath = HttpContext.Current.Server.MapPath(url);
            try
            {
                using (FileStream fs = new FileStream(defaultImagePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, (int)fs.Length);
                    return imageData;
                }
            }
            catch
            {
                return new byte[0];
            }
        }
    }
}