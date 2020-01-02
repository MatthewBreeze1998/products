using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloud_System_dev_ops.Models;
using Cloud_System_dev_ops.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cloud_System_dev_ops.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var name = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var displayName = name ?? userId;

            var rand = new Random().Next();
            var value = new GetValueDto
            {
                Number = rand,
                Message = string.IsNullOrEmpty(displayName) ? $"Your result {rand}" : $"{displayName}, your personal information"

            };


            return Ok(value);
        }
    }
}