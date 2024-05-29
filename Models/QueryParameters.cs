namespace MerchStore.Models
{
    public class QueryParameters
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public double minPrice { get; set; }
        public double maxPrice { get; set; }
        public int stock { get; set; }
        public string category { get; set; }
    }
}
