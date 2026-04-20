using Microsoft.EntityFrameworkCore;
using URL_Shortening.DTOs;
using URL_Shortening.Services;
using URL_ShorteningDB;
namespace URL_Shortening.Endpoints;

public static class URLEpnts
{
    public static void MapUrlEndpoints(this WebApplication app)
    {
        app.MapPost("/shorten-url", async (UrlDto urlDto, ApplicationDbContext dbContext) =>
        {
            var urlService = new UrlService(dbContext);
            var (success, error, entity) = await urlService.ValidateAndCreateAsync(urlDto.Url);

            if (!success)
                return Results.BadRequest(new { error });

            var response = new SalidaDto(
                ShortUrl: entity!.UrlCorta ?? "",
                OriginalUrl: entity.UrlOriginal,
                Clicks: entity.Clicks
            );

            return Results.Ok(response);
        });

        app.MapGet("/urls", async (ApplicationDbContext dbContext) =>
        {
            var urls = await dbContext.UrlMappings.OrderByDescending(u => u.Id).ToListAsync();
            var response = urls.Select(u => new SalidaDto(
                ShortUrl: u.UrlCorta ?? "",
                OriginalUrl: u.UrlOriginal,
                Clicks: u.Clicks
            ));
            return Results.Ok(response);
        });

        app.MapDelete("/urls/{shortCode}", async (string shortCode, ApplicationDbContext dbContext) =>
        {
            var urlService = new UrlService(dbContext);
            var (success, error) = await urlService.DeleteByShortCodeAsync(shortCode);

            if (!success)
                return Results.NotFound(new { error });

            return Results.Ok(new { message = "URL deleted successfully" });
        });

        app.MapPut("/urls/{shortCode}", async (string shortCode, UrlDto urlDto, ApplicationDbContext dbContext) =>
        {
            var urlService = new UrlService(dbContext);
            var (success, error, entity) = await urlService.UpdateByShortCodeAsync(shortCode, urlDto.Url);

            if (!success)
                return Results.BadRequest(new { error });

            return Results.Ok(entity);
        });

        app.MapGet("/r/{shortCode}", async (string shortCode, ApplicationDbContext dbContext) =>
        {
            var clickService = new ClickService(dbContext);
            var (success, error, data) = await clickService.IncrementAndGetAsync(shortCode);

            if (!success)
                return Results.NotFound(new { error });

            return Results.Redirect(data!.OriginalUrl, permanent: false);
        });

        app.MapGet("/stats/{shortCode}", async (string shortCode, ApplicationDbContext dbContext) =>
        {
            var clickService = new ClickService(dbContext);
            var data = await clickService.GetStatsAsync(shortCode);

            if (data == null)
                return Results.NotFound(new { error = "URL not found" });

            return Results.Ok(data);
        });

        app.MapPost("/debug/increment/{shortCode}", async (string shortCode, ApplicationDbContext dbContext) =>
        {
            var clickService = new ClickService(dbContext);
            var (success, error, data) = await clickService.IncrementAndGetAsync(shortCode);

            return Results.Ok(new { success, error, data });
        });
    }
}