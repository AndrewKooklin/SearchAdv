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
        List<string> listAdvertisingPlatforms = new List<string>();

        public LocationAPIController()
        {

        }

        [HttpGet]
        [Route("/ru")]
        [Route("/ru/svrd/revda")]
        [Route("/ru/svrd/pervik")]
        [Route("/ru/msk")]
        [Route("/ru/permobl")]
        [Route("/ru/chelobl")]
        [Route("/ru/svrd")]
        public async Task<List<string>> GetLocations()
        {
            MemoryStorage memoryStorage = new MemoryStorage();
            var memItems = memoryStorage.DataFromFileToMemory;
            memItems.Seek(0, SeekOrigin.Begin);
            List<AdvLocation> lAdvLocations = 
                await JsonSerializer.DeserializeAsync<List<AdvLocation>>(memItems);
            
            var path = Request.Path.Value;
            List<AdvLocation> newlAdvLocations = 
                lAdvLocations.Distinct().Where(e => e.Location.StartsWith(path)).ToList();
            string name = "";
            foreach (var item in newlAdvLocations)
            {
                if (!item.Name.Equals(name))
                {
                    listAdvertisingPlatforms.Add(item.Name);
                }
                
                name = item.Name;
            }

            if(listAdvertisingPlatforms == null || listAdvertisingPlatforms.Count <= 0)
            {
                listAdvertisingPlatforms.Add("no matches");
            }

            return listAdvertisingPlatforms;

        }
    }
}
