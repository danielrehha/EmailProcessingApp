using EmailProcessingApp.Domain.Models;
using EmailProcessingApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmailProcessingApp.Persistence.Tests.UnitTests
{
    [Collection("Parallel")]
    public class ResponseEmailRepositoryTests
    {
        private const string _testEmailAddress = "test@test.com";

        [Fact]
        public async Task Should_Return_Response_Email()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EPA_DB")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.SendEmails.Add(new ResponseEmail
                {
                    Key = Guid.NewGuid(),
                    Email = _testEmailAddress,
                    EmailBody = Encoding.UTF8.GetBytes("first"),
                    IsSent = false,
                    CreationDate = DateTime.Now
                });
                context.SendEmails.Add(new ResponseEmail
                {
                    Key = Guid.NewGuid(),
                    Email = _testEmailAddress,
                    EmailBody = Encoding.UTF8.GetBytes("second"),
                    IsSent = false,
                    CreationDate = DateTime.Now.AddDays(1)
                });
                context.SendEmails.Add(new ResponseEmail
                {
                    Key = Guid.NewGuid(),
                    Email = _testEmailAddress,
                    EmailBody = Encoding.UTF8.GetBytes("third"),
                    IsSent = false,
                    CreationDate = DateTime.Now.Subtract(TimeSpan.FromDays(1))
                });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options))
            {
                ResponseEmailRepository repository = new ResponseEmailRepository(context);
                var result = await repository.FindByEmailAndDateAsync(_testEmailAddress, DateTime.Now);

                Assert.NotNull(result);
                Assert.True(result.EmailBody.Length > 0);
                Assert.True("first" == Encoding.UTF8.GetString(result.EmailBody));
            }
        }
    }
}
