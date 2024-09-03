using Application.Common.Security.Attributes;
using Application.Common.Security.Enums;
using Application.TemplateAuthentication.Commands.LoginTemplate;
using Application.TemplateAuthentication.Commands.RegisterTemplate;
using Application.TemplateTodo.Queries.GetTemplateTodo;
using Application.TemplateTodo.Queries.GetTemplateTodoByUserId;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Web.Filters;

namespace Web.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class TemplateAuthenticationController : ApiControllerBase
    {
        [HttpPost("[Action]")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(LoginTemplateCommand command) => await Mediator.Send(command);

        [HttpPost("[Action]")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterTemplateCommand command)
        {
            
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [JwtAuthorizeFilter]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "User")]
        public async Task<ActionResult<List<TodoExampleDTO>>> TestEndpoint([FromQuery]GetTemplateTodoByUserIdQuery query)
        {
            var items = HttpContext.Items["userId"].ToString();
            query.UserId = Int32.Parse(items ?? "0");
            return await Mediator.Send(query);

        }
    }
}
