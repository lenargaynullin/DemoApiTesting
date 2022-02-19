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
                Assert.Fail(message: $"{Api} ���������� �����������, ���������� �������� �����������!");
            }
            responseUsers = JsonConvert.DeserializeObject<ResponseGetUsers>(stringResponse);
        }

        [Test]
        public void CheckPageFromUsersApiTesting()
        {
            Assert.AreEqual(1, responseUsers.Page, "�������� �� ���������");
        }

        [Test]
        public void CheckResponseIsNotEmptyFromUsersApiTesting()
        {
            Assert.IsNotNull(responseUsers, "����� �� Api ������ ������ ��������");
        }

        [Test]
        public void CheckDataIsNotEmptyFromUsersApiTesting()
        {
            Assert.IsNotNull(responseUsers.Data, "���� data � ������ �� Api ������� ������ ��������");
        }

        [Test]
        public void CheckIdFromUsersApiTesting()
        {
            Assert.AreEqual(4, responseUsers.Data[3].Id, "���� id � ������ �� Api �� ������������� ����������");
        }
    }
}