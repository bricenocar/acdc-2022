using Microsoft.EntityFrameworkCore;
using ACDC2022.Models;

namespace ACDC2022.Data
{
    public class ACDC2022DbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Telemetry> Telemetries { get; set; }

        public ACDC2022DbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Users */
            modelBuilder.Entity<User>().ToContainer("Users");
            modelBuilder.Entity<User>().HasNoDiscriminator();
            modelBuilder.Entity<User>().HasPartitionKey(o => o.WalletId);
            modelBuilder.Entity<User>().UseETagConcurrency();

            /* Telemetries */
            modelBuilder.Entity<Telemetry>().ToContainer("Telemetries");
            modelBuilder.Entity<Telemetry>().HasNoDiscriminator();
            modelBuilder.Entity<Telemetry>().HasPartitionKey(o => o.DeviceId);
            modelBuilder.Entity<Telemetry>().UseETagConcurrency();
        }
    }
}
