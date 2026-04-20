namespace URL_Shortening.Entidades
{
    public class UrlsEntidades
    {
        public int Id { get; set; }
        public string UrlOriginal { get; set; } = null!;
        public string? UrlCorta { get; set; }
        public int Clicks { get; set; } = 0;

        public DateTime createAt { get; set; }

        public DateTime updateAt { get; set; }
    }
}