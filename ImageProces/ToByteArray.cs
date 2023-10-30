using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    /// <summary>
    /// Třída ToByteArray poskytuje metody pro převod různých typů obrázků na pole bajtů.
    /// </summary>
    public sealed class ToByteArray
    {
        /// <summary>
        /// Převede BitmapImage na pole bajtů.
        /// </summary>
        /// <param name="bitmapImage">BitmapImage pro převod</param>
        /// <returns>Pole bajtů a chybová zpráva</returns>
        public static async Task<(byte[] ByteArray, string Error)> ImageByteArray(BitmapImage bitmapImage)
        {
            var frame = BitmapFrame.Create(bitmapImage);
            return await ImageByteArray(frame);
        }

        /// <summary>
        /// Převede URI na pole bajtů.
        /// </summary>
        /// <param name="uri">URI obrázku</param>
        /// <returns>Pole bajtů a chybová zpráva</returns>
        public static async Task<(byte[] ByteArray, string Error)> ImageByteArray(Uri uri)
        {
            var frame = BitmapFrame.Create(uri);
            return await ImageByteArray(frame);
        }

        /// <summary>
        /// Převede stream na pole bajtů.
        /// </summary>
        /// <param name="stream">Stream s obrázkem</param>
        /// <returns>Pole bajtů a chybová zpráva</returns>
        public static async Task<(byte[] ByteArray, string Error)> ImageByteArray(Stream stream)
        {
            var frame = BitmapFrame.Create(stream);
            return await ImageByteArray(frame);
        }

        /// <summary>
        /// Převede BitmapSource na pole bajtů.
        /// </summary>
        /// <param name="bitmapSource">BitmapSource pro převod</param>
        /// <returns>Pole bajtů a chybová zpráva</returns>
        public static async Task<(byte[] ByteArray, string Error)> ImageByteArray(BitmapSource bitmapSource)
        {
            var frame = BitmapFrame.Create(bitmapSource);
            return await ImageByteArray(frame);
        }

        private static Task<(byte[] ByteArray, string Error)> ImageByteArray(BitmapFrame bitmapFrame)
        {
            byte[] imageBytes = null;
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder(); // Můžete použít jiný encoder podle potřeby
                    encoder.Frames.Add(bitmapFrame);
                    encoder.Save(stream);

                    imageBytes = stream.ToArray();
                    return Task.FromResult((imageBytes, string.Empty));
                }
            }
            catch (Exception ex)
            {
                imageBytes = null;
                return Task.FromResult((imageBytes, $"Chyba při převodu na pole bajtů: {ex.Message}"));
            }
        }
    }
}
