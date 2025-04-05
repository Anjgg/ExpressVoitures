namespace ExpressVoitures.Data.Models
{
    public class CreateModel
    {
        public string CodeVin { get; set; } = null!;
        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public string Finition { get; set; } = null!;
        public int? Annee { get; set; }
        public string? ImagePath { get; set; } 
        public DateTimeOffset DateAchat { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset DateMiseEnVente { get; set; }
        public DateTimeOffset? DateVente { get; set; }
        public decimal PrixAchat { get; set; }
        public decimal PrixReparation { get; set; }
        public decimal PrixVente { get; set; }
    }
}
