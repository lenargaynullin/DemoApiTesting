namespace DemoApiTesting.models
{
    public class PetRequest
    {
        public int Id { get; set; }
        public IdName Category { get; set; }
        public string Name { get; set; }
        public string[] PhotoUrls { get; set; }
        public IdName[] Tags { get; set; }
        public string Status { get; set; }
    }

    public class IdName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
}