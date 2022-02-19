namespace DemoApiTesting
{
    public class PostPet
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string[] PhotoUrls { get; set; }
        public Category[] Tags { get; set; }
        public string Status { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}