using Microsoft.EntityFrameworkCore;
using URL_Shortening.Entidades;
using URL_Shortening.DTOs;
using URL_ShorteningDB;

namespace URL_Shortening.Services;

public class ClickService
{
    private readonly ApplicationDbContext _db;

    public ClickService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<(bool Success, string? Error, SalidaDto? Data)> IncrementAndGetAsync(string shortCode)
    {
        var entity = await _db.UrlMappings.FirstOrDefaultAsync(u => u.UrlCorta == shortCode);
        
        if (entity == null)
            return (false, "URL not found", null);

        var newClicks = entity.Clicks + 1;
        var now = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        
        await _db.Database.ExecuteSqlRawAsync(
            "UPDATE UrlMappings SET Clicks = {0}, updateAt = {1} WHERE Id = {2}",
            newClicks, now, entity.Id);

        var salida = new SalidaDto(
            ShortUrl: entity.UrlCorta!,
            OriginalUrl: entity.UrlOriginal,
            Clicks: newClicks
        );

        return (true, null, salida);
    }

    public async Task<SalidaDto?> GetStatsAsync(string shortCode)
    {
        var entity = await _db.UrlMappings.FirstOrDefaultAsync(u => u.UrlCorta == shortCode);
        
        if (entity == null)
            return null;

        return new SalidaDto(
            ShortUrl: entity.UrlCorta!,
            OriginalUrl: entity.UrlOriginal,
            Clicks: entity.Clicks
        );
    }
}
