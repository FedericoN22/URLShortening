namespace URL_Shortening.DTOs;

public record SalidaDto(
    string ShortUrl,
    string OriginalUrl,
    int Clicks
);