using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleServices roleServices;

        public RoleController(RoleServices roleServices)
        {
            this.roleServices = roleServices;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = roleServices.Get();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var data = roleServices.Get(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(RoleDTO role)
        {
            try
            {
                if (roleServices.Add(role))
                {
                    return Ok(new { Message = "Role Created Successfully" });
                }
                return BadRequest(new { Message = "Failed to create role" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
