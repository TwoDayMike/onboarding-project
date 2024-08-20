using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Application.Common.Interfaces;
using Application.Common.Security.Attributes;
using MediatR;
using System.ComponentModel;
using System.Diagnostics;

namespace Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuthorizationService _identityService;


        public AuthorizationBehaviour(
            ICurrentUserService currentUserService,
            IAuthorizationService identityService)
        {
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var attributeCollection = TypeDescriptor.GetAttributes(request);
            var authAttributes = new List<Attribute>();

            foreach (var attribute in attributeCollection)
            {
                if (attribute != null && attribute.GetType().GetInterfaces().Contains(typeof(IAuthAttribute)))
                    authAttributes.Add((Attribute)attribute);
            }

            //Command or query is missing an AuthorizationAttribute attribute, please add one of the following:
            //AuthorizeAttribute - Specifies that the command or query that this attribute is applied to does require authorization.
            //TODOAuthorizeAttribute - Specifies that the command or query that this attribute is applied to require authorization at a later stage.
            //AllowAnonymous - Specifies that the command or query that this attribute is applied to does require authorization.
            if (!authAttributes.Any())
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
                throw new Exception("Missing AuthorizationAttribute on Command");
            }

            var authorizeAttributes = authAttributes.Where(x => x.GetType() == typeof(AuthorizeAttribute)).Select(x => x as AuthorizeAttribute);
            if (authorizeAttributes.Any())
            {
                //Must be authenticated user
                if (_currentUserService.UserId == null)
                {
                    throw new UnauthorizedAccessException();
                }
                var specificAttributes = authorizeAttributes.Where(a => a != null && (a.Roles.Any() || a.Scopes.Any()));
                if (specificAttributes.Any())
                {
                    var authorized = specificAttributes.All(x => x != null && (x.EvaluateAll ?
                     x.Roles.All(r => _identityService.IsInRole(r)) && x.Scopes.All(s => _identityService.HasScope(s)) :
                      x.Roles.Any(r => _identityService.IsInRole(r)) || x.Scopes.Any(s => _identityService.HasScope(s))));
                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }
            }
            return await next();
        }
    }
}