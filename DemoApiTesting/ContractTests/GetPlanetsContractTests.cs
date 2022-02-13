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
        [TestCase(9999)]
      

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
            
            // Сравнить ответ с файлом Json схема
            await CheckValidationResponseMessageBySchemaAsync(response, schema);
        }
		        
    }
}