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

        entity.Clicks++;
        entity.updateAt = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        await _db.SaveChangesAsync();

        var salida = new SalidaDto(
            ShortUrl: entity.UrlCorta!,
            OriginalUrl: entity.UrlOriginal,
            Clicks: entity.Clicks
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
