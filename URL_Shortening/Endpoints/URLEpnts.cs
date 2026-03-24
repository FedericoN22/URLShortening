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

        app.MapDelete("/urls/{id}", async (int id, ApplicationDbContext dbContext) =>
        {
            var urlService = new UrlService(dbContext);
            var (success, error) = await urlService.DeleteAsync(id);

            if (!success)
                return Results.NotFound(new { error });

            return Results.Ok(new { message = "URL deleted successfully" });
        });

        app.MapPut("/urls/{id}", async (int id, UrlDto urlDto, ApplicationDbContext dbContext) =>
        {
            var urlService = new UrlService(dbContext);
            var (success, error, entity) = await urlService.UpdateAsync(id, urlDto.Url);

            if (!success)
                return Results.BadRequest(new { error });

            return Results.Ok(entity);
        });
    }
}