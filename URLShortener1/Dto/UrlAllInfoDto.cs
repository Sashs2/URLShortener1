namespace URLShortener1.Dto
{
    public class UrlAllInfoDto
    {
        public string LongUrl { get; set; } = string.Empty;

        public string ShortUrl { get; set; } = string.Empty;

        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UserName { get; set; }
    }
}
