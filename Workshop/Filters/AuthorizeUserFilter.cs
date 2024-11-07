using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Linq;

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

			// Извлекаем userId из токена
			var userIdFromToken = _httpContextAccessor.HttpContext.User
				.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userIdFromToken))
			{
				context.Result = new UnauthorizedResult();
				return;
			}

			var userId = int.Parse(userIdFromToken);

			// Извлекаем userId из параметра в URL (например, {id})
			var routeDataUserId = context.RouteData.Values["userId"] as string;

			if (routeDataUserId == null)
			{
				context.Result = new BadRequestResult();
				return;
			}

			var requestedUserId = int.Parse(routeDataUserId);

			// Проверяем, что userId из токена совпадает с переданным id
			if (userId != requestedUserId)
			{
				context.Result = new ForbidResult();
			}
		}
	}
}
