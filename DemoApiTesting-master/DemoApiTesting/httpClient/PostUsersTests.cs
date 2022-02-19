﻿using System;
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
                      
            var request = new PostUser()
            {
                Id = 0,
                Email = "lenar@mail.ru",
                First_name = "Mortherus",
                Last_name = "Gaynullin",
                Avatar = "https://vk.com/img/faces/111-image.jpg"
            };

            var response = await client.GetAsync(baseAddress, new CancellationToken());
            var stringResponse = await response.Content.ReadAsStringAsync();
            responsePostUsers = JsonConvert.DeserializeObject<ResponsePostUsers>(stringResponse);
            

            var statusCode = response.StatusCode;
                       
            if (
                response.StatusCode == HttpStatusCode.Created &&
                responsePostUsers.Email == "lenar@mail.ru" &&
                responsePostUsers.FirstName == "Mortherus" &&
                responsePostUsers.LastName == "Gaynullin"&&
                responsePostUsers.Avatar == "https://vk.com/img/faces/111-image.jpg"
                )
            {
                Assert.Pass("Статус 201 Created. {Api} отработала корректно, ответ соответсвует запросу");
            }
            else
            {
                Assert.Fail(message: $"{Api} отработала некорректно");
            }
            
                       
        }
    }
}