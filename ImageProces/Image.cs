using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    /// <summary>
    /// Třída Image poskytuje metody pro práci s obrázky, včetně jejich načítání, konverze a aplikace efektu šedé barvy.
    /// </summary>
    public sealed class ImageProcessing
    {
        /// <summary>
        /// Načte obrázek typu BitmapImage z URI.
        /// </summary>
        /// <param name="img">URI obrázku</param>
        /// <param name="Width">Šířka obrázku</param>
        /// <param name="Height">Výška obrázku</param>
        /// <param name="FTP_User">Uživatelské jméno pro FTP (nepovinné)</param>
        /// <param name="FTP_Password">Heslo pro FTP (nepovinné)</param>
        /// <returns>BitmapImage a chybovou zprávu</returns>
        public static async Task<(BitmapImage IMG, string Err)> GetBitmapImageFreezable(Uri img, double Width = double.NaN, double Height = double.NaN, string FTP_User = null, string FTP_Password = null)
        {
            BitmapImage _image = null;

            try
            {
                _image = new BitmapImage();
                _image.BeginInit();

                // Použití původního URI, pokud je obrázek soubor
                if (img.IsFile)
                {
                    _image.UriSource = img;
                }
                else
                {
                    // Obrázek z FTP
                    if (img.ToString().Contains("ftp://"))
                    {
                        WebClient webClient = new WebClient();
                        if (!string.IsNullOrEmpty(FTP_User) && !string.IsNullOrEmpty(FTP_Password))
                        {
                            webClient.Credentials = new NetworkCredential(FTP_User, FTP_Password);
                        }

                        byte[] data = await webClient.DownloadDataTaskAsync(img);
                        MemoryStream ms = new MemoryStream(data);

                        _image.CacheOption = BitmapCacheOption.OnLoad;
                        _image.StreamSource = ms;
                    }
                    // Obrázek z HTTP/HTTPS
                    else
                    {
                        var client = new HttpClient();
                        Stream stream = await client.GetStreamAsync(img);

                        _image.StreamSource = stream;
                    }

                    // Nastavení šířky a výšky obrázku
                    if (Height > 0 && Width > 1 && Height < 9999 && Width < 9999)
                    {
                        _image.DecodePixelHeight = (int)Height;
                        _image.DecodePixelWidth = (int)Width;
                    }
                }
                _image.EndInit();

                // Počkejte, dokud se obrázek nerozmrazí
                while (!_image.CanFreeze)
                {
                    await Task.Delay(100);
                }
                return await Task.FromResult((_image, ""));
            }
            catch (Exception ee)
            {
                _image = null;
                return await Task.FromResult((_image, ee.Message));
            }
        }

        /// <summary>
        /// Konvertuje pole bajtů na BitmapImage s možností aplikovat efekt šedé barvy.
        /// </summary>
        /// <param name="byteArray">Pole bajtů obrázku</param>
        /// <param name="Gray">Převést na šedou (true/false)</param>
        /// <returns>BitmapImage s efektem a chybovou zprávu</returns>
        public static async Task<(FormatConvertedBitmap, string Err)> GetFormatConvertedBitmapFreezable(byte[] byteArray, bool Gray)
        {
            FormatConvertedBitmap formatConvertedBitmap = new FormatConvertedBitmap();

            try
            {
                formatConvertedBitmap.BeginInit();
                var IMG = FromByteArray.ByteArrayToBitmapImage(byteArray);

                if (string.IsNullOrEmpty(IMG.Error))
                {
                    formatConvertedBitmap.Source = IMG.IMG;
                    formatConvertedBitmap.DestinationFormat = Gray ? PixelFormats.Gray8 : PixelFormats.Rgb24;
                    formatConvertedBitmap.EndInit();

                    // Počkejte, dokud se efekt nezmrazí
                    while (!formatConvertedBitmap.CanFreeze)
                    {
                        await Task.Delay(100);
                    }
                }
                else
                {
                    throw new Exception(IMG.Error);
                }
                return await Task.FromResult((formatConvertedBitmap, ""));
            }
            catch (Exception ee)
            {
                formatConvertedBitmap = null;
                return await Task.FromResult((formatConvertedBitmap, ee.Message));
            }
        }

        /// <summary>
        /// Umožní efekt šedé barvy na základě obrázku z URI.
        /// </summary>
        /// <param name="img">URI obrázku</param>
        /// <param name="Gray">Převést na šedou (true/false)</param>
        /// <param name="Width">Šířka obrázku</param>
        /// <param name="Height">Výška obrázku</param>
        /// <param name="FTP_User">Uživatelské jméno pro FTP (nepovinné)</param>
        /// <param name="FTP_Password">Heslo pro FTP (nepovinné)</param>
        /// <returns>BitmapImage s efektem šedé barvy a chybovou zprávu</returns>
        public static async Task<(FormatConvertedBitmap IMG, string Err)> GetFormatConvertedBitmapFreezable(Uri img, bool Gray, double Width = double.NaN, double Height = double.NaN, string FTP_User = null, string FTP_Password = null)
        {
            FormatConvertedBitmap formatConvertedBitmap = new FormatConvertedBitmap();

            try
            {
                formatConvertedBitmap.BeginInit();
                var IMG = await GetBitmapImageFreezable(img, Width, Height, FTP_User, FTP_Password);

                if (string.IsNullOrEmpty(IMG.Err))
                {
                    formatConvertedBitmap.Source = IMG.IMG;
                    formatConvertedBitmap.DestinationFormat = Gray ? PixelFormats.Gray8 : PixelFormats.Rgb24;
                    formatConvertedBitmap.EndInit();

                    // Počkejte, dokud se efekt nezmrazí
                    while (!formatConvertedBitmap.CanFreeze)
                    {
                        await Task.Delay(100);
                    }
                }
                else
                {
                    throw new Exception(IMG.Err);
                }
                return await Task.FromResult((formatConvertedBitmap, ""));
            }
            catch (Exception ee)
            {
                formatConvertedBitmap = null;
                return await Task.FromResult((formatConvertedBitmap, ee.Message));
            }
        }

        /// <summary>
        /// Vytvoří efekt šedé barvy na základě obrázku typu BitmapImage.
        /// </summary>
        /// <param name="image">BitmapImage pro efekt</param>
        /// <param name="Gray">Převést na šedou (true/false)</param>
        /// <returns>BitmapImage s efektem šedé barvy a chybovou zprávu</returns>
        public static async Task<(FormatConvertedBitmap IMG, string Err)> GetFormatConvertedBitmapFreezable(BitmapImage image, bool Gray)
        {
            FormatConvertedBitmap formatConvertedBitmap = new FormatConvertedBitmap();

            try
            {
                formatConvertedBitmap.BeginInit();
                formatConvertedBitmap.Source = image;
                formatConvertedBitmap.DestinationFormat = Gray ? PixelFormats.Gray8 : PixelFormats.Rgb24;
                formatConvertedBitmap.EndInit();

                // Počkejte, dokud se efekt nezmrazí
                while (!formatConvertedBitmap.CanFreeze)
                {
                    await Task.Delay(100);
                }
                return await Task.FromResult((formatConvertedBitmap, ""));
            }
            catch (Exception dd)
            {
                formatConvertedBitmap = null;
                return await Task.FromResult((formatConvertedBitmap, dd.Message));
            }
        }
    }
}
