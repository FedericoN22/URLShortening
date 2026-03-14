using Microsoft.EntityFrameworkCore;
using URL_Shortening.Entidades;
namespace URL_ShorteningDB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UrlsEntidades> UrlMappings { get; set; }
    }
}