using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Phlox_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhloxController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Get()
        {
            return Ok("Hello World!");
        }
    }
}