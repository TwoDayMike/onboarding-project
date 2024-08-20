using Application.Common.Interfaces;
using Application.Common.Security;

namespace Web.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsInRole(Role role)
        {
            throw new NotImplementedException();
        }

        public bool HasScope(Scope scope)
        {
            throw new NotImplementedException();
        }
    }
}
