namespace radar_api.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string UrlCode { get; set; }
        public decimal TargetVariation { get; set; }
    }
}
