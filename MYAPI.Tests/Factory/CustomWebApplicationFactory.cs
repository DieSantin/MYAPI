using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MYAPI.Data.Context;
using MYAPI.Models.EXAPIEntities;
using MySqlConnector;
using System.Data.Common;
using Bogus;

namespace MYAPI.Tests.Factory;

public class CustomWebApplicationFactory : WebApplicationFactory<Program> 
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
 
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql("Server=localhost;Database=TestMYAPIDatabase;User=root;Password=root", 
                    ServerVersion.AutoDetect("Server=localhost;Database=TestMYAPIDatabase;User=root;Password=root"));
            });

            var provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                var context = scopedServices.GetRequiredService<AppDbContext>();

                context.Users.RemoveRange(context.Users);
                context.Addresses.RemoveRange(context.Addresses);
                context.CreditCards.RemoveRange(context.CreditCards);
                context.Employments.RemoveRange(context.Employments);
                context.Subscriptions.RemoveRange(context.Subscriptions);
                context.Coordinates.RemoveRange(context.Coordinates);
                context.SaveChanges();  

                var faker = new Faker();

                var user = new User
                {
                    Email = "existingUser@email.com",
                    Username = "existingUsername",
                    Gender = "Male",
                    Id = faker.Random.Number(1, 10000),
                    Uid = faker.Random.Guid(),
                    Password = faker.Internet.Password(),
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    Avatar = new Uri(faker.Internet.Avatar()),
                    PhoneNumber = faker.Phone.PhoneNumber(),
                    SocialInsuranceNumber = faker.Random.Long(10000000000, 10000000000000),
                    DateOfBirth = faker.Date.Between(new DateTime(1960, 1, 1), new DateTime(2000, 12, 31)),
                    CreationDate = DateTime.Now,
                    Employment = new Employment
                    {
                        Title = faker.Name.JobTitle(),
                        KeySkill = faker.PickRandom(new[] { "Problem solving", "Networking skills", "Self-motivated" })
                    },
                    CreditCard = new CreditCard
                    {
                        CcNumber = faker.Finance.CreditCardNumber()
                    },
                    Subscription = new Subscription
                    {
                        Plan = faker.PickRandom(new[] { "Diamond", "Gold", "Business" }),
                        Status = faker.PickRandom(new[] { "Active", "Idle", "Canceled" }),
                        PaymentMethod = faker.PickRandom(new[] { "Debit card", "WeChat Pay", "Google Pay", "Credit card" }),
                        Term = faker.PickRandom(new[] { "Monthly", "Quarterly", "Annually" }),
                    },
                    Address = new Address
                    {
                        City = faker.Address.City(),
                        StreetName = faker.Address.StreetName(),
                        StreetAddress = faker.Address.StreetAddress(),
                        ZipCode = faker.Address.ZipCode(),
                        State = faker.Address.State(),
                        Country = faker.Address.Country(),
                        Coordinates = new Coordinates
                        {
                            Lat = faker.Address.Latitude(),
                            Lng = faker.Address.Longitude(),
                        }
                    }             
                };

                if (!context.Users.Any())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
        });
    }
}
