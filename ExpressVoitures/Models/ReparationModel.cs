namespace ExpressVoitures.Models
{
    public class ReparationModel
    {
        public int Id { get; set; }

        public ICollection<VoitureModel> Voiture { get; set; }

        public ICollection<TypeModel> Type { get; set; }
    }
}