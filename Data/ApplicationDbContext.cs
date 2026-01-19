using Microsoft.EntityFrameworkCore;
using Npgsql;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
{
    public DbSet<Author> Authors {get; set;}
}