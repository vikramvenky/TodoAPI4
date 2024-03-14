using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Uuid2Controller : ControllerBase
    {
        // Static list to store generated UUIDs
        private static List<Guid> uuidList = new List<Guid>();

        [HttpGet]
        public IActionResult GetUUID()
        {
            if (uuidList.Count > 0)
            {
                Guid uuid = uuidList[0];
                uuidList.RemoveAt(0);
                return Ok(uuid);
            }
            else
            {
                Guid newUUID = Guid.NewGuid();
                return Ok(newUUID);
            }
        }



        [HttpPost("add")]
        public IActionResult AddUUID()
        {
            Guid newUUID = Guid.NewGuid();
            uuidList.Add(newUUID);
            return Ok("UUID added to the list.");
        }
    }
}

