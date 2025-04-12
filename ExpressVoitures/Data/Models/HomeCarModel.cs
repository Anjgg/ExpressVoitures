namespace ExpressVoitures.Data.Models
{
    public class HomeCarModel
    {
        public int Id { get; set; }
        public string Brand { get; set; } = String.Empty;
        public string Model { get; set; } = String.Empty;
        public DateTime ManufactureDate { get; set; }
        public decimal SellingPrice { get; set; }
        public bool IsAvailable { get; set; }
    }
}
