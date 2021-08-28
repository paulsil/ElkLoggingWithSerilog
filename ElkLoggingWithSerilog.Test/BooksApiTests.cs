using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using Books.API;
using Xunit;

namespace ElkLoggingWithSerilog.Test
{
    public class BooksApiTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public BooksApiTests()
        {
            _server = new TestServer(new WebHostBuilder()
           .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetBob()
        {
            // Act
            var response = await _client.GetAsync("/Book");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal("{\"name\":\"Bob\"}", responseString);
        }
    }
}
