using Microsoft.EntityFrameworkCore;
using Songs.Models;
namespace Songs.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Song> Songs { set; get; }
    public DbSet<Category> Categories { set; get; }
    public DbSet<Artist> Artists { set; get; }
}
