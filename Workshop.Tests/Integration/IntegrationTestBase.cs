using System.Net.Http;
using Xunit;

namespace Workshop.Tests.Integration
{
    public class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;

        public IntegrationTestBase(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
    }
}
