namespace URLShortener1.Dto
{
    public class UrlDto
    {
        public string LongUrl { get; set; } = string.Empty;

        public string ShortUrl { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
