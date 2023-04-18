using Application.UserManagement.Commands.AddUserToRole;
using Application.UserManagement.Commands.CreateUser;
using Application.UserManagement.Commands.DeleteUser;
using Application.UserManagement.Commands.UpdateUser;
using Application.UserManagement.Queries.GetUser;
using Application.UserManagement.Queries.GetUserRoles;
using Application.UserManagement.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllAsync(int? page, int? pageSize)
        {
            var request = new GetUsersRequest() { Page = page, PageSize = pageSize };

            return Ok(await this.Mediator.Send(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAsync(Guid id)
        {
            var request = new GetUserRequest() { UserId = id };

            return Ok(await this.Mediator.Send(request));
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateAsync(CreateUser command)
        {
            _ = await this.Mediator.Send(command);

            return StatusCode(201);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync(UpdateUser command)
        {
            _ = await this.Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteUser() { UserId = id };

            _ = await this.Mediator.Send(command);

            return Ok();
        }

        [HttpPost("{userId}/{roleId}")]
        public async Task<ActionResult> AddUserToRole(Guid userId, Guid roleId)
        {
            var command = new AddUserToRole(userId,roleId);

            _ = await this.Mediator.Send(command);

            return StatusCode(201);
        }

        [HttpGet("{userId}/Roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUserRoles(Guid userId)
        {
            var request = new GetUserRolesRequest() { UserId=userId };

            return Ok(await this.Mediator.Send(request));
        }
    }
}
