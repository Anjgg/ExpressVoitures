namespace ExpressVoitures.Data.Models
{
    public class VoitureModel
    {
        public required string CodeVin { get; set; }

        public required string Marque { get; set; }

        public required string Modele { get; set; }

        public required string Finition { get; set; }

        public int? Annee { get; set; }

        public string? ImagePath { get; set; }

        public ReparationModel? Reparations { get; set; }

        public PrixModel? Prix { get; set; }

        public DateModel? Date { get; set; }
    }
}
