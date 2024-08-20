using Application.Common.Security.Enums;

namespace Application.Common.Interfaces
{
    public interface IAuthorizationService
    {

        bool IsInRole(Role role);

        bool HasScope(Scope scope1);
    }
}