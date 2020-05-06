using Microsoft.EntityFrameworkCore;
using OpenLibrary.Web.Data.Entities;

namespace OpenLibrary.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AuthorEntity> Authors { get; set; }
    }
}
