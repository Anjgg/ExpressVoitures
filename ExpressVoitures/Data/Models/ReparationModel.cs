namespace ExpressVoitures.Data.Models
{
    public class ReparationModel
    {
        public int Id { get; set; }

        public ICollection<VoitureModel> Voitures { get; set; }

        public ICollection<TypeModel> Types { get; set; }
    }
}