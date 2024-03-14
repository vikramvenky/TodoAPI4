using Microsoft.AspNetCore.Mvc;
using System;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UUID1Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUUID()
        {
            Guid uuid = Guid.NewGuid();
            return Ok(uuid);
        }
    }
}
