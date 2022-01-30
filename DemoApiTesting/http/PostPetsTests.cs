using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DemoApiTesting
{
    public class PostPetsTests
    {

        private const string Host = "https://petstore.swagger.io/v2";
        private const string Api = "/pet";
        
        [OneTimeSetUp]
        public async Task Setup()
        {
            var baseAddress = new Uri(Host + Api);
            var client = new HttpClient() { BaseAddress = baseAddress } ;
            var requestStr =
                "{\"id\": 0,\"category\": {\"id\": 0,\"name\": \"string\"},\"name\": \"doggie\",\"photoUrls\": [\"string\"],\"tags\": " +
                "[{\"id\": 0,\"name\": \"string\"}],\"status\": \"available\"}";

            var request = new PostPet()
            {
                Id = 0,
                Category = new Category() {Id = 0, Name = "ololo"},
                Name = "flaffy",
                PhotoUrls = new string[] {"string"},
                Status = "free", Tags = new Category[] {new Category() {Id = 0, Name = "ololo"}}
            };
            StringContent strCont = new StringContent(requestStr, Encoding.UTF8, "application/json");
            var response1 = await client.PostAsync(baseAddress, strCont);
            
            var response = await client.PostAsJsonAsync(baseAddress, request);

            var stringResponse = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Assert.Fail($"{Api} отработала некорректно, дальнейшие проверки бессмысленны!");
            }
     //       responsePlanets = JsonConvert.DeserializeObject<ResponsePlanets>(stringResponse);
        }

        [Test]
        public void CheckCountFromPlanetsApiTesting()
        {
        //    Assert.AreEqual(60, responsePlanets.Count, "Количество планет не совпадает");
        }
        
  
    }
}