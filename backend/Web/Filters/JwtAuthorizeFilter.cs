using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Web.Filters
{
    public class JwtAuthorizeFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var jwtProvider = context.HttpContext.RequestServices.GetRequiredService<IJwtProvider>();

            var hasAuthorizeAttribute = context.ActionDescriptor.EndpointMetadata.Any(x => x is AuthorizeAttribute);
            if (hasAuthorizeAttribute)
            {
                var token = context.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];

                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        var claimsPrincipal = jwtProvider.Validate(token);

                        var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        context.HttpContext.Items["userId"] = userId;
                    }
                    catch
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
            }
        }
    }
}
