
using Application.TemplateExampleUsers.Commands.TemplateDeleteUserById;
using Application.TemplateExampleUsers.Commands.TemplateUpdateUserById;
using Application.TemplateExampleUsers.Queries.TemplateGetAllUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TemplateUserController : ApiControllerBase
    {
        [HttpGet("[Action]")]
        public async Task<ActionResult<List<TemplateUserDTO>>> Get([FromQuery]TemplateGetAllUsersQuery query)
        {
            return await Mediator.Send(query);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("[Action]")]
        public async Task<ActionResult> Delete([FromQuery] TemplateDeleteUserByIdCommand query)
        {
            await Mediator.Send(query);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[Action]")]
        public async Task<ActionResult> Update(TemplateUpdateUserByIdCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }

    
}
