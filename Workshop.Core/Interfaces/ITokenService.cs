﻿using Microsoft.AspNetCore.Http;
using Workshop.Core.Entities; 

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs odpowiedzialny za generowanie tokenów.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generowanie tokenu JWT na podstawie danych użytkownika.
        /// </summary>
        /// <param name="user">Obiekt użytkownika, dla którego generowany jest token.</param>
        /// <returns>Token JWT w postaci ciągu znaków.</returns>
        string GenerateJwtToken(User user);
        int GetUserIdFromToken(HttpContext httpContext);
    }
}
