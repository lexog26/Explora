using Explora.ApiClient.Configurations;
using Explora.ApiClient.Interfaces;
using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Enums;
//using IdentityModel.Client;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Explora.ApiClient
{
    public class ExploraClient : IExploraClient
    {
        /// <summary>
        /// Creates an ExploraApi client
        /// </summary>
        /// <param name="applicationRootPath">Folder where the application files are present</param>
        public ExploraClient()
        {
            InitializeApiConfiguration();
        }

        public ExploraApiConfig Configuration { get; set; }

        public BlobDto DownloadBlobsData(FileDto fileDto)
        {
            //var accessToken = GetApiAccessTokenAsync();
            using (var httpClient = new HttpClient())
            {
                //httpClient.SetBearerToken(accessToken);
                httpClient.BaseAddress = new Uri(Configuration.ServerUrl);
                var request = new HttpRequestMessage(HttpMethod.Get, fileDto.Url);
                var response = httpClient.SendAsync(request).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var stream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
                    return new BlobDto
                    {
                        Name = fileDto.Name,
                        Extension = fileDto.Extension,
                        Data = stream
                    };
                }
                return null;
            }
        }

        public IEnumerable<FileDto> GetFiles(Platform platform, DateTime lastTimeStamp)
        {
            //Get access token
            //var accessToken = GetApiAccessTokenAsync();

            using (var httpClient = new HttpClient())
            {
                //httpClient.SetBearerToken(accessToken);
                httpClient.BaseAddress = new Uri(Configuration.ServerUrl);
                var path = GetByPlatformStampPath(platform, lastTimeStamp);
                var request = new HttpRequestMessage(HttpMethod.Get, path);
                var response = httpClient.SendAsync(request).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var resultStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    IEnumerable<FileDto> result = null; //JsonConvert.DeserializeObject<IEnumerable<FileDto>>(resultStr);
                    /*foreach (var item in result)
                    {
                        item.Url = Configuration.ServerUrl + "/" + item.Url;
                    }*/
                    return result;
                }
                return null;
            }
        }

        protected virtual void InitializeApiConfiguration()
        {
            Configuration = new ExploraApiConfig
            {
                ServerUrl = "https://localhost:5001",
                GetAllPath = "/api/files",
            };
        }

        protected virtual string GetApiAccessTokenAsync()
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                client.BaseAddress = new Uri(Configuration.IdentityUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                                  new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "client_credentials"),
                        new KeyValuePair<string, string>("client_id", Configuration.ApiClient.ClientId),
                        new KeyValuePair<string, string>("client_secret", Configuration.ApiClient.ClientSecret),
                        new KeyValuePair<string, string>("scope", Configuration.ApiScope)
                    }
                );

                var response = client.PostAsync("connect/token", formContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    /*var responseJson = response.Content.ReadAsStringAsync().Result;
                    var jObject = JObject.Parse(responseJson);
                    return jObject.GetValue("access_token").ToString();*/
                }
                return null;
            }
        }

        protected virtual string GetByPlatformStampPath(Platform platform, DateTime dateTime)
        {
            return Configuration.GetAllPath + $"/platform/{platform}?lastTimeStamp={dateTime}";
        }
    }
}
