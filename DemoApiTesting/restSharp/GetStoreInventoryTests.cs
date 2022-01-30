using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace DemoApiTesting.restSharp
{
    public class GetStoreInventoryTests
    {
        
        private const string Host = "https://petstore.swagger.io/v2";
        private const string Api = "/pet";
        
        [Test]
        public async Task CheckStoryInventory()
        {
            var restClient = new RestClient(Host);
            
            RestRequest request = new RestRequest(Api, Method.Get);
            var response = await restClient.ExecuteAsync(request);
            var r = JObject.Parse(response.Content);
            //Проверяем, что статус ответа API == ОК 
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "полученный code некорректен");
        }
    }
}