namespace ExpressVoitures.Data.Models
{
    public class ReparationModel
    {
        public int Id { get; set; }
        
        
        public ICollection<ReparationTypeModel> ReparationTypes { get; set; } = new List<ReparationTypeModel>();

    }
}