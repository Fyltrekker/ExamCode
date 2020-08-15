using Microsoft.AspNetCore.Mvc;
using ViseoTechnicalTest.RequestResponse.Request;
using ViseoTechnicalTest.RequestResponse.Response;
using ViseoTechnicalTest.Service;

namespace ViseoTechnicalTest.Contoller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private IUsersService usersService;
        public SecurityController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        /// <summary>
        /// Generates a jwt token.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Authenticate")]
        [HttpPost]
        public IActionResult Post([FromBody] AuthenticateRequest request)
        {
            var result = usersService.GenerateToken(new Service.ServiceModels.Request.AuthenticateRequest
            {
                Password = request.Password,
                Username = request.Username
            });

            if (!string.IsNullOrEmpty(result.JWT))
            {
                return Ok(new AuthenticateResponse { token = result.JWT });
            }
            else
            {
                return BadRequest("Invalid Username or Password.");
            }
        }

    }
}
