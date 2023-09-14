using Bogus;
using MYAPI.Data.Entities;

namespace MYAPI.Tests.MockServices;

public class FakeUsersForUnitTests
{
    public static List<User> GenerateUsers(int count)
    {
        var faker = new Faker<User>()
            .RuleFor(u => u.Id, f => f.Random.Number(1, 10000))
            .RuleFor(u => u.Uid, f => f.Random.Guid())
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Avatar, f => new Uri(f.Internet.Avatar()))
            .RuleFor(u => u.Gender, f => f.PickRandom(new[] { "Male", "Female", "Polygender", "Other" }))
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.SocialInsuranceNumber, f => f.Random.Long(10000000000, 10000000000000))
            .RuleFor(u => u.DateOfBirth, f => f.Date.Between(new DateTime(1960, 1, 1), new DateTime(2000, 12, 31)))
            .RuleFor(u => u.CreationDate, DateTime.Now)
            .RuleFor(u => u.Employment, f => new Employment
            {
                Title = f.Name.JobTitle(),
                KeySkill = f.PickRandom(new[] { "Problem solving", "Networking skills", "Self-motivated" })
            })
            .RuleFor(u => u.Address, f => new Address
            {
                City = f.Address.City(),
                StreetName = f.Address.StreetName(),
                StreetAddress = f.Address.StreetAddress(),
                ZipCode = f.Address.ZipCode(),
                State = f.Address.State(),
                Country = f.Address.Country(),
                Coordinates = new Coordinates
                {
                    Lat = f.Address.Latitude(),
                    Lng = f.Address.Longitude()
                }
            })
            .RuleFor(u => u.CreditCard, f => new CreditCard
            {
                CcNumber = f.Finance.CreditCardNumber()
            })
            .RuleFor(u => u.Subscription, f => new Subscription
            {
                Plan = f.PickRandom(new[] { "Diamond", "Gold", "Business" }),
                Status = f.PickRandom(new[] { "Active", "Idle", "Canceled" }),
                PaymentMethod = f.PickRandom(new[] { "Debit card", "WeChat Pay", "Google Pay", "Credit card" }),
                Term = f.PickRandom(new[] { "Monthly", "Quarterly", "Annually" })
            })
            .Generate(count);

        return faker;
    }
}
