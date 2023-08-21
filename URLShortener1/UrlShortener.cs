namespace URLShortener1
{
    public class UrlShortener
    { private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const int Base = 62;

    public static string ShortenUrl(string longUrl, int id)
    {
        string shortUrl = ConvertToBase62(id);
        return $"http://yourdomain.com/{shortUrl}";
    }

    private static string ConvertToBase62(int value)
    {
        string result = string.Empty;

        while (value > 0)
        {
            int remainder = value % Base;
            result = Characters[remainder] + result;
            value /= Base;
        }

        return result;
    }
    }
}
