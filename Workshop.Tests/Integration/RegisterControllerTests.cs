using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Xunit;
using Workshop.DTOs;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
namespace Workshop.Tests.Integration
{
    public class RegisterControllerTests : IntegrationTestBase
    {
        public RegisterControllerTests(CustomWebApplicationFactory<Program> factory):base(factory) {}

        [Fact]
        public async Task Register_WhenValidData_ShouldReturnToken()
        {
            // Arrange
            var registerUserDto = new RegisterUserDto
            {
                Login = "newuser",
                Email = "newuser@example.com",
                Password = "SecurePassword123"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Register/register", registerUserDto);

            // Assert
            response.EnsureSuccessStatusCode();  // Status 200 OK
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            var token = jsonResponse.GetProperty("token").GetString();

            // Sprawdzamy, czy token nie jest pusty
            Assert.False(string.IsNullOrEmpty(token), "Token should not be null or empty.");
        }

        [Fact]
        public async Task Register_WhenInvalidData_ShouldReturnBadRequest()
        {
            // Arrange
            var registerUserDto = new RegisterUserDto
            {
                Login = "",  // Pusty login
                Email = "invalidemail",  // Niepoprawny format email
                Password = "123"  // Zbyt krótkie hasło
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Register/register", registerUserDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);  // Status 400 Bad Request
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            // Проверяем, что есть ошибки валидации

            var errors = jsonResponse.GetProperty("errors");

            Assert.True(errors.TryGetProperty("Email", out var emailErrors));
            Assert.True(errors.TryGetProperty("Login", out var loginErrors));
            Assert.True(errors.TryGetProperty("Password", out var passwordErrors));

            // Sprawdzamy zawartość błędów dla każdego z pól

            // Błąd dla pola "Email"
            var emailErrorList = emailErrors.EnumerateArray().Select(e => e.GetString()).ToList();
            Assert.Contains("Invalid email format.", emailErrorList);

            // Błąd dla pola "Login"
            var loginErrorList = loginErrors.EnumerateArray().Select(e => e.GetString()).ToList();
            Assert.Contains("Login is required.", loginErrorList);
            Assert.Contains("Login must be between 3 and 50 characters.", loginErrorList);

            // Błąd dla pola "Password"
            var passwordErrorList = passwordErrors.EnumerateArray().Select(e => e.GetString()).ToList();
            Assert.Contains("Password must be at least 6 characters long.", passwordErrorList);
        }

        [Fact]
        public async Task Register_WhenUserAlreadyExists_ShouldReturnBadRequest()
        {
            // Arrange
            var existingUserDto = new RegisterUserDto
            {
                Login = "existinguser",
                Email = "existinguser@example.com",
                Password = "ExistingPassword123"
            };

            // Rejestrujemy pierwszego użytkownika
            var firstResponse = await _client.PostAsJsonAsync("/api/Register/register", existingUserDto);
            firstResponse.EnsureSuccessStatusCode();

            // Act - próbujemy zarejestrować tego samego użytkownika ponownie
            var secondResponse = await _client.PostAsJsonAsync("/api/Register/register", existingUserDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, secondResponse.StatusCode);  // Status 400 Bad Request
            var jsonResponse = await secondResponse.Content.ReadFromJsonAsync<JsonElement>();
            var message = jsonResponse.GetProperty("message").GetString();

            Assert.Equal("Email is already in use.", message);
        }
    }
}
