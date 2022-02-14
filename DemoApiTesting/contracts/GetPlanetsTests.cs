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

namespace DemoApiTesting
{
    public class GetPlanetsContractTests
    {

        private const string Host = "https://swapi.dev/api";
        
        private ResponsePlanets responsePlanets;
        

        [Test]
        [TestCase(1)]
        
       
        public async Task CheckContractPlanetsApiTesting(int page)
        {
            string Api = $"/planets/?page={page}";
            var baseAddress = new Uri(Host + Api);
            var client = new HttpClient() { BaseAddress = baseAddress }; // https://swapi.dev/api/planets/?page=

            // Ответ от сервера
            var response = await client.GetAsync(baseAddress, new CancellationToken());

            // Строковый ответ от сервера 
            var stringResponse = await response.Content.ReadAsStringAsync();
            
            // преобразуем содержимое ответа апи в строку
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
            JSchema schema = JSchema.Parse(File.ReadAllText($@"{path}"+"getPlanets.Negative.json"));

            // преобразуем стринговый ответ от апи в JObject
            var jObject = JObject.Parse(stringResponse);
            if (jObject == null) throw new ArgumentNullException(nameof(jObject));
             
            // метод IsValid проверяет соответствует ли ответ апи (jObject) нашему контрактному файлу getUsers.Negative.json (schema)
            var valid = jObject.IsValid(schema, out IList<string> messages);
            
            Assert.IsTrue(valid, $"Полученный json невалиден. Невалидные поля {string.Join(", ", messages.ToArray())}");
        }
        
    }
}