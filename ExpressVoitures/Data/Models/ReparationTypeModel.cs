namespace ExpressVoitures.Data.Models
{
    public class ReparationTypeModel
    {
        public int Id { get; set; }
        public int ReparationId { get; set; }
        public int TypeId { get; set; }

        public ReparationModel Reparation { get; set; } = null!;
        public Type Type { get; set; } = null!;
    }
}
