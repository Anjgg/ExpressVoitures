namespace ExpressVoitures.Data.Models
{
    public class VoitureModel
    {
        public string CodeVin { get; set; }

        public string Marque { get; set; }

        public string Modele { get; set; }

        public string Finition { get; set; }

        public int Annee { get; set; }

        public ReparationModel Reparations { get; set; }

        public PrixModel Prix { get; set; }

        public DateModel Date { get; set; }
    }
}
