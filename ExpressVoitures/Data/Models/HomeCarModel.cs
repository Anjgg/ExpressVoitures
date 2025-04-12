namespace ExpressVoitures.Data.Models
{
    public class HomeCarModel
    {
        public int Id { get; set; }

        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public string? ImagePath { get; set; }
        public DateTimeOffset DateFabrication { get; set; }
        public bool IsAvailable { get; set; } = true;


        public int PrixVente { get; set; }
    }
}