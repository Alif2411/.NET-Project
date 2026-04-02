using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        UserServices service;
        public AuthController(UserServices service) {
            this.service = service;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDTO user)
        {
            try
            {
                var res = service.Add(user);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO login) {
            try
            {
                var user = service.Login(login.Username, login.Password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
