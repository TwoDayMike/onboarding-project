using Application.TemplateAuthentication.Commands.LoginTemplate;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TemplateAuthenticationController : ApiControllerBase
    {
        [HttpPost("[Action]")]
        public async Task<ActionResult<string>> Login(LoginTemplateCommand command) => await Mediator.Send(command); 
    }
}
