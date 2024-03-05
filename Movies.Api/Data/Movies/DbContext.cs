using Microsoft.EntityFrameworkCore;

namespace Movies.Api.Data.Movies;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext(DbContextOptions options)
        : base(options)
    {
    }

    public virtual DbSet<Movie> Movies => Set<Movie>();
}
