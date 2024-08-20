using Application.TemplateTodo.Commands.CreateTodo;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TemplateTodoController : ApiControllerBase
    {
        [HttpPost("[Action]")]
        public async Task<ActionResult<int>> Create(CreateTemplateTodoCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
