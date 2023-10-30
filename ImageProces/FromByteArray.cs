using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    /// <summary>
    /// Třída FromByteArray poskytuje metody pro převod pole bajtů na různé typy obrázků.
    /// </summary>
    public sealed class FromByteArray
    {
        /// <summary>
        /// Převede pole bajtů na BitmapImage.
        /// </summary>
        /// <param name="byteArray">Pole bajtů</param>
        /// <returns>BitmapImage a chybová zpráva</returns>
        public static (BitmapImage IMG, string Error) ByteArrayToBitmapImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return (null, "Chyba: Pole bajtů je null nebo prázdné.");
            }

            try
            {
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();

                    return (bitmapImage, string.Empty);
                }
            }
            catch (Exception ex)
            {
                return (null, $"Chyba při převodu pole bajtů na BitmapImage: {ex.Message}");
            }
        }

        /// <summary>
        /// Převede pole bajtů na Bitmap.
        /// </summary>
        /// <param name="byteArray">Pole bajtů</param>
        /// <returns>Bitmap a chybová zpráva</returns>
        public static (Bitmap IMG, string Error) ByteArrayToBitmap(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return (null, "Chyba: Pole bajtů je null nebo prázdné.");
            }

            try
            {
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    Bitmap bitmap = new Bitmap(stream);
                    return (bitmap, string.Empty);
                }
            }
            catch (Exception ex)
            {
                return (null, $"Chyba při převodu pole bajtů na Bitmap: {ex.Message}");
            }
        }
    }
}
