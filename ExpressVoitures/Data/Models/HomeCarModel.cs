namespace ExpressVoitures.Data.Models
{
    public class HomeCarModel
    {
        public int Id { get; set; }

        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public DateTime Annee { get; set; }

        public int PrixVente { get; set; }
    }
}