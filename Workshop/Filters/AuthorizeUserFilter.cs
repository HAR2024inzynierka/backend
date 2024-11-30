using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Workshop.Filters
{
	public class AuthorizeUserFilter : IAuthorizationFilter
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthorizeUserFilter(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var httpContext = _httpContextAccessor.HttpContext;

			if (httpContext == null) {
				context.Result = new UnauthorizedResult();
				return;
			}

			// Извлекаем userId из токена
			var userIdFromToken = httpContext.User
				.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userIdFromToken))
			{
				context.Result = new UnauthorizedResult();
				return;
			}

			if(!int.TryParse(userIdFromToken, out int userId))
			{
				context.Result = new BadRequestResult();
				return;
			}


			// Извлекаем userId из параметра в URL (например, {id})
			var routeDataUserId = context.RouteData.Values["userId"] as string;

			if (routeDataUserId == null)
			{
				context.Result = new BadRequestResult();
				return;
			}

			if (!int.TryParse(routeDataUserId, out var requestedUserId))
			{
                context.Result = new BadRequestResult();
                return;
            }

			// Проверяем, что userId из токена совпадает с переданным id
			if (userId != requestedUserId)
			{
				context.Result = new ForbidResult();
			}
		}
	}
}
