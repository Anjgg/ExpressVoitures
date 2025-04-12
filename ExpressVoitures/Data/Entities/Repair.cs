namespace ExpressVoitures.Data.Entities
{
    public class Repair
    {
        public int Id { get; set; }

        public string Description { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public double DaysOfWork { get; set; }

        public int CarId { get; set; }
        public virtual Car? Car { get; set; }
    }
}
