using Microsoft.EntityFrameworkCore;
using MYAPI.Models.EXAPIEntities;

namespace MYAPI.Data.Context;

public class TestMYAPIDbContext : DbContext
{
    public TestMYAPIDbContext(DbContextOptions<TestMYAPIDbContext> options) : base(options)
    {

    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<CreditCard> CreditCards { get; set; }
    public virtual DbSet<Employment> Employments { get; set; }
    public virtual DbSet<Subscription> Subscriptions { get; set; }
    public virtual DbSet<Coordinates> Coordinates { get; set; }  
}
