namespace ExpressVoitures.Models
{
    public class PrixModel
    {
        public int Id { get; set; }

        public decimal PrixAchat { get; set; }

        public decimal PrixReparation { get; set; }

        public decimal PrixVente { get; set; }

        public VoitureModel Voiture { get; set; }
    }
}