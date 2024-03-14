using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UUID5Controller : ControllerBase
    {
        private static readonly object lockObject = new object();
        private static readonly List<Guid> uuidList = new List<Guid>();
        private static readonly HashSet<Guid> returnedUUIDs = new HashSet<Guid>();

        private static readonly HttpClient httpClient = new HttpClient();

        private const int Threshold = 10;

        [HttpGet]
        public async Task<IActionResult> GetUUID()
        {
            if (uuidList.Count < Threshold || uuidList.Count == 0)
            {
                await RetrieveUUIDsFromExternalAPI();
            }

            lock (lockObject)
            {
                if (uuidList.Count == 0)
                {
                    return BadRequest("UUIDs not available");
                }
                else
                {
                    Guid existingUUID = uuidList[0];

                    if (returnedUUIDs.Contains(existingUUID))
                    {
                        uuidList.RemoveAt(0);
                        return GetUUID().Result;
                    }
                    else
                    {
                        returnedUUIDs.Add(existingUUID);
                        return Ok(existingUUID);
                    }
                }
            }
        }

        private async Task RetrieveUUIDsFromExternalAPI()
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://api.github.com/");

            if (response.IsSuccessStatusCode)
            {
                List<Guid> receivedUUIDs = await response.Content.ReadAsAsync<List<Guid>>();

                lock (lockObject)
                {
                    uuidList.AddRange(receivedUUIDs);
                }
            }
            else
            {
                throw new Exception("Failed to retrieve UUIDs from external API");
            }
        }
    }
}
