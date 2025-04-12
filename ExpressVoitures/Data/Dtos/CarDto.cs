namespace ExpressVoitures.Data.Dto
{
    public class CarDto
    {
        public int Id { get; set; }
        
        public string VinCode { get; set; } = String.Empty;
        public string Brand { get; set; } = String.Empty;
        public string Model { get; set; } = String.Empty;
        public string Version { get; set; } = String.Empty;
        public DateTime ManufactureDate { get; set; }
        public string? ImagePath { get; set; }
    }
}
