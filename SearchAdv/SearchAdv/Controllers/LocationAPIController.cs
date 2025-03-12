using Microsoft.AspNetCore.Mvc;
using SearchAdv.Model;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace SearchAdv.Controllers
{
    [ApiController]
    public class LocationAPIController : ControllerBase
    {
        public LocationAPIController()
        {

        }

        [HttpGet]
        [Route("[controller]/GetLocations")]
        public async Task<List<AdvLocation>> GetLocations()
        {
            MemoryStorage memoryStorage = new MemoryStorage();
            var memItems = memoryStorage.DataFromFileToMemory;
            memItems.Seek(0, SeekOrigin.Begin);
            List<AdvLocation> advLocations = await JsonSerializer.DeserializeAsync<List<AdvLocation>>(memItems);
            return advLocations;
        }
    }
}
