# Class-ImageProces
Třída to zpracování a převod obrázků
 .NET Framework 4.5

<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Popis Použití Knihovny pro Zpracování Obrázků</title>
</head>
<body>

<h2>Třída <code>ToByteArray</code> 📸</h2>

<h3>Veřejné Metody:</h3>
<ul>
    <li><code>ImageByteArray(BitmapImage bitmapImage)</code>: Převede <code>BitmapImage</code> na pole bajtů.</li>
    <li><code>ImageByteArray(Uri uri)</code>: Převede URI na pole bajtů.</li>
    <li><code>ImageByteArray(Stream stream)</code>: Převede stream na pole bajtů.</li>
    <li><code>ImageByteArray(BitmapSource bitmapSource)</code>: Převede <code>BitmapSource</code> na pole bajtů.</li>
</ul>

<p>Příklad použití:</p>

<pre><code>
var bitmapImage = new BitmapImage(new Uri("cesta_k_obrazku.jpg"));
var result = await ToByteArray.ImageByteArray(bitmapImage);
if (string.IsNullOrEmpty(result.Error))
{
    byte[] byteArray = result.ByteArray;
    // prováděj další operace s polem bajtů
}
else
{
    console.log(`Chyba: ${result.Error}`);
}
</code></pre>

<h2>Třída <code>ImageProcessing</code> 🎨</h2>

<h3>Veřejné Metody:</h3>
<ul>
    <li><code>GetBitmapImageFreezable(Uri img, double Width = double.NaN, double Height = double.NaN, string FTP_User = null, string FTP_Password = null)</code>: Načte obrázek typu <code>BitmapImage</code> z URI.</li>
    <li><code>GetFormatConvertedBitmapFreezable(byte[] byteArray, bool Gray)</code>: Konvertuje pole bajtů na <code>FormatConvertedBitmap</code> s možností aplikovat efekt šedé barvy.</li>
    <li><code>GetFormatConvertedBitmapFreezable(Uri img, bool Gray, double Width = double.NaN, double Height = double.NaN, string FTP_User = null, string FTP_Password = null)</code>: Umožní efekt šedé barvy na základě obrázku z URI.</li>
    <li><code>GetFormatConvertedBitmapFreezable(BitmapImage image, bool Gray)</code>: Vytvoří efekt šedé barvy na základě obrázku typu <code>BitmapImage</code>.</li>
</ul>

<p>Příklad použití:</p>

<pre><code>
var uri = new Uri("c:\\cesta_k_obrazku.jpg");
var result = await ImageProcessing.GetBitmapImageFreezable(uri, 300, 200);
if (string.IsNullOrEmpty(result.Err))
{
    var bitmapImage = result.IMG;
    // prováděj další operace s BitmapImage
}
else
{
    console.log(`Chyba: ${result.Err}`);
}
</code></pre>

<h2>Třída <code>FromByteArray</code> 🔄</h2>

<h3>Veřejné Metody:</h3>
<ul>
    <li><code>ByteArrayToBitmapImage(byte[] byteArray)</code>: Převede pole bajtů na <code>BitmapImage</code>.</li>
    <li><code>ByteArrayToBitmap(byte[] byteArray)</code>: Převede pole bajtů na <code>Bitmap</code>.</li>
</ul>

<p>Příklad použití:</p>

<pre><code>
byte[] byteArray = // načti pole bajtů
var result = FromByteArray.ByteArrayToBitmapImage(byteArray);
if (string.IsNullOrEmpty(result.Error))
{
    var bitmapImage = result.IMG;
    // prováděj další operace s BitmapImage
}
else
{
    console.log(`Chyba: ${result.Error}`);
}
</code></pre>

<p>Tímto způsobem můžeš snadno a efektivně manipulovat s obrázky ve svých C# projektech. Pokud máš nějaké otázky, neváhej se zeptat! 🖼️💻🚀</p>

</body>
</html>
