#nullable disable
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => this.mediator ?? this.HttpContext.RequestServices.GetService<IMediator>();
    }
}
