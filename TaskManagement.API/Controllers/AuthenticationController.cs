using Application.IdentityManagement.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] CreateAuthenticationTokenRequest request)
        {
            var result = await this.Mediator.Send(request);

            return !result.Success ? this.StatusCode(401) : this.StatusCode(200, result);
        }
    }
}
