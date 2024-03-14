using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UUID3Controller : ControllerBase
    {
        private static readonly List<Guid> uuidList = new List<Guid>();
        private static readonly HashSet<Guid> returnedUUIDs = new HashSet<Guid>();

        [HttpGet]
        public IActionResult GetUUID()
        {
            if (uuidList.Count == 0)
            {
                Guid newUUID = Guid.NewGuid();
                uuidList.Add(newUUID);
                return Ok(newUUID);
            }
            else
            {
                Guid existingUUID = uuidList[0];

                if (returnedUUIDs.Contains(existingUUID))
                {
                    uuidList.RemoveAt(0);
                    return GetUUID();
                }
                else
                {
                    returnedUUIDs.Add(existingUUID);
                    return Ok(existingUUID);
                }
            }
        }
    }
}
