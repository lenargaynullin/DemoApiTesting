using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DemoApiTesting
{
    public class GetPlanetsTests
    {

        private const string Host = "https://swapi.dev/api";
        private const string Api = "/planets";
        private ResponsePlanets responsePlanets;
        
        [OneTimeSetUp]
        public async Task Setup()
        {
            var baseAddress = new Uri(Host + Api);
            var client = new HttpClient() { BaseAddress = baseAddress } ;
            var response = await client.GetAsync(baseAddress, new CancellationToken());

            var stringResponse = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Assert.Fail($"{Api} отработала некорректно, дальнейшие проверки бессмысленны!");
            }
            responsePlanets = JsonConvert.DeserializeObject<ResponsePlanets>(stringResponse);
        }

        [Test]
        public void CheckCountFromPlanetsApiTesting()
        {
            Assert.AreEqual(60, responsePlanets.Count, "Количество планет не совпадает");
        }
        
        [Test]
        public void CheckResponseIsNotEmptyFromPlanetsApiTesting()
        {
            Assert.IsNotNull(responsePlanets, "Ответ от api вернул пустое значение");
        }
        
        [Test]
        public void CheckResultsIsNotEmptyFromPlanetsApiTesting()
        {
            Assert.IsNotNull(responsePlanets.Result, "Поле result в ответе от api вернуло пустое значение");
        }
        
        [Test]
        public void CheckNameFromPlanetsApiTesting()
        {
            Assert.AreEqual("Hoth", responsePlanets.Result[3].Name, "Поле name в ответе от api не соответствует ожидаемому");
        }
    }
}