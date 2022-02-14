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
        [TestCase(1)]
      

        // Негативное тестирование https://swapi.dev/api/planets/?page=0
        public async Task CheckContractGetPlanetNegativeTesting(int page)
        {
	        string Api = $"/planets/?page={page}";
            
            // Создаем HTTP клиент для отправления запроса, получения ответа
            var client = new HttpClient() ;
            
            // Переменная response = Отправляем запрос по адресу URI (https://swapi.dev/api/planets/?page={page}
            var response = await client.GetAsync(new Uri(Host + Api), new CancellationToken());

            
            // Присваиваем переменной Json-схема schema = Загрузить Json-схему из строки которая содержит Схему Json
            JSchema schema = JSchema.Parse(GetFileAsString("getPlanets.Negative.json"));

            //{
            //    var direct = Directory.GetCurrentDirectory();
            //    var path = direct.Substring(0, direct.IndexOf(@"\bin\", StringComparison.Ordinal));
            //    var fullPath = path + @"\ContractTests\contracts\";
            //    return File.ReadAllText(fullPath + getPlanets.Negative.json);

            //}


            // Сравнить ответ с файлом Json схема
            await CheckValidationResponseMessageBySchemaAsync(response, schema);

            // {
            //    JObject jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
            //    bool result = jObject.IsValid(schema, out IList<string> msg);
            //    Assert.IsTrue(result, "некорректные поля: " + string.Join(" ,", msg.ToArray()));
            // }           
        }
		        
    }
}