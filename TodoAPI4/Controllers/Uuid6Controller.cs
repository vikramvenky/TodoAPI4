using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UUID6Controller : ControllerBase
    {
        private static List<Guid> uuidList = new List<Guid>();
        private static readonly object lockObject = new object();

        [HttpGet]
        public IActionResult GetUUID()
        {
            lock (lockObject)
            {
                if (uuidList.Count == 0)
                {
                    RetrieveUUIDsFromAPI();
                }

                if (uuidList.Count > 0)
                {
                    Guid uuid = uuidList[0];
                    uuidList.RemoveAt(0);
                    return Ok(uuid);
                }
                else
                {
                    return BadRequest("No UUIDs available at the moment.");
                }
            }
        }

        private void RetrieveUUIDsFromAPI()
        {
            Thread.Sleep(5000);
            for (int i = 0; i < 10; i++)
            {
                uuidList.Add(Guid.NewGuid());
            }
        }
    }
}
