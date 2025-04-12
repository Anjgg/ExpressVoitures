namespace ExpressVoitures.Data.Models
{
    public class HomeCarModel
    {
        public int Id { get; set; }

        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public DateTimeOffset DateFabrication { get; set; }

        public int PrixVente { get; set; }
    }
}