using Microsoft.EntityFrameworkCore;
using MYAPI.Data.Entities;

namespace MYAPI.Data;

public class DataContext : DbContext
{
    public DataContext()
    {

    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public void Migrate()
        => Database.Migrate();

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<CreditCard> CreditCards { get; set; }
    public virtual DbSet<Employment> Employments { get; set; }
    public virtual DbSet<Subscription> Subscriptions { get; set; }
    public virtual DbSet<Coordinates> Coordinates { get; set; }
}
