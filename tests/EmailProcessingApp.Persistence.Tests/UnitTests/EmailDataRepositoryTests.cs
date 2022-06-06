using EmailProcessingApp.Domain.Models;
using EmailProcessingApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EmailProcessingApp.Persistence.Tests.UnitTests
{
    [Collection("Parallel")]
    public class EmailDataRepositoryTests
    {
        private const string _testEmailAddress = "test@test.com";

        [Fact]
        public async Task Should_Validate_Attribute_List()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EPA_DB")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.EmailData.Add(new EmailData
                {
                    Key = Guid.NewGuid(),
                    Email = _testEmailAddress,
                    Attributes = "1,2,3,4,5,6,7,8",
                    CreationDate = DateTime.Now
                });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options))
            {
                EmailDataRepository repository = new EmailDataRepository(context);
                var result = await repository.ValidateAttributeListAsync(_testEmailAddress, new List<string>() { "9", "10" });

                Assert.True(result);
            }
        }

        [Fact]
        public async Task Should_Not_Validate_Attribute_List()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EPA_DB")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.EmailData.Add(new EmailData
                {
                    Key = Guid.NewGuid(),
                    Email = _testEmailAddress,
                    Attributes = "1,2,3,4,5,6,7,8,9",
                    CreationDate = DateTime.Now
                });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options))
            {
                EmailDataRepository repository = new EmailDataRepository(context);
                var result = await repository.ValidateAttributeListAsync(_testEmailAddress, new List<string>() { "9", "10" });

                Assert.False(result);
            }
        }

        [Fact]
        public async Task Should_Return_Email_Data_By_Range()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EPA_DB")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.EmailData.Add(new EmailData
                {
                    Key = Guid.NewGuid(),
                    Email = _testEmailAddress,
                    Attributes = "first",
                    CreationDate = DateTime.Now
                });
                context.EmailData.Add(new EmailData
                {
                    Key = Guid.NewGuid(),
                    Email = _testEmailAddress,
                    Attributes = "second",
                    CreationDate = DateTime.Now.Subtract(TimeSpan.FromDays(1))
                });
                context.EmailData.Add(new EmailData
                {
                    Key = Guid.NewGuid(),
                    Email = _testEmailAddress,
                    Attributes = "third",
                    CreationDate = DateTime.Now.AddDays(1)
                });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options))
            {
                EmailDataRepository repository = new EmailDataRepository(context);
                var result = await repository.GetRangeByEmailAddressAsync(_testEmailAddress, DateTime.Now, DateTime.Now);

                Assert.True(result.Count == 1);
                Assert.True(result.First().Attributes == "first");
            }
        }
    }
}
