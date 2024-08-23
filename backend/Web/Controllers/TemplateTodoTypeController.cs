using Application.TemplateTodoTypes.Commands.CreateTemplateTodoType;
using Application.TemplateTodoTypes.Commands.DeleteTemplateTodoTypeById;
using Application.TemplateTodoTypes.Queries.GetTemplateTodoTypes;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TemplateTodoTypeController : ApiControllerBase
    {
        [HttpPost("[Action]")]
        public async Task<ActionResult<int>> Create(CreateTemplateTodoTypeCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("[Action]")]
        public async Task<ActionResult> Delete(DeleteTemplateTodoTypeByIdCommand command)
        {
            await Mediator.Send(command);
            return NotFound();
        }

        [HttpGet("[Action]")]
        public async Task<ActionResult<List<TodoTypeExampleDTO>>> Get([FromQuery]GetTemplateTodoTypeQuery query)
        {
            return await Mediator.Send(query);
        }

    }
}
