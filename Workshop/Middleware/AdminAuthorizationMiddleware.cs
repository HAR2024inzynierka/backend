using System.Security.Claims;

namespace Workshop.Middleware
{
    /// <summary>
    /// Middleware do autoryzacji roli administratora.
    /// Sprawdza, czy użytkownik posiada rolę administratora (reprezentowaną jako wartość "1" w roli).
    /// Jeśli użytkownik nie ma uprawnień administratora, zwraca status 403 (Forbidden) i odpowiednią wiadomość.
    /// </summary>
    public class AdminAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="AdminAuthorizationMiddleware"/>.
        /// </summary>
        /// <param name="next">Delegat do następnego middleware w łańcuchu.</param>
        public AdminAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Sprawdza, czy użytkownik ma rolę administratora i przekazuje żądanie do następnego middleware.
        /// W przypadku braku uprawnień administratora, zwraca status 403 (Forbidden).
        /// </summary>
        /// <param name="context">Obiekt zawierający dane żądania.</param>
        public async Task Invoke(HttpContext context)
        {
            // Pobiera rolę użytkownika z tokenu (ClaimTypes.Role)
            var userRole = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            // Jeśli użytkownik ma rolę "1" (admin), przekazuje żądanie do następnego middleware
            if (userRole == "1")
            {
                await _next(context);
            }
            // Jeśli użytkownik nie ma roli administratora, zwraca status 403 i komunikat o braku dostępu
            else
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied. Admin only.");
            }
        }
    }
}
