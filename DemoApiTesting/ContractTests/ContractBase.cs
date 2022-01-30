using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace DemoApiTesting.ContractTests
{
    public abstract class ContractBase
    {
        protected string GetFileAsString(string fileName)
        {
            var direct = Directory.GetCurrentDirectory();
            var path = direct.Substring(0, direct.IndexOf(@"\bin\", StringComparison.Ordinal));
            var fullPath = path + @"\ContractTests\contracts\";
            return File.ReadAllText(fullPath + fileName);
        }

        protected async Task CheckValidationResponseMessageBySchemaAsync(HttpResponseMessage response, JSchema schema)
        {
            JObject jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
            bool result = jObject.IsValid(schema, out IList<string> msg);
            Assert.IsTrue(result, "некорректные поля: " + string.Join(" ,", msg.ToArray()));
        }
    }
}