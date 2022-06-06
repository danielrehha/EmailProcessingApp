using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Infrastructure.Static;
using EmailProcessingApp.Tests.Shared.Core;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace EmailProcessingApp.Infrastructure.Tests.ComponentTests
{
    [Collection("Serial")]

    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class MessageTemplateServiceTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly MessageTemplateService _messageTemplateService;

        public MessageTemplateServiceTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;

            var blobServiceInstance = _factory.Services.GetService(typeof(IBlobService));

            _messageTemplateService = new MessageTemplateService((IBlobService)blobServiceInstance, _factory.Configuration);
        }

        [Fact]
        [Priority(0)]
        public async Task Should_Return_Message_Template()
        {
            var result = await _messageTemplateService.GetMessageTemplateAsync(MessageTemplateType.ResponseEmailBody);

            Assert.NotNull(result);
            Assert.Contains("Congratulations", result);
        }
    }
}
