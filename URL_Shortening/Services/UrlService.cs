using Microsoft.EntityFrameworkCore;
using URL_Shortening.Entidades;
using URL_ShorteningDB;

namespace URL_Shortening.Services;

public class UrlService
{
    private readonly ApplicationDbContext _db;

    public UrlService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<(bool Success, string? Error, UrlsEntidades? Entity)> ValidateAndCreateAsync(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return (false, "URL cannot be empty", null);

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
            (uri.Scheme != "http" && uri.Scheme != "https"))
            return (false, "Invalid URL format", null);

        var normalized = uri.GetLeftPart(UriPartial.Path).ToLower();

        var existing = await _db.UrlMappings
            .FirstOrDefaultAsync(u => u.UrlOriginal == normalized);

        if (existing != null)
            return (true, null, existing);

        var entity = new UrlsEntidades
        {
            UrlOriginal = normalized,
            createAt = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        };

        _db.Add(entity);
        await _db.SaveChangesAsync();

        entity.UrlCorta = ShortCode.GenerateShortCode(entity.Id + 10000000);

        await _db.SaveChangesAsync();

        return (true, null, entity);
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(int id)
    {
        var entity = await _db.UrlMappings.FindAsync(id);
        if (entity == null)
            return (false, "URL not found");

        _db.UrlMappings.Remove(entity);
        await _db.SaveChangesAsync();

        return (true, null);
    }

    public async Task<(bool Success, string? Error, UrlsEntidades? Entity)> UpdateAsync(int id, string newUrl)
    {
        if (string.IsNullOrWhiteSpace(newUrl))
            return (false, "URL cannot be empty", null);

        if (!Uri.TryCreate(newUrl, UriKind.Absolute, out var uri) ||
            (uri.Scheme != "http" && uri.Scheme != "https"))
            return (false, "Invalid URL format", null);

        var entity = await _db.UrlMappings.FindAsync(id);
        if (entity == null)
            return (false, "URL not found", null);

        entity.UrlOriginal = uri.GetLeftPart(UriPartial.Path).ToLower();
        entity.updateAt = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        await _db.SaveChangesAsync();

        return (true, null, entity);
    }
}