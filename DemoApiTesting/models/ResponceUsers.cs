using System.Collections.Generic;


namespace DemoApiTesting.models
{
    
    public class ResponceUsers
    {
        public string page { get; set; }
        public string per_page { get; set; }
        public string total { get; set; }
        public string total_pages { get; set; }
        public string data { get; set; }
        public string[] support { get; set; }

        
    }

    public class Workers
    {
        public string url { get; set; }
        public string text { get; set; }
    }

    Workers resObj = JsonConvert.DeserializeObject<Workers>(JSON_STRING);

    Worker result = resObj.workers.Find(x => x.id == "5");
}
