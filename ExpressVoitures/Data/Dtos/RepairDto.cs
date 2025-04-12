namespace ExpressVoitures.Data.Dto
{
    public class RepairDto
    {
        public int Id { get; set; }

        public required string Description { get; set; }
        public decimal Price { get; set; }
        public double DaysOfWork { get; set; }

        public int CarId { get; set; }
    }
}