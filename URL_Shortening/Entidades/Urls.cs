namespace URL_Shortening.Entidades
{
    public class UrlsEntidades
    {
        public int Id { get; set; }
        public string UrlOriginal { get; set; } = null!;
        public string? UrlCorta { get; set; }
        public int Clicks { get; set; } = 0;

        public int createAt { get; set; }

        public int updateAt { get; set; }
    }
}