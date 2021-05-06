using Explora.ApiClient;
using Explora.ApiClient.Configurations;
using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explora.Synchronization.TestConsole
{
    public class A
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var cad = "[{\"id\":17,\"url\":\"api/files/2/file-data\",\"deleted\":true,\"name\":\"cerroplomo\",\"extension\":null,\"lastTimestamp\":\"2021-03-05T01:29:56\",\"version\":1,\"platform\":2,\"description\":\"description\"}]";
            var obj = JsonConvert.DeserializeObject<IEnumerable<object>>(cad);
            var items = new List<FileDto>(); 
            foreach (JObject item in obj)
            {
                var dto = new FileDto();
                dto.Id = item.Value<int>("id");
                dto.Name = item.Value<string>("name");
                dto.Url = item.Value<string>("url");
                dto.Deleted = item.Value<bool>("deleted");
                dto.Extension = item.Value<string>("extension");
                dto.LastTimestamp = DateTime.Parse(item.Value<string>("lastTimestamp"));
                dto.Version = item.Value<int>("version");
                dto.Description = item.Value<string>("description");
                dto.Platform = (Platform)item.Value<int>("platform");
                items.Add(dto);
            }



            var serverConfig = new ExploraApiConfig
            {
                ServerUrl = "http://159.65.254.176:14500",
                //ServerUrl = "http://localhost:5000",
                GetAllPath = "/api/files",
                //IdentityUrl = "http://localhost:10500"
                IdentityUrl = "http://159.65.254.176:10500"
            };

            var exploraClient = new ExploraClient()
            {
                Configuration = serverConfig
            };

            var dataManagerService = new ExploraApplicationDataManager();

            var synchManager = new ExploraSynchronization(dataManagerService, exploraClient);

            var files = synchManager.StartSynchronization(Platform.Android);

            var blobDto = exploraClient.DownloadBlobsData(files.First());

            dataManagerService.SaveBlobData(blobDto);
        }
    }
}
