using Application.TaskManagement.Commands.CreateTask;
using Application.TaskManagement.Commands.DeleteTask;
using Application.TaskManagement.Commands.UpdateTask;
using Application.TaskManagement.Queries.GetTask;
using Application.TaskManagement.Queries.GetTasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : BaseApiController
    {
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllAsync(int? page, int? pageSize)
        {
            var request = new GetTasksRequest() { Page = page, PageSize = pageSize };

            return Ok(await this.Mediator.Send(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAsync(Guid id)
        {
            var request = new GetTaskRequest() { TaskId = id };

            return Ok(await this.Mediator.Send(request));
        }

        [Authorize(Policy = "TaskCreate")]
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateAsync(CreateTask command)
        {
            _ = await this.Mediator.Send(command);

            return StatusCode(201);
        }

        [Authorize(Policy ="TaskUpdate")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync(UpdateTask command)
        {
            _ = await this.Mediator.Send(command);

            return Ok();
        }

        [Authorize(Policy = "TaslDelete")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteTask() { TaskId = id };

            _ = await this.Mediator.Send(command);

            return this.Ok();
        }
    }
}
