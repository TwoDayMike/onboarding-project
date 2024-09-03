using Application.TemplateTodo.Commands.CreateTodo;
using Application.TemplateTodo.Commands.DeleteTemplateTodoById;
using Application.TemplateTodo.Commands.UpdateTemplateTodoAssignee;
using Application.TemplateTodo.Commands.UpdateTemplateTodoName;
using Application.TemplateTodo.Queries.GetTemplateTodo;
using Microsoft.AspNetCore.Authorization;
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

        [HttpDelete("[Action]")]
        public async Task<ActionResult> DeleteTodoById([FromQuery] DeleteTemplateTodoByIdCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("[Action]")]
        public async Task<ActionResult> Update(UpdateTemplateTodoIsCompleteCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPut("[Action]"), Authorize]
        public async Task<ActionResult> AssigneTodo(UpdateTemplateTodoAssigneeCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
        [HttpGet("[Action]")]
        public async Task<ActionResult<List<TodoExampleDTO>>> Get([FromQuery]GetTemplateTodoQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
