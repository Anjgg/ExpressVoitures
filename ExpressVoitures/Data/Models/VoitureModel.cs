namespace ExpressVoitures.Data.Models
{
    public class VoitureModel
    {
        public string CodeVin { get; set; } = null!;
        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public string Finition { get; set; } = null!;
        public DateTime Annee { get; set; }
        public string? ImagePath { get; set; }

        public IEnumerable<ReparationModel> Reparations { get; set; } = new List<ReparationModel>();
        public PrixModel Prix { get; set; } = new PrixModel();
        public DateModel Date { get; set; } = new DateModel();
    }
}
