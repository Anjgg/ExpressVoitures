namespace ExpressVoitures.Data.Models
{
    public class ReparationModel
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;
        public decimal Prix { get; set; }
        public double Duree { get; set; }

        public VoitureModel? Voiture { get; set; }
    }
}