using GameOfLife.Services;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using GameOfLife.Models;
using GameOfLife;

namespace IntegrationTests
{
    public class GameOfLifeApiIntegrationTests : IClassFixture<WebApplicationFactory<GameOfLife.Program>>
    {
        private readonly HttpClient _client;

        public GameOfLifeApiIntegrationTests(WebApplicationFactory<GameOfLife.Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_UploadBoard_ReturnsGuidAnd200()
        {
            var board = new Board
            {
                Rows = 3,
                Columns = 3,
                Cells = new[]
                {
                    new[] { 0, 1, 0 },
                    new[] { 0, 1, 0 },
                    new[] { 0, 1, 0 }
                }
            };

            var response = await _client.PostAsJsonAsync("/api/GameOfLife/save", board);
            response.EnsureSuccessStatusCode();

            var id = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrWhiteSpace(id));
        }
    }
}