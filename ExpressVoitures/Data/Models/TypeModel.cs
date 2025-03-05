namespace ExpressVoitures.Data.Models
{
    public class TypeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Prix { get; set; }

        public decimal Duree { get; set; }

        public ICollection<ReparationModel> Reparation { get; set; }
    }
}