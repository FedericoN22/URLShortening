using Microsoft.EntityFrameworkCore;
using URL_ShorteningDB;
using URL_Shortening.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("Data Source=urlshortening.db");
});

builder.Services.AddCors();


var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapUrlEndpoints();

app.Run();
