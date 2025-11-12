using KF.Host.Domain;
using Microsoft.EntityFrameworkCore;

namespace KF.Host.Persistence;

public class DataBaseContext(DbContextOptions<DataBaseContext> options) : DbContext(options)
{
    public DbSet<Key> Keys { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>(b =>
        {
            b.ComplexProperty(e => e.Money);
            b.HasOne(e => e.Subscription).WithOne().HasForeignKey<Subscription>(e => e.PaymentId).IsRequired(false);
        });
    }
}