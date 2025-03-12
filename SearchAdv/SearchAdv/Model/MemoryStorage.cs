using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SearchAdv.Model
{
    public class MemoryStorage
    {
        public MemoryStream DataFromFileToMemory { get; set; }

        public MemoryStorage()
        {
            DataFromFileToMemory = GetStream().GetAwaiter().GetResult();
        }

        private async Task<MemoryStream> GetStream()
        {
            MemoryStream ms = new MemoryStream();
            List<AdvLocation> lAdvLocations = GetListAdvLocation();
            await JsonSerializer.SerializeAsync(ms, lAdvLocations);
            return ms;
        }

        private List<AdvLocation> GetListAdvLocation()
        {
            List<AdvLocation> listAdvLocations = new List<AdvLocation>();
            string fileName = "ListOfLocation.txt";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            var lines = File.ReadAllLines(path).ToList();
            var newListLines = new List<string>();
            if (lines != null && lines.Count > 0)
            {
                foreach (var line in lines)
                {
                    string cleanedString = Regex.Replace(line, @"[\u0000-\u001F]", string.Empty);
                    newListLines.Add(cleanedString);
                }
            }

            foreach(var str in newListLines)
            {
                string[] nameAndLocations = str.Split(':');
                string[] locations = nameAndLocations[1].Split(',');
                for (int i = 0; i < locations.Length; i++)
                {
                        AdvLocation advLocation = new AdvLocation
                        {
                            Name = nameAndLocations[0],
                            Location = locations[i]
                        };
                        listAdvLocations.Add(advLocation);
                }
            }

            return listAdvLocations;
        }

        public byte[] SerializeAdvLocation(AdvLocation advLocation)
        {
            var memoryStream = new MemoryStream();
            using var writer = new BinaryWriter(memoryStream);
            writer.Write(advLocation.Name);
            writer.Write(advLocation.Location);

            return memoryStream.ToArray();
        }
    }
}
