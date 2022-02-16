using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using NJsonSchema;

namespace DemoApiTesting
{
    public class GetUsersTests
    {

        private const string Host = "https://reqres.in/api";

        private ResponsePlanets responsePlanets;
        private static IEnumerable<object> jObject;

        [Test]
        [TestCase(0)]


        public async Task CheckContractPlanetsApiTesting(int page)
        {
            string Api = $"/users/?page={page}";
            var baseAddress = new Uri(Host + Api);
            var client = new HttpClient() { BaseAddress = baseAddress }; 

            // ����� �� �������
            var response = await client.GetAsync(baseAddress, new CancellationToken());

            // ��������� ����� �� ������� 
            var stringResponse = await response.Content.ReadAsStringAsync();

            // ����������� ���������� ������ ��� � ������
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Assert.Fail($"{Api} ���������� �����������, ���������� �������� ������������!");
            }

            //������� ���� � �����
            var direct = Directory.GetCurrentDirectory();

            // ������� �� ����� ���� ������ (���,��� ����� bin) � �������� ��� �� relativePath
            var path = direct.Substring(0, direct.IndexOf(@"\bin\", StringComparison.Ordinal)) + @"\contracts\";

            // ����������� ��� ���� getUsers.Negative.json � ������ JSchema (��� ����� �� ������� ������� ���� � ������)
            // ������������ �� JSON Schema Validation ����� ���������� ���:
            // https://json-schema.org/draft/2019-09/json-schema-validation.html
            // JSchema schema = JSchema.Parse(File.ReadAllText($@"{path}" + "getPlanets.Negative.json"));



            // ����������� ���������� ����� �� ��� � JObject
            var jObject = JObject.Parse(stringResponse);
            if (jObject == null) throw new ArgumentNullException(nameof(jObject));

            // ����� � �������
            Console.WriteLine("���� getPlanets.Negative.json:");
            Console.WriteLine(schema);

            Console.WriteLine();
            Console.WriteLine("����� �������:");
            Console.WriteLine(jObject);

            IList<string> messages;

            // ����� IsValid ��������� ������������� �� ����� ��� (jObject) ������ ������������ ����� getUsers.Negative.json (schema)
            bool valid = jObject.IsValid(schema, out messages);

            Console.WriteLine(valid);

            var Name = jObject["name"]; ;


            //���������, ��� ������ ������ API == �� 
            // Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "���������� code �����������");

            Assert.IsTrue(valid, $"���������� json ���������. ���������� ���� {string.Join(", ", messages.ToArray())}");
        }
    }

}