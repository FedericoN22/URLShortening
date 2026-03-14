using Microsoft.EntityFrameworkCore;
using URL_Shortening.DTOs;
using URL_Shortening.Entidades;
using URL_Shortening.Services;
using URL_ShorteningDB; // ← el namespace donde está tu ApplicationDbContext
namespace URL_Shortening.Endpoints;

public static class URLEpnts
{
    public static void MapUrlEndpoints(this WebApplication app)
    {
        app.MapPost("/shorten-url", async (UrlDto urlDto, ApplicationDbContext dbContext) =>
        {
            var shortCode = new ShortCode();

            var shortUrlEntity = new UrlsEntidades
            {
                UrlOriginal = urlDto.Url,
                UrlCorta = shortCode.GenerateShortCode(urlDto.Url),
                createAt = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            };

            dbContext.Add(shortUrlEntity);
            await dbContext.SaveChangesAsync();

            return Results.Ok(shortUrlEntity);
        });

        app.MapGet("/urls", async (ApplicationDbContext dbContext) =>
        {
            var urls = await dbContext.UrlMappings.OrderByDescending(u => u.Id).ToListAsync();
            return Results.Ok(urls);
        });
    }
}