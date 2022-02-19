using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DemoApiTesting.models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DemoApiTesting.httpClient
{
    public class GetUserTests
    {
        private const string Host = "https://reqres.in/api";
        private const string Api = "/users";
        private ResponseGetUsers responseUsers;

        [OneTimeSetUp]
        public async Task Setup()
        {
            var baseAddress = new Uri(Host + Api);
            var client = new HttpClient() {BaseAddress = baseAddress };
            var response = await client.GetAsync(baseAddress, new CancellationToken());

            var stringResponse = await response.Content.ReadAsStringAsync();
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Assert.Fail(message: $"{Api} отработала некорректно, дальнейшие проверки бессмыслены!");
            }
            responseUsers = JsonConvert.DeserializeObject<ResponseGetUsers>(stringResponse);
        }
    }
}