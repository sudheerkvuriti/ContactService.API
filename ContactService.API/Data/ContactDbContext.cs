using ContactService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactService.API.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

        // optional: configure table names / indexes
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>(b =>
            {
                b.ToTable("Contacts");
                b.HasKey(x => x.Id);
                b.HasIndex(x => x.Email).IsUnique(false);
                b.Property(x => x.FirstName).HasMaxLength(100);
                b.Property(x => x.LastName).HasMaxLength(100);
                b.Property(x => x.Email).HasMaxLength(255);
                b.Property(x => x.Phone).HasMaxLength(50);
            });
        }
    }
}
