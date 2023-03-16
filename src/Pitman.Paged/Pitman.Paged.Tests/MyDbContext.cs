using Microsoft.EntityFrameworkCore;

namespace Pitman.Paged.Tests
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<MyEntity> MyEntities { get; set; }
    }
}