using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DemoApiTesting.models;
using NUnit.Framework;

namespace DemoApiTesting.httpClient
{
    public class PostPetTests
    {
        private const string Host = "https://petstore.swagger.io/v2";
        private const string Api = "/pet";

        [Test]
        public async Task CheckPostPetTesting()
        {
            var baseAddress = Host + Api;
            var client = new HttpClient();
            var strBody = "{\"id\": 0,\"category\": {\"id\": 0,\"name\": \"string\"},\"name\": \"doggie\"," +
                          "\"photoUrls\": [\"string\"],\"tags\": [{\"id\": 0,\"name\": \"string\"}],\"statu\": \"available\"}";
            var contentBody = new StringContent(strBody, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(baseAddress, contentBody);

            var statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode, $"Ответ от api {Api} не соответствует ожидаемому");
        }
        
        [Test]
        public async Task CheckPostPetByJsonTesting()
        {
            var baseAddress = Host + Api;
            var client = new HttpClient();
            var request = new PetRequest()
            {
                Id = 0,
                Category = new IdName() { Id = 0, Name = "ololo" },
                Name = "olololo",
                PhotoUrls = new string[] {"string", "343434"},
                Tags = new IdName[]
                {
                    new IdName() { Id = 1, Name = "name" },
                    new IdName() { Id = 2, Name = "name2" }
                }
            };

            var response = await client.PostAsJsonAsync(baseAddress, request);

            var statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode, $"Ответ от api {Api} не соответствует ожидаемому");
        }
    }
}