using LeadService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeadService.Infrastructure.Persistence
{
    public class LeadDbContext : DbContext
    {
        public LeadDbContext(DbContextOptions<LeadDbContext> options)
           : base(options)
        {
        }

        // Example DbSets
        public DbSet<Lead> Leads { get; set; }
        public DbSet<LeadAddress> LeadAddresses { get; set; }
        public DbSet<LeadCommDetail> LeadCommDetails { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
