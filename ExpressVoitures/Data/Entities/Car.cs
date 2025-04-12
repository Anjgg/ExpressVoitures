namespace ExpressVoitures.Data.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public string VinCode { get; set; } = String.Empty;
        public string Brand { get; set; } = String.Empty;
        public string Model { get; set; } = String.Empty;
        public string Version { get; set; } = String.Empty;
        public DateTime ManufactureDate { get; set; }
        public string? ImagePath { get; set; }

        public virtual IEnumerable<Repair> Repairs { get; set; } = new List<Repair>();
        public virtual Price Price { get; set; } = new Price();
        public virtual EventHistory EventHistory { get; set; } = new EventHistory();
    }
}
