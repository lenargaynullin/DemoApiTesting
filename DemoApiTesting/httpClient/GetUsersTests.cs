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

            // Ответ от сервера
            var response = await client.GetAsync(baseAddress, new CancellationToken());

            // Строковый ответ от сервера 
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Преобразуем содержимое ответа апи в строку
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Assert.Fail($"{Api} отработала некорректно, дальнейшие проверки бессмысленны!");
            }

            //Находим путь к файлу
            var direct = Directory.GetCurrentDirectory();

            // Убираем из этого пути лишнее (все,что после bin) и заменяем его на relativePath
            var path = direct.Substring(0, direct.IndexOf(@"\bin\", StringComparison.Ordinal)) + @"\contracts\";

            // Преобразуем наш файл getUsers.Negative.json в формат JSchema (для этого мы сначала считаем файл в строку)
            // Документацию по JSON Schema Validation можно посмотреть тут:
            // https://json-schema.org/draft/2019-09/json-schema-validation.html
            // JSchema schema = JSchema.Parse(File.ReadAllText($@"{path}" + "getPlanets.Negative.json"));



            // преобразуем стринговый ответ от апи в JObject
            var jObject = JObject.Parse(stringResponse);
            if (jObject == null) throw new ArgumentNullException(nameof(jObject));

            // Вывод в консоль
            Console.WriteLine("Файл getPlanets.Negative.json:");
            Console.WriteLine(schema);

            Console.WriteLine();
            Console.WriteLine("Ответ сервера:");
            Console.WriteLine(jObject);

            IList<string> messages;

            // метод IsValid проверяет соответствует ли ответ апи (jObject) нашему контрактному файлу getUsers.Negative.json (schema)
            bool valid = jObject.IsValid(schema, out messages);

            Console.WriteLine(valid);

            var Name = jObject["name"]; ;


            //Проверяем, что статус ответа API == ОК 
            // Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "полученный code некорректен");

            Assert.IsTrue(valid, $"Полученный json невалиден. Невалидные поля {string.Join(", ", messages.ToArray())}");
        }
    }

}