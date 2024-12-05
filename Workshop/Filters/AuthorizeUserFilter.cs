using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Workshop.Filters
{
    /// <summary>
    /// Filtr autoryzacji użytkownika. Służy do weryfikacji, czy użytkownik ma dostęp do zasobów na podstawie jego ID.
    /// Weryfikuje, czy ID użytkownika w tokenie JWT jest zgodne z ID użytkownika w ścieżce URL.
    /// Jeśli ID nie jest zgodne, użytkownikowi zostanie zabroniony dostęp (status 403).
    /// </summary>
    public class AuthorizeUserFilter : IAuthorizationFilter
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Inicjalizuje nową instancję filtra autoryzacji użytkownika.
        /// </summary>
        /// <param name="httpContextAccessor">Dostęp do kontekstu HTTP, umożliwiający odczytanie tokenu użytkownika.</param>
        public AuthorizeUserFilter(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

        /// <summary>
        /// Metoda wykonująca walidację autoryzacji przed przetworzeniem żądania.
        /// Weryfikuje, czy ID użytkownika w tokenie pasuje do ID użytkownika w ścieżce URL.
        /// </summary>
        /// <param name="context">Obiekt zawierający dane o bieżącym żądaniu.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
		{
			var httpContext = _httpContextAccessor.HttpContext;

            // Sprawdza, czy kontekst HTTP jest dostępny
            if (httpContext == null) {
				context.Result = new UnauthorizedResult(); // Użytkownik niezalogowany
                return;
			}

            // Odczytuje ID użytkownika z tokenu (claim NameIdentifier)
            var userIdFromToken = httpContext.User
				.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Jeśli ID użytkownika w tokenie jest puste, zwraca Unauthorized
            if (string.IsNullOrEmpty(userIdFromToken))
			{
				context.Result = new UnauthorizedResult();
				return;
			}

            // Próbuje przekonwertować ID z tokenu na liczbę całkowitą
            if (!int.TryParse(userIdFromToken, out int userId))
			{
				context.Result = new BadRequestResult();
				return;
			}

            // Odczytuje ID użytkownika z danych ścieżki URL
            var routeDataUserId = context.RouteData.Values["userId"] as string;

            // Sprawdza, czy ID użytkownika w ścieżce URL jest dostępne
            if (routeDataUserId == null)
			{
				context.Result = new BadRequestResult();
				return;
			}

            // Próbuje przekonwertować ID użytkownika z URL na liczbę całkowitą
            if (!int.TryParse(routeDataUserId, out var requestedUserId))
			{
                context.Result = new BadRequestResult();
                return;
            }

            // Porównuje ID użytkownika z tokenu z ID w URL
            if (userId != requestedUserId)
			{
				context.Result = new ForbidResult();
			}
		}
	}
}
