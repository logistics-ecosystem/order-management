using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.Metrics;
using Logistics.Models;

namespace Logistics.DBContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<AcceptedOrder> AcceptedOrdersHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Order>().HasOne(o => o.AcceptedOrder).WithOne(ao => ao.Orders).HasForeignKey<AcceptedOrder>(ao => ao.OrderId);

            /*modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.AddressFrom).IsRequired(); 
                entity.Property(e => e.AddressTo).IsRequired();
                entity.Property(e => e.DateTimeFrom).IsRequired();
                entity.Property(e => e.DateTimeTo).IsRequired();
                //entity.Property(e => e.FrachtType).IsRequired();
                entity.Property(e => e.Distance).IsRequired();
                //entity.Property(e => e.TrunkType).IsRequired();
                entity.Property(e => e.Weight).IsRequired();
                entity.Property(e => e.LoadingMetre).IsRequired();
                entity.Property(e => e.Height).IsRequired();
                //entity.Property(e => e.LoadingType).IsRequired();
                entity.Property(e => e.Temperature).IsRequired();
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.ContactInfo).IsRequired().HasMaxLength(255);
                entity.Property(e => e.UniqueId).IsRequired();

                entity.Property(e => e.AddressFrom).HasDefaultValue("");
                entity.Property(e => e.AddressTo).HasDefaultValue("");
                entity.Property(e => e.ContactInfo).HasDefaultValue("");


            });

            modelBuilder.Entity<Order>().HasIndex(o => o.DateTimeFrom);
            modelBuilder.Entity<Order>().HasIndex(o => o.DateTimeTo);

            modelBuilder.Entity<AcceptedOrder>(entity =>
            {
                entity.Property(e => e.DateTimeAccepted).IsRequired();
                entity.Property(e => e.UniqueId).IsRequired();
                entity.Property(e => e.OrderId).IsRequired();
            });*/
        }
        
    }
}
