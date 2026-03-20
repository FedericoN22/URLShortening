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

            return Results.Ok(entity);
        });

        app.MapGet("/urls", async (ApplicationDbContext dbContext) =>
        {
            var urls = await dbContext.UrlMappings.OrderByDescending(u => u.Id).ToListAsync();
            return Results.Ok(urls);
        });
    }
}