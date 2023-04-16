using Application.RoleManagement.Commands.CreateRole;
using Application.RoleManagement.Commands.DeleteRole;
using Application.RoleManagement.Commands.UpdateRole;
using Application.RoleManagement.Queries.GetRole;
using Application.RoleManagement.Queries.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseApiController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllAsync(int? page, int? pageSize)
        {
            var request = new GetRolesRequest() { Page = page, PageSize = pageSize };

            return Ok(await this.Mediator.Send(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAsync(Guid id)
        {
            var request = new GetRoleRequest() { RoleId = id };

            return Ok(await this.Mediator.Send(request));
        }
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateAsync(CreateRole command)
        {
            _ = await this.Mediator.Send(command);

            return StatusCode(201);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync(UpdateRole command)
        {
            _ = await this.Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteRole() { RoleId=id };

            _ = await this.Mediator.Send(command);

            return this.Ok();
        }
    }
}
