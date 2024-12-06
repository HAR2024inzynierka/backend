using System.Net.Http.Json;
using Xunit;
using Workshop.DTOs;
using System.Text.Json;

namespace Workshop.Tests.Integration
{
    public class AuthControllerTests : IntegrationTestBase
    {
        public AuthControllerTests(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Login_WhenValidCredentials_ShouldReturnToken()
        {
            // Arrange
            var loginData = new LoginUserDto
            {
                Email = "test1@example.com",
                Password = "test123"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Auth/login", loginData);
            
            var content = await response.Content.ReadAsStringAsync();
            


            // Assert
            response.EnsureSuccessStatusCode();  // Status 200 OK
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var token = jsonResponse.GetProperty("token").GetString();

            Assert.False(string.IsNullOrEmpty(token), "Token should not be null or empty.");
        
        }

        [Fact]
        public async Task Login_WhenInvalidCredentials_ShouldReturnBadRequest()
        {
            // Arrange
            var loginData = new LoginUserDto
            {
                Email = "wrong@example.com",
                Password = "wrongpassword"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Auth/login", loginData);

            // Assert
            Assert.False(response.IsSuccessStatusCode);  // Status 400 Bad Request
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var message = jsonResponse.GetProperty("message").GetString();

            Assert.Equal("Invalid email or password", message);
        }
    }
}
