using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;
using DemoApiTesting.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace DemoApiTesting.httpClient
{
    public class PostUserTests
    {
        private const string Host = "https://reqres.in/api";
        private const string Api = "/users";
        private ResponsePostUsers responsePostUsers;
        /*
        [Test]
        public async Task CheckPostUsersTesting()
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
        */
              
        [Test]
        public async Task CheckPostUserByJsonTesting()
        {
            var baseAddress = Host + Api;
            var client = new HttpClient();
                      
            var request = new UserRequest()
            {
                id = 0,
                email = "lenar@mail.ru",
                first_name = "Mortherus",
                last_name = "Gaynullin",
                avatar = "https://vk.com/img/faces/111-image.jpg"
            };

            var response = await client.PostAsJsonAsync(baseAddress, request);


            JObject jObject = JObject.Parse(await response.Content.ReadAsStringAsync());

            var statusCode = response.StatusCode;

            string stringSchema = @"{'id':""33"",'email':""int"",'first_name':""mortheus"",'last_name':""Gaynullin"",'avatar':""https://reqres.in/img/faces/1-image.jpg"",'createdAt':""2022-02-23T12:14:58.278Z""}";
            JSchema schema = JSchema.Parse(stringSchema);

            
            bool result = jObject.IsValid(schema, out IList<string> msg);
            Assert.IsTrue(result, "некорректные поля: " + string.Join(" ,", msg.ToArray()));
        }
    }
}