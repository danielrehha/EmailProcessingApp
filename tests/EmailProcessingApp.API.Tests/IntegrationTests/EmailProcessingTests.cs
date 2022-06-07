using EmailProcessingApp.Tests.Shared.Core;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace EmailProcessingApp.API.Tests.IntegrationTests
{
    [Collection("Serial")]

    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class EmailProcessingTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public EmailProcessingTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateDefaultClient(new Uri("https://localhost"));
        }

        [Fact]
        [Priority(0)]
        public async Task Should_Process_Email_Data_With_No_Response_Email()
        {
            /*
             *  Normally I would write end-to-end integration tests covering every possible scenario but I think that is a bit out of scope for this test
             */
        }

        [Fact]
        [Priority(1)]

        public async Task Should_Process_Email_Data_With_Response_Email()
        {
            // ...
        }
    }
}
