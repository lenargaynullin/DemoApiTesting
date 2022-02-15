using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace DemoApiTesting.ContractTests
{
    public class GetPlanetsContractTests : ContractBase
    {
        private const string Host = "https://swapi.dev/api";

        [Test]
        [TestCase(0)]
      

        // Негативное тестирование https://swapi.dev/api/planets/?page=0
        public async Task CheckContractGetPlanetNegativeTesting(int page)
        {
	        string Api = $"/planets/?page={page}";
            
            // Создаем HTTP клиент для отправления запроса, получения ответа
            var client = new HttpClient() ;
            
            // Ответ от сервера
            var response = await client.GetAsync(new Uri(Host + Api), new CancellationToken());

            

            // Строковый ответ от сервера 
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Присваиваем переменной Json-схема schema = Загрузить Json-схему из строки которая содержит Схему Json
            JSchema schema = JSchema.Parse(GetFileAsString("getPlanets.Positive.json"));

            // Преобразуем наш файл getUsers.Negative.json в формат JSchema (для этого мы сначала считаем файл в строку)
            // Документацию по JSON Schema Validation можно посмотреть тут:
            // https://json-schema.org/draft/2019-09/json-schema-validation.html
            JSchema schema = JSchema.Parse(File.ReadAllText($@"{path}" + "getPlanets.Negative.json"));

            

            //{
            //    var direct = Directory.GetCurrentDirectory();
            //    var path = direct.Substring(0, direct.IndexOf(@"\bin\", StringComparison.Ordinal));
            //    var fullPath = path + @"\ContractTests\contracts\";
            //    return File.ReadAllText(fullPath + getPlanets.Negative.json);

            //}


            // Сравнить ответ с файлом Json схема
            await CheckValidationResponseMessageBySchemaAsync(response, schema);
            var a = 3;
            // {
            //    JObject jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
            //    bool result = jObject.IsValid(schema, out IList<string> msg);
            //    Assert.IsTrue(result, "некорректные поля: " + string.Join(" ,", msg.ToArray()));
            // }           
        }
		        
    }
}