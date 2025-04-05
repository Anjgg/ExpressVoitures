namespace ExpressVoitures.Data.Models
{
    public class TypeModel
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public decimal Prix { get; set; }
        public decimal Duree { get; set; }

        public ICollection<ReparationTypeModel> ReparationTypes { get; set; } = new List<ReparationTypeModel>();
    }
}