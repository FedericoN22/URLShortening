using Microsoft.EntityFrameworkCore;
using URL_ShorteningDB;
using URL_Shortening.Endpoints;
using URL_Shortening.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ShortCode>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("Data Source=urlshortening.db");
});

builder.Services.AddCors();


var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapUrlEndpoints();

app.Run();
