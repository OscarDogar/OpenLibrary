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

        public DbSet<GenderEntity> Genders { get; set; }

        public DbSet<TypeOfDocumentEntity> TypeOfDocuments { get; set; }

        public DbSet<DocumentLanguageEntity> DocumentLanguages { get; set; }

        public DbSet<DocumentEntity> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthorEntity>()
                   .HasIndex(t => t.Name)
                   .IsUnique();
            modelBuilder.Entity<DocumentLanguageEntity>()
                   .HasIndex(t => t.Name)
                   .IsUnique();
            modelBuilder.Entity<GenderEntity>()
                   .HasIndex(t => t.Name)
                   .IsUnique();
            modelBuilder.Entity<TypeOfDocumentEntity>()
                   .HasIndex(t => t.Name)
                   .IsUnique();
        }
    }
}
